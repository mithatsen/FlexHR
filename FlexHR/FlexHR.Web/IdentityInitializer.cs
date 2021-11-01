using FlexHR.Business.Concrete;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,IStaffService staffService ,
            IStaffPersonelInfoService personelInfoService,IStaffOtherInfoService otherInfoService,IStaffSalaryService staffSalaryService)
        {
            var userCount = userManager.Users.ToList().Count;
            if (userCount <= 0)
            {
                var adminRole = await roleManager.FindByNameAsync("Manager");
                if (adminRole == null)
                {
                    await roleManager.CreateAsync(new AppRole { Name = "Manager" });
                }
                var adminUser = await userManager.FindByNameAsync("admin");
                if (adminUser == null)
                {
                    var addedStaff=staffService.AddResult(new Staff
                    {
                        NameSurname="Admin",
                        EmailPersonal="mail@mail.com",
                        PhonePersonal="0123456789",
                        JobJoinDate=DateTime.Now,
                        IsActive=true,
                        WillUseSystem=true,
                        ContractTypeGeneralSubTypeId=2,
                        PersonalNo=1
                    });
                    personelInfoService.Add(new StaffPersonelInfo
                    {
                        StaffId = addedStaff.StaffId,
                        IsActive = true
                    }); 
                    otherInfoService.Add(new StaffOtherInfo
                    {
                        StaffId = addedStaff.StaffId,
                        IsActive = true
                    }); 
                    staffSalaryService.Add(new StaffSalary 
                    {
                        StaffId = addedStaff.StaffId,
                        CurrencyGeneralSubTypeId = 111,
                        PeriodGeneralSubTypeId = 115,
                        FeeTypeGeneralSubTypeId = 107,
                        StartDate = DateTime.Now,
                        AgiPayment = 12,
                        Salary = 2825, 
                        IsActive = true }
                    );
                    AppUser user = new AppUser
                    {
                        UserName = "admin",
                        StaffId = addedStaff.StaffId,
                        IsActive = true
                    };
                    await userManager.CreateAsync(user, "Test.123");
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }

        }
    }
}
