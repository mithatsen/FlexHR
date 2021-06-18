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
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IStaffCareerService, StaffCareerManager>();
            services.AddScoped<IStaffDebitService, StaffDebitManager>();
            services.AddScoped<IStaffGeneralSubTypeService, StaffGeneralSubTypeManager>();
            services.AddScoped<IStaffLeaveService, StaffLeaveManager>();
            services.AddScoped<IStaffOtherInfoService, StaffOtherInfoManager>();
            services.AddScoped<IStaffPaymentService, StaffPaymentManager>();
            services.AddScoped<IStaffPersonelInfoService, StaffPersonelInfoManager>();
            services.AddScoped<IStaffService, StaffManager>();
            services.AddScoped<IStaffSalaryService, StaffSalaryManager>();
            services.AddScoped<IStaffShiftService, StaffShiftManager>();
            services.AddScoped<ITownService, TownManager>();
            services.AddScoped<IStaffRoleService, StaffRoleManager>();
            services.AddScoped<IStaffFileService, StaffFileManager>();

            services.AddScoped<ICityDal, EfCityRepository>();
            services.AddScoped<ICompanyDal, EfCompanyRepository>();
            services.AddScoped<ICompanyBranchDal, EfCompanyBranchRepository>();
            services.AddScoped<ICountryDal, EfCountryRepository>();
            services.AddScoped<IEmailHistoryDal, EfEmailHistoryRepository>();
            services.AddScoped<IEventDal, EfEventRepository>();
            services.AddScoped<IGeneralSubTypeDal, EfGeneralSubTypeRepository>();
            services.AddScoped<IGeneralTypeDal, EfGeneralTypeRepository>();
            services.AddScoped<IPublicHolidayDal, EfPublicHolidayRepository>();
            services.AddScoped<IRoleDal, EfRoleRepository>();
            services.AddScoped<IStaffDal, EfStaffRepository>();
            services.AddScoped<IStaffCareerDal, EfStaffCareerRepository>();
            services.AddScoped<IStaffDebitDal, EfStaffDebitRepository>();
            services.AddScoped<IStaffFileDal, EfStaffFileRepository>();
            services.AddScoped<IStaffGeneralSubTypeDal, EfStaffGeneralSubTypeRepository>();
            services.AddScoped<IStaffLeaveDal, EfStaffLeaveRepository>();
            services.AddScoped<IStaffOtherInfoDal, EfStaffOtherInfoRepository>();
            services.AddScoped<IStaffPaymentDal, EfStaffPaymentDalRepository>();
            services.AddScoped<IStaffPersonelInfoDal, EfStaffPersonelInfoRepository>();
            services.AddScoped<IStaffSalaryDal, EfStaffSalaryRepository>();
            services.AddScoped<IStaffShiftDal, EfStaffShiftRepository>();
            services.AddScoped<ITownDal, EfTownRepository>();
            services.AddScoped<IStaffRoleDal, EfStaffRoleRepository>();
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
        }
    }
}
