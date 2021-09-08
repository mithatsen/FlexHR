using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffTracking;
using FlexHR.Entity.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffTrackingRepository : IStaffTrackingDal
    {
        private readonly IStaffTrackingDal _staffTrackingDal;
        private readonly IConfiguration _configuration;

        public EfStaffTrackingRepository(IStaffTrackingDal staffTrackingDal, IConfiguration configuration)
        {
            _staffTrackingDal = staffTrackingDal;
            _configuration = configuration;
        }

        public List<ListStaffTimeKeepingDto> GetStaffTimeKeepingMonthly(DateTime dateTime, List<Staff> staffs)
        {
            // SELECT* FROM StaffTracking WHERE YEAR(UploadDate) = '2021' AND MONTH(UploadDate) = '10';
            List<ListStaffTimeKeepingDto> models = new List<ListStaffTimeKeepingDto>();
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionStrings);



            foreach (var item in staffs)
            {
                var query = $"SELECT DURUMU FROM StaffTracking WHERE YEAR(UploadDate) = '{dateTime.Year}' AND MONTH(UploadDate) = '{dateTime.Month}' and KART_NO='{item.PersonalNo}' ORDER BY UploadDate; ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = query;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var status = "";
                List<string> statusList = new List<string>();
                while (dr.Read())
                {
                    if (dr.GetString(0) == "Devamsız")
                    {
                        status = "D";
                    }
                    else if (dr.GetString(0) == "Yıllık İzin")
                    {
                        status = "İ";
                    }
                    else if (dr.GetString(0) == "RAPORLU")
                    {
                        status = "R";
                    }
                    else
                    {
                        status = "X";
                    }
                    statusList.Add(status);
                }
                dr.Close();
                con.Close();
                models.Add(new ListStaffTimeKeepingDto
                {
                    DaysStatus = statusList,
                    NameSurname = item.NameSurname,
                    PersonalNo = item.PersonalNo
                });



            }
            return models;


        }
    }
}
