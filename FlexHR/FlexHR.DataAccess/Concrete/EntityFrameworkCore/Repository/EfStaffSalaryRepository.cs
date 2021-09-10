using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.DTO.Dtos.StaffSalaryDtos;
using FlexHR.DTO.ViewModels;
using FlexHR.Entity.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffSalaryRepository : EfGenericRepository<StaffSalary>, IStaffSalaryDal
    {
        private readonly FlexHRContext _context;
        private readonly IConfiguration _configuration;
        public EfStaffSalaryRepository(FlexHRContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<StaffTrackingMothlySalaryHelper> GetStaffSalaryMonthly(DateTime dateTime)
        {
           // List<ListStaffSalaryMonthlyDto> models = new List<ListStaffSalaryMonthlyDto>();
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection con = new SqlConnection(connectionStrings);
            var queryMain = $"SELECT  KART_NO, GIRIS_SAAT, CIKIS_SAAT,FAZLA_MESAI,SEHIR,BOLUM  FROM StaffTracking WHERE YEAR(UploadDate) = '{dateTime.Year}' AND MONTH(UploadDate) = '{dateTime.Month}' ORDER BY KART_NO; ";
            SqlCommand cmdMain = new SqlCommand();
            cmdMain.Connection = con;
            cmdMain.CommandText = queryMain;
            con.Open();
            SqlDataReader drMain = cmdMain.ExecuteReader();
            List<StaffTrackingMothlySalaryHelper> mainModels = new List<StaffTrackingMothlySalaryHelper>();
            while (drMain.Read())
            {
                mainModels.Add(new StaffTrackingMothlySalaryHelper { CardNo = drMain.GetString(0), EntryTime = drMain.GetString(1), CkeckoutTime = drMain.GetString(2), Overtime = drMain.GetString(3), Branch = drMain.GetString(4), Department = drMain.GetString(5) });

            }
            con.Close();
            return mainModels;
        }

    }
}
