using FlexHR.Business.Concrete;
using FlexHR.Business.Interface;
using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository;
using FlexHR.DataAccess.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.DIContainer
{
    public static class CustomExtensions
    {
        public static void AddContainerWithDependencies(this IServiceCollection services)
        {
            services.AddDbContext<FlexHRContext>();
            services.AddScoped<ICityService, CityManager>();
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<ICompanyBranchService, CompanyBranchManager>();
            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<IEmailHistoryService, EmailHistoryManager>();
            services.AddScoped<IEventService, EventManager>();
            services.AddScoped<IGeneralSubTypeService, GeneralSubTypeManager>();
            services.AddScoped<IGeneralTypeService, GeneralTypeManager>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped<IPublicHolidayService, PublicHolidayManager>();
            services.AddScoped<IAppRoleService, AppRoleManager>();           
            services.AddScoped<IStaffCareerService, StaffCareerManager>();
            services.AddScoped<IStaffDebitService, StaffDebitManager>();
            services.AddScoped<IStaffLeaveService, StaffLeaveManager>();
            services.AddScoped<IStaffOtherInfoService, StaffOtherInfoManager>();
            services.AddScoped<IStaffPaymentService, StaffPaymentManager>();
            services.AddScoped<IStaffPersonelInfoService, StaffPersonelInfoManager>();
            services.AddScoped<IStaffService, StaffManager>();
            services.AddScoped<IStaffSalaryService, StaffSalaryManager>();
            services.AddScoped<IStaffShiftService, StaffShiftManager>();
            services.AddScoped<ITownService, TownManager>();
            services.AddScoped<IStaffFileService, StaffFileManager>();
            services.AddScoped<ICompanyFileService, CompanyFileManager>();
            services.AddScoped<IReceiptService, ReceiptManager>();
            services.AddScoped<ILeaveTypeService, LeaveTypeManager>();
            services.AddScoped<ILeaveRuleService, LeaveRuleManager>();
            services.AddScoped<IStaffTrackingService, StaffTrackingManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IFileColumnService, FileColumnManager>();

            services.AddScoped<IStaffTrackingDal, EfStaffTrackingRepository>();
            services.AddScoped<IReceiptDal, EfReceiptRepository>();
            services.AddScoped<ICityDal, EfCityRepository>();
            services.AddScoped<ICompanyDal, EfCompanyRepository>();
            services.AddScoped<ICompanyBranchDal, EfCompanyBranchRepository>();
            services.AddScoped<ICountryDal, EfCountryRepository>();
            services.AddScoped<IEmailHistoryDal, EfEmailHistoryRepository>();
            services.AddScoped<IEventDal, EfEventRepository>();
            services.AddScoped<IGeneralSubTypeDal, EfGeneralSubTypeRepository>();
            services.AddScoped<IGeneralTypeDal, EfGeneralTypeRepository>();
            services.AddScoped<IPublicHolidayDal, EfPublicHolidayRepository>();
             services.AddScoped<IAppRoleDal, EfAppRoleRepository>();
            services.AddScoped<IStaffDal, EfStaffRepository>();
            services.AddScoped<IStaffCareerDal, EfStaffCareerRepository>();
            services.AddScoped<IStaffDebitDal, EfStaffDebitRepository>();
            services.AddScoped<IStaffFileDal, EfStaffFileRepository>();
            services.AddScoped<ICompanyFileDal, EfCompanyFileRepository>();
            services.AddScoped<IStaffLeaveDal, EfStaffLeaveRepository>();
            services.AddScoped<IStaffOtherInfoDal, EfStaffOtherInfoRepository>();
            services.AddScoped<IStaffPaymentDal, EfStaffPaymentDalRepository>();
            services.AddScoped<IStaffPersonelInfoDal, EfStaffPersonelInfoRepository>();
            services.AddScoped<IStaffSalaryDal, EfStaffSalaryRepository>();
            services.AddScoped<IStaffShiftDal, EfStaffShiftRepository>();
            services.AddScoped<ITownDal, EfTownRepository>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();
         //   services.AddScoped<IStaffRoleDal, EfStaffRoleRepository>();
            services.AddScoped<ILeaveTypeDal, EfLeaveTypeRepository>();
            services.AddScoped<ILeaveRuleDal, EfLeaveRuleRepository>();
            services.AddScoped<IFileColumnDal, EfFileColumnRepository>();
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
        }
    }
}
