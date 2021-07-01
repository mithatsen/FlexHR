using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCardDtos;
using FlexHR.Entity.Enums;
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
        private readonly IStaffFileService _staffFileService;

        public StaffCard(IStaffCareerService staffCareerService, IGeneralSubTypeService generalSubTypeService, ICompanyService companyService, IStaffService staffService, IStaffFileService staffFileService)
        {
            _staffCareerService = staffCareerService;
            _generalSubTypeService = generalSubTypeService;
            _companyService = companyService;
            _staffService = staffService;
            _staffFileService = staffFileService;

        }
        public IViewComponentResult Invoke(int id)
        {
            var careerResult = _staffCareerService.Get(x => x.IsActive == true && x.StaffId == id, null, "CompanyBranch").OrderByDescending(p=>p.JobStartDate).ToList();

            var staff = _staffService.GetAllTables(id);
            var picture = _staffFileService.Get(x => x.StaffId == id && x.IsActive == true && x.FileGeneralSubTypeId == 3).OrderByDescending(x => x.StaffFileId).FirstOrDefault();

            StaffCardDto staffCardDto;

            if (careerResult.Count > 0)
            {
                var activeCareer = careerResult.First();
                staffCardDto = new StaffCardDto
                {
                    DepartmantName = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.DepartmantGeneralSubTypeId),
                    BranchName = activeCareer.CompanyBranch != null ? activeCareer.CompanyBranch.BranchName : "-",
                    CompanyName = _companyService.GetCompanyNameByCompanyId(activeCareer.CompanyId),
                    Title = _generalSubTypeService.GetDescriptionByGeneralSubTypeId(activeCareer.TitleGeneralSubTypeId),
                    PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : null

                };
            }
            else
            {
                staffCardDto = new StaffCardDto
                {
                    DepartmantName = "-",
                    BranchName = "-",
                    CompanyName = "-",
                    Title = "-",
                    PictureUrl = picture != null ? picture.FileFullPath + picture.FileName : null
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
