using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using FlexHR.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class FlexHRContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public FlexHRContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            base.OnConfiguring(optionsBuilder);
        }
      

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyBranch> CompanyBranch { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<EmailHistory> EmailHistory { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<GeneralSubType> GeneralSubType { get; set; }
        public virtual DbSet<GeneralType> GeneralType { get; set; }
        public virtual DbSet<PublicHoliday> PublicHoliday { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffCareer> StaffCareer { get; set; }
        public virtual DbSet<StaffDebit> StaffDebit { get; set; }
        public virtual DbSet<StaffFile> StaffFile { get; set; }
        public virtual DbSet<StaffGeneralSubType> StaffGeneralSubType { get; set; }
        public virtual DbSet<StaffLeave> StaffLeave { get; set; }
        public virtual DbSet<StaffOtherInfo> StaffOtherInfo { get; set; }
        public virtual DbSet<StaffPayment> StaffPayment { get; set; }
        public virtual DbSet<StaffPersonelInfo> StaffPersonelInfo { get; set; }
        public virtual DbSet<StaffSalary> StaffSalary { get; set; }
        public virtual DbSet<StaffShift> StaffShift { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<StaffRole> StaffRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyBranchMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new CountryMap());
            modelBuilder.ApplyConfiguration(new EmailHistoryMap());
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new GeneralSubTypeMap());
            modelBuilder.ApplyConfiguration(new GeneralTypeMap());
            modelBuilder.ApplyConfiguration(new PublicHolidayMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new StaffCareerMap());
            modelBuilder.ApplyConfiguration(new StaffDebitMap());
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new TownMap());
            modelBuilder.ApplyConfiguration(new StaffFileMap());
            modelBuilder.ApplyConfiguration(new StaffGeneralSubTypeMap());
            modelBuilder.ApplyConfiguration(new StaffLeaveMap());
            modelBuilder.ApplyConfiguration(new StaffMap());
            modelBuilder.ApplyConfiguration(new StaffOtherInfoMap());
            modelBuilder.ApplyConfiguration(new StaffPaymentMap());
            modelBuilder.ApplyConfiguration(new StaffPersonelInfoMap());
            modelBuilder.ApplyConfiguration(new StaffSalaryMap());
            modelBuilder.ApplyConfiguration(new StaffShiftMap());
            modelBuilder.ApplyConfiguration(new StaffRoleMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
