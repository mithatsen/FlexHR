using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffCareerDtos;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffCareer : ViewComponent
    {
        private readonly IStaffCareerService _careerService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IMapper _mapper;
        public StaffCareer(IStaffCareerService careerService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService)
        {
            _careerService = careerService;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int id)
        {
            var result = _careerService.GetAllTableByStaffId(id);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var departmentName = "";
            var title = "";
           
            var models = new List<ListStaffCareerDto>();
            foreach (var item in result)
            {
                var model = new ListStaffCareerDto
                {
                    //CompanyBranchId = item.CompanyBranchId,
                    //CompanyId=item.CompanyId,
                    //DepartmantGeneralSubTypeId=item.DepartmantGeneralSubTypeId,
                    //DepartmantName=departmentName,
                    //IsActive=item.IsActive,
                    //JobFinishDate=item.JobFinishDate,
                    //JobStartDate=item.JobStartDate,
                    //ModeOfOperationGeneralSubTypeId=item.ModeOfOperationGeneralSubTypeId,
                    //StaffCareerId=item.StaffCareerId,

                    JobStartDate = item.JobStartDate,
                    JobFinishDate = item.JobFinishDate,
                   // ModeOfOperation = _staffGeneralSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ModeOfOperation,temp),
                    CompanyName = item.CompanyBranch.Company.CompanyName,
                    BranchName = item.CompanyBranch.BranchName,
                 //   DepartmantName = _staffGeneralSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Department, temp),
                 //   Title = _staffGeneralSubTypeService.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.Title, temp),
                    IsActive = item.IsActive

                };
                models.Add(model);
            }

            return View(models);
        }
    }
}
