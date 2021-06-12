﻿using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.StaffDtos;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IStaffGeneralSubTypeService _staffGeneralSubTypeService;
        private readonly IMapper _mapper;
     
       

        public StaffController(IStaffService staffService, IMapper mapper, IStaffGeneralSubTypeService staffGeneralSubTypeService)
        {
            _staffService = staffService;
            _mapper = mapper;
            _staffGeneralSubTypeService = staffGeneralSubTypeService;
     
        }

        public IActionResult Index()
        {
            var result = _staffService.GetAll();
            return View(_mapper.Map<List<ListStaffDto>>(result));
        }
        public IActionResult UpdateStaff(int id)
        {
            var result = _staffService.GetAllTables(id);
            var temp = _staffGeneralSubTypeService.GetByStaffId(id);
            var departmentName = "";
            var superscription = "";
            //var contractType = "";
            //foreach (var item in temp)
            //{
            //    if (item.GeneralSubType.GeneralTypeId == 3)
            //    {
            //        departmentName = item.GeneralSubType.Description;
            //    }else if (item.GeneralSubType.GeneralTypeId == 5)
            //    {
            //        superscription = item.GeneralSubType.Description;
            //    }
            //    else if (item.GeneralSubType.GeneralTypeId == 1)
            //    {
            //        contractType = item.GeneralSubType.Description;
            //    }
            //}


            //var contractTypeList = _generalSubType.GetGeneralSubTypeByGeneralTypeId(1);
            var staffInfo = _staffGeneralSubTypeService.GetGeneralSubTypeByStaffGeneralSubTypeList(temp);
            for (int i = 0; i < staffInfo.Count; i++)
            {
                if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Department)
                {
                    departmentName = staffInfo[i];
                }
                else if (Convert.ToInt32(staffInfo.GetKey(i)) == (int)GeneralTypeEnum.Title)
                {
                    superscription = staffInfo[i];
                }
                //else if (Convert.ToInt32(temp5[i, 0]) == (int)GeneralTypeEnum.ContractType)
                //{
                //    contractType = temp5[i, 1];
                //}

            }
            var model = new UpdateStaffDto
            {
                StaffId = result.StaffId,
                EmailJob = result.EmailJob,
                EmailPersonal = result.EmailPersonal,
                JobJoinDate = result.JobJoinDate,
                NameSurname = result.NameSurname,
                PhoneJob = result.PhoneJob,
                PhonePersonal = result.PhonePersonal,                             
                DepartmantName = departmentName,
                Superscription = superscription,
            };
            return View(model);
        }
    }
}
