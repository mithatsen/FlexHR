using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.ViewComponents
{
    public class StaffComponent : ViewComponent
    {
        private readonly IStaffService _staffService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
       
        private readonly IGeneralSubTypeService _generalSubType;
        private readonly IRoleService _roleService;

        public StaffComponent(IStaffService staffService, IStaffGeneralSubTypeService staffGeneralSubTypeService, IGeneralSubTypeService generalSubType, IRoleService roleService)
        {
            _staffService = staffService;
   
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


           

            var staffInfo = _staffGeneralSubTypeService.GetGeneralSubTypeByStaffGeneralSubTypeList(temp);
            for (int i = 0; i < staffInfo.Count; i++)
            {
                int generalTypeId= Convert.ToInt32(staffInfo.GetKey(i));

                if (generalTypeId == (int)GeneralTypeEnum.Department)
                {
                    departmentName = staffInfo[i];
                }
                else if (generalTypeId == (int)GeneralTypeEnum.Title)
                {
                    superscription = staffInfo[i];
                }
                else if (generalTypeId == (int)GeneralTypeEnum.ContractType)
                {
                    contractType = staffInfo[i];
                }
         
            }
        
            var roleList = _roleService.GetAll();
            var contractTypeList = _generalSubType.GetGeneralSubTypeByGeneralTypeId((int)GeneralTypeEnum.ContractType);


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
