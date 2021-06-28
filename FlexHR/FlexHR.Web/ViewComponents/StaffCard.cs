using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCardDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffCard : ViewComponent
    {
     
        private readonly IStaffCareerService _staffCareerService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly ICompanyService _companyService;
        private readonly IStaffService _staffService;

        public StaffCard(IStaffCareerService staffCareerService, IGeneralSubTypeService generalSubTypeService, ICompanyService companyService, IStaffService staffService)
        {
            _staffCareerService = staffCareerService;
            _generalSubTypeService = generalSubTypeService;
            _companyService = companyService;
            _staffService = staffService;

        }
        public IViewComponentResult Invoke(int id)
        {
            var careerResult = _staffCareerService.GetAllTableByStaffId(id);
            var staff = _staffService.GetAllTables(id);
            StaffCardDto staffCardDto;

            if (careerResult.Count>0)
            {
                var activeCareer = careerResult.First();
                staffCardDto = new StaffCardDto
                {
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.DepartmantGeneralSubTypeId),
                    BranchName = activeCareer.CompanyBranch != null ? activeCareer.CompanyBranch.BranchName : "-",
                    CompanyName = _companyService.GetCompanyNameByCompanyId(activeCareer.CompanyId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.TitleGeneralSubTypeId),

                };
            }
            else
            {
                staffCardDto = new StaffCardDto
                {
                    DepartmantName = "-",
                    BranchName = "-",
                    CompanyName = "-",
                    Title = "-"
                };
            }
            staffCardDto.JobJoinDate = staff.JobJoinDate;
            staffCardDto.NameSurname = staff.NameSurname;
            staffCardDto.EmailJob = staff.EmailJob;
            staffCardDto.PhoneJob = staff.PhoneJob;
            
            return View(staffCardDto);
        }
    }
}
