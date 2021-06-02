using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffComponent:ViewComponent
    {
        private readonly IStaffService _staffService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IMapper _mapper;
        private readonly IGeneralSubTypeService _generalSubType;
        private readonly IRoleService _roleService;

        public StaffComponent(IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService, IGeneralSubTypeService generalSubType, IRoleService roleService)
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
            _generalSubType = generalSubType;
            _roleService = roleService;
        }

        public IViewComponentResult Invoke(int id)
        {
            var result = _staffService.GetAllTables(id);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var departmentName = "";
            var superscription = "";
            var contractType = "";
            foreach (var item in temp)
            {
                if (item.GeneralSubType.GeneralTypeId == 3)
                {
                    departmentName = item.GeneralSubType.Description;
                }
                else if (item.GeneralSubType.GeneralTypeId == 5)
                {
                    superscription = item.GeneralSubType.Description;
                }
                else if (item.GeneralSubType.GeneralTypeId == 1)
                {
                    contractType = item.GeneralSubType.Description;
                }
            }
            var contractTypeList = _generalSubType.GetGeneralSubTypeByGeneralTypeId(1);
            var roleList = _roleService.GetAll();



            var model = new UpdateStaffDto
            {
                EmailJob = result.EmailJob,
                EmailPersonal = result.EmailPersonal,
                JobFinishDate = result.JobFinishDate,
                JobJoinDate = result.JobJoinDate,
                NameSurname = result.NameSurname,
                PhoneJob = result.PhoneJob,
                PhonePersonal = result.PhonePersonal,
                StaffId = result.StaffId,
                IsActive = result.IsActive,
                DepartmantName = departmentName,
                Superscription = superscription,
                ContractTypeList = contractTypeList,
                ContractType = contractType,
                Roles = roleList
            };
            return View(model);
        }
    }
}
