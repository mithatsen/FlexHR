using AutoMapper;
using FlexHR.Business.Interface;
using FlexHR.DTO.Dtos.CompanyBranchDtos;
using FlexHR.DTO.Dtos.CompanyDtos;
using FlexHR.DTO.Dtos.GeneralSubTypeDtos;
using FlexHR.DTO.Dtos.LeaveRuleDtos;
using FlexHR.DTO.Dtos.LeaveTypeDtos;
using FlexHR.DTO.Dtos.RoleDtos;
using FlexHR.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexHR.Web.Controllers
{
    public class SystemVariableController : Controller
    {
        private readonly IGeneralTypeService _generalTypeService;
        private readonly IGeneralSubTypeService _generalSubTypeService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRuleService _leaveRuleService;        
        private readonly ICompanyBranchService _companyBranchService;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IAppRoleService _appRoleService;

        public SystemVariableController(IGeneralTypeService generalTypeService, IGeneralSubTypeService generalSubTypeService, IMapper mapper, ILeaveTypeService leaveTypeService, ILeaveRuleService leaveRuleService,
            ICompanyService companyService,  ICompanyBranchService companyBranchService, IAppRoleService appRoleService)
        {
            _generalTypeService = generalTypeService;
            _generalSubTypeService = generalSubTypeService;
            _mapper = mapper;
            _leaveTypeService = leaveTypeService;
            _companyService = companyService;
            _leaveRuleService = leaveRuleService;
            _appRoleService = appRoleService;
            _companyBranchService = companyBranchService;
        }

        public IActionResult Index()
        {
            ViewBag.GeneralTypeList = new SelectList(_generalTypeService.Get(p => p.IsActive == true), "GeneralTypeId", "Description");
            ViewBag.GeneralSubTypeUpdateStatus = TempData["GeneralSubTypeUpdateStatus"] != null ? TempData["GeneralSubTypeUpdateStatus"] : "false";
            return View();
        }
        public IActionResult GetGeneralTypeById(int id)
        {
            var result = _generalSubTypeService.GetGeneralSubTypeByGeneralTypeId(id);
            return PartialView("_GetGeneralSubTypeTable", _mapper.Map<List<ListGeneralSubTypeDto>>(result));
        } 
       
        public IActionResult GetUpdateGeneralSubTypeModal(int id)
        {
            var result = _generalSubTypeService.GetById(id);
            return PartialView("_GetGeneralSubTypeUpdateModal", _mapper.Map<ListGeneralSubTypeDto>(result));
        }
        public IActionResult GetUpdateLeaveTypeModal(int id)
        {
            var result = _leaveTypeService.GetById(id);         
            return PartialView("_GetLeaveTypeUpdateModal", _mapper.Map<ListLeaveTypeDto>(result));
        }
        public IActionResult GetUpdateLeaveRuleModal(int id)
        {
            var result = _leaveRuleService.GetById(id);
            return PartialView("_GetLeaveRuleUpdateModal", _mapper.Map<ListLeaveRuleDto>(result));
        } 
        public IActionResult GetUpdateCompanyModal(int id)
        {
            var result = _companyService.GetById(id);
            return PartialView("_GetCompanyUpdateModal", _mapper.Map<ListCompanyDto>(result));
        }
        public IActionResult GetUpdateRoleModal(int id)
        {
            var result = _appRoleService.GetById(id);
            return PartialView("_GetRoleUpdateModal", _mapper.Map<ListRoleDto>(result));
        }
        public IActionResult GetUpdateCompanyBranchModal(int id)
        {
            ViewBag.Companies = new SelectList(_companyService.Get(p=>p.IsActive==true), "CompanyId", "CompanyName");
            var result = _companyBranchService.GetById(id);
            return PartialView("_GetCompanyBranchUpdateModal", _mapper.Map<ListCompanyBranchDto>(result));
        }


        [HttpPost]
        public bool AddGeneralSubType(AddGeneralSubTypeDto model)
        {
            var temp = _generalSubTypeService.Get(p => p.GeneralTypeId == model.GeneralTypeId).LastOrDefault().Name;
            var temp2 = Convert.ToInt32(temp.Substring(1)) + 1;
            temp = temp.Substring(0, 1);
            temp += temp2;
            model.Name = temp;
            _generalSubTypeService.Add(_mapper.Map<GeneralSubType>(model));
            //TempData["GeneralSubTypeUpdateStatus"] = "true";
            return true;
        }
        [HttpPost]
        public bool AddLeaveType(AddLeaveTypeDto model)
        {
            try
            {
                _leaveTypeService.Add(_mapper.Map<LeaveType>(model));
                return true;
            }
            catch (Exception)
            {
                return false;
            }      
        }
        public bool AddLeaveRule(AddLeaveRuleDto model)
        {
            try
            {
                _leaveRuleService.Add(_mapper.Map<LeaveRule>(model));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool AddCompany(AddCompanyDto model)
        {
            try
            {
                _companyService.Add(_mapper.Map<Company>(model));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool AddCompanyBranch(AddCompanyBranchDto model)
        {
            try
            {
                _companyBranchService.Add(_mapper.Map<CompanyBranch>(model));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool AddRole(AddRoleDto model)
        {
            try
            {
                _appRoleService.Add(_mapper.Map<AppRole>(model));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        [HttpPost]
        public IActionResult UpdateGeneralSubType(ListGeneralSubTypeDto model)
        {
            _generalSubTypeService.Update(_mapper.Map<GeneralSubType>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateLeaveType(ListLeaveTypeDto model)
        {
            model.IsActive = true;
            _leaveTypeService.Update(_mapper.Map<LeaveType>(model));
       
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateLeaveRule(ListLeaveRuleDto model)
        {
            model.IsActive = true;
            _leaveRuleService.Update(_mapper.Map<LeaveRule>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        } 
        [HttpPost]
        public IActionResult UpdateCompany(ListCompanyDto model)
        {
            model.IsActive = true;
            _companyService.Update(_mapper.Map<Company>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateRole(ListRoleDto model)
        {
            model.IsActive = true;
            _appRoleService.Update(_mapper.Map<AppRole>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCompanyBranch(ListCompanyBranchDto model)
        {
            model.IsActive = true;
            _companyBranchService.Update(_mapper.Map<CompanyBranch>(model));
            TempData["GeneralSubTypeUpdateStatus"] = "true";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public bool DeleteGeneralSubtype(int id)
        {
            try
            {
                var career = _generalSubTypeService.GetById(id);
                career.IsActive = false;
                _generalSubTypeService.Update(career);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteLeaveType(int id)
        {
            try
            {
                var temp = _leaveTypeService.GetById(id);
                temp.IsActive = false;
                _leaveTypeService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteLeaveRule(int id)
        {
            try
            {
                var temp = _leaveRuleService.GetById(id);
                temp.IsActive = false;
                _leaveRuleService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteCompany(int id)
        {
            try
            {
                var temp = _companyService.GetById(id);
                temp.IsActive = false;
                _companyService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteCompanyBranch(int id)
        {
            try
            {
                var temp = _companyBranchService.GetById(id);
                temp.IsActive = false;
                _companyBranchService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool DeleteRole(int id)
        {
            try
            {
                var temp = _appRoleService.GetById(id);
                temp.IsActive = false;
                _appRoleService.Update(temp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IActionResult GetLeaveTypeList()
        {
            var result = _leaveTypeService.GetAll();
            return PartialView("_GetLeaveTypeTable", _mapper.Map<List<ListLeaveTypeDto>>(result));
        }
        public IActionResult GetLeaveRuleList()
        {
            var result = _leaveRuleService.GetAll().OrderBy(x=>x.SeniorityYear);
            return PartialView("_GetLeaveRuleTable", _mapper.Map<List<ListLeaveRuleDto>>(result));
        }
        public IActionResult GetCompanyList()
        {
            var result = _companyService.GetAll();
            return PartialView("_GetCompanyTable", _mapper.Map<List<ListCompanyDto>>(result));
        }
        public IActionResult GetRoleList()
        {
            var result = _appRoleService.GetAll();
            return PartialView("_GetRoleTable", _mapper.Map<List<ListRoleDto>>(result));
        }
        public IActionResult GetCompanyBranchList()
        {
            ViewBag.Companies = new SelectList(_companyService.Get(p => p.IsActive == true), "CompanyId", "CompanyName");
            var result = _companyBranchService.Get(x => x.IsActive == true,null,"Company");
            var temp = _mapper.Map<List<ListCompanyBranchDto>>(result);  // ÖĞREN İÇ İÇE MAP
           

            return PartialView("_GetCompanyBranchTable", temp);
        }
        


    }
}
