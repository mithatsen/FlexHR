﻿using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
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


        public List<Staff> GetStaffBySearchString(string search)
        {
            return _context.Staff.Include(p => p.StaffPersonelInfo).Where(p => p.IsActive == true && (p.NameSurname.Contains(search) || p.StaffPersonelInfo.FirstOrDefault().IdNumber.Contains(search))).ToList();
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
                mainModels.Add(new StaffTrackingHelper { UploadDate = drMain.GetDateTime(0), CardNo = drMain.GetString(1), Status = drMain.GetString(2),Branch= drMain.GetString(3),Department= drMain.GetString(4) });

            }

            foreach (var item in staffs)
            {
                var trackingList = mainModels.Where(x => Convert.ToInt32(x.CardNo) == item.PersonalNo).ToList();
                var status = "";
                List<string> statusList = new List<string>();
                for (int i = 1; i < 32; i++)
                {

                    for (int j = 0; j < trackingList.Count; j++)
                    {
                        if (trackingList[j].UploadDate.Day == i)
                        {
                            if (trackingList[j].Status == "Devamsız")
                            {
                                status = "D";

                            }
                            else if (trackingList[j].Status == "Yıllık İzin")
                            {
                                status = "İ";
                            }
                            else if (trackingList[j].Status == "RAPORLU")
                            { 
                                status = "R";
                            }
                            else
                            {
                                status = "X";
                            }
                        
                            break;
                        }
                        else
                        {
                            status = "";
                         
                        }
                    }
                    statusList.Add(status);
                }
                if (trackingList.Count != 0)
                {
                    models.Add(new ListStaffTimeKeepingDto
                    {
                        DaysStatus = statusList,
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
