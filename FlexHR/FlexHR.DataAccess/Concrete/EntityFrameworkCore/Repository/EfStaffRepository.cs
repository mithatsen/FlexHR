using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffRepository : EfGenericRepository<Staff>, IStaffDal
    {
        private readonly FlexHRContext _context;
        private readonly IConfiguration _configuration;
        public EfStaffRepository(FlexHRContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public StaffSalaryMonthlyHelper GetAbsenceInformationMonthly(DateTime dateTime, int staffId)
        {
            var jobRotation = _context.JobRotation.ToList();
            var jobRotationHistory = _context.JobRotationHistory.Include(x=>x.JobRotations).ToList();
            var cardNo = _context.Staff.FirstOrDefault(X => X.StaffId == staffId).PersonalNo;
            List<ListStaffTrackingDto> models = new List<ListStaffTrackingDto>();
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionStrings);
            var queryMain = $"SELECT  UploadDate,KART_NO,GIRIS_SAAT,CIKIS_SAAT,DURUMU FROM StaffTracking WHERE YEAR(UploadDate) = '{dateTime.Year}' AND MONTH(UploadDate) = '{dateTime.Month}' AND KART_NO='{cardNo}' ";
            SqlCommand cmdMain = new SqlCommand();
            cmdMain.Connection = con;
            cmdMain.CommandText = queryMain;
            con.Open();
            SqlDataReader drMain = cmdMain.ExecuteReader();

            while (drMain.Read())
            {
                models.Add(new ListStaffTrackingDto { UploadDate = drMain.GetDateTime(0), CardNo = Convert.ToInt32(drMain.GetString(1)), EnterTime = drMain.GetString(2), ExitTime = drMain.GetString(3), Status = drMain.GetString(4) });
            }
            int deductionMinute = 0;
            int deductionDay = 0;
            var resultColor = _context.ColorCode.ToList();
            var description = resultColor.FirstOrDefault(x => x.Description == "Devamsız").Name;

            foreach (var item in models)
            {
                var rotation = jobRotationHistory.Where(x => x.StaffId == staffId && x.JobRotationDate >= item.UploadDate).OrderByDescending(x => x.JobRotationDate).FirstOrDefault();   // <= vardı,  galiba >= olması gerekiyor 
                var shiftTime = rotation.JobRotations != null ? rotation.JobRotations.ShiftTime : jobRotation.FirstOrDefault().ShiftTime;
                if (item.Status == description) // o gün geldi mi
                {
                    deductionDay++;
                }
                else if (rotation.JobRotations != null)// o gün geldi ama saatinde geldi mi , daha önce vardiya tanmlama yapılmadıysa giriş çıkış saat hesabı olmuyor
                {
                    //MESAİ BAŞLANGICINDA GEÇ KALIRSA
                    if (Convert.ToDateTime((Convert.ToDateTime(item.EnterTime).ToString("hh:mm"))) - Convert.ToDateTime(rotation.JobRotations.StartDate.ToString("hh:mm")) > TimeSpan.Zero) //sabah geç geldi   **********devamsızlarda hata veriyor***************
                    {
                        deductionMinute += Convert.ToInt32((Convert.ToDateTime(Convert.ToDateTime(item.EnterTime).ToString("hh:mm")) - (Convert.ToDateTime(rotation.JobRotations.StartDate.ToString("hh:mm")))).TotalMinutes);
                    }
                    //MESAİ BİTİMİ ERKEN ÇIKARSA
                    if ((Convert.ToDateTime(Convert.ToDateTime(item.ExitTime).ToString("hh:mm"))) - Convert.ToDateTime(rotation.JobRotations.EndDate.ToString("hh:mm")) < TimeSpan.Zero)
    
                    {
                        deductionMinute += Convert.ToInt32((Convert.ToDateTime(rotation.JobRotations.EndDate.ToString("hh:mm"))-(Convert.ToDateTime(Convert.ToDateTime(item.ExitTime).ToString("hh:mm")))).TotalMinutes);
                    }
                    //burada vardiyada sadece saat tutulduğu için  2 takla attırdım
                }

            }


            return new StaffSalaryMonthlyHelper { Day = deductionDay, Hour = (float)deductionMinute / 60 };
        }

        public List<Staff> GetStaffBySearchString(string search)
        {
            return _context.Staff.Include(p => p.StaffPersonelInfo).Where(p => p.IsActive == true && (p.NameSurname.Contains(search) || p.StaffPersonelInfo.FirstOrDefault().IdNumber.Contains(search))).ToList();
        }

        public int GetStaffReportDayMonthly(DateTime dateTime, int cardNo)
        {
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionStrings);
            con.Open();
            var resultColor = _context.ColorCode.ToList();
            var desc = resultColor.FirstOrDefault(x => x.Description == "Raporlu").Name;
            var queryMain = $"select COUNT(*) from StaffTracking where YEAR(UploadDate) = '{dateTime.Year}' AND MONTH(UploadDate) = '{dateTime.Month}' AND KART_NO='{cardNo}' AND DURUMU='{desc}' ";
            SqlCommand cmdMain = new SqlCommand();
            cmdMain.Connection = con;
            cmdMain.CommandText = queryMain;

            SqlDataReader drMain = cmdMain.ExecuteReader();
            var reportDayCount = 0;
            while (drMain.Read())
            {
                reportDayCount = drMain.GetInt32(0);

            }

            con.Close();
            return reportDayCount;

        }

        public List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs)
        {
            List<ListStaffTimeKeepingDto> models = new List<ListStaffTimeKeepingDto>();
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionStrings);
            var queryMain = $"SELECT  UploadDate,KART_NO,DURUMU,SEHIR,BOLUM FROM StaffTracking WHERE YEAR(UploadDate) = '{dateTime.Year}' AND MONTH(UploadDate) = '{dateTime.Month}' ORDER BY UploadDate; ";
            SqlCommand cmdMain = new SqlCommand();
            cmdMain.Connection = con;
            cmdMain.CommandText = queryMain;
            con.Open();
            SqlDataReader drMain = cmdMain.ExecuteReader();
            List<StaffTrackingHelper> mainModels = new List<StaffTrackingHelper>();
            while (drMain.Read())
            {
                mainModels.Add(new StaffTrackingHelper { UploadDate = drMain.GetDateTime(0), CardNo = drMain.GetString(1), Status = drMain.GetString(2), Branch = drMain.GetString(3), Department = drMain.GetString(4) });

            }
            var resultColor = _context.ColorCode.ToList();
            List<ColorCodeHelper> colorCodes = new List<ColorCodeHelper>();

            foreach (var x in resultColor)
            {
                ColorCodeHelper colorCode = new ColorCodeHelper { Code = x.Code, Color = x.Color, Description = x.Description };
                colorCodes.Add(colorCode);
            }


            foreach (var item in staffs)
            {
                var trackingList = mainModels.Where(x => Convert.ToInt32(x.CardNo) == item.PersonalNo).ToList();

                List<ColorCodeHelper> statusList = new List<ColorCodeHelper>();
                for (int i = 1; i < 32; i++)
                {
                    ColorCodeHelper statusOnly = new ColorCodeHelper();
                    for (int j = 0; j < trackingList.Count; j++)
                    {
                        if (trackingList[j].UploadDate.Day == i)
                        {
                            foreach (var item2 in resultColor)
                            {

                                if (trackingList[j].Status == item2.Name)
                                {
                                    statusOnly.Code = item2.Code;
                                    statusOnly.Color = item2.Color;
                                    break;
                                }
                            }

                            break;
                        }
                        else
                        {
                            statusOnly.Code = "";
                            statusOnly.Color = "";
                        }
                    }
                    statusList.Add(statusOnly);
                }
                if (trackingList.Count != 0)
                {
                    models.Add(new ListStaffTimeKeepingDto
                    {
                        DaysStatus = statusList,
                        ColorCodes = colorCodes,
                        NameSurname = item.NameSurname,
                        PersonalNo = item.PersonalNo,
                        Branch = trackingList[0].Branch,
                        Department = trackingList[0].Department
                    });
                }




            }
            con.Close();
            return models;


        }

        //public int GetStaffIdByUserName(string userName)
        //{
        //    return _context.Staff.Where(p => p.UserName == userName).FirstOrDefault().StaffId;
        //}
    }
}
