using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfStaffGeneralSubTypeRepository : EfGenericRepository<StaffGeneralSubType>, IStaffGeneralSubTypeDal
    {
        private readonly FlexHRContext _context;
        public EfStaffGeneralSubTypeRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }

        public List<StaffGeneralSubType> GetByStaffId(int id)
        {
            return _context.StaffGeneralSubType.Where(x => x.StaffId == id).ToList();
        }

        public string GetGeneralSubTypeByGeneralTypeId(int generalTypeId, List<StaffGeneralSubType> subTypes)
        {
            foreach (var item in subTypes)
            {
                switch (generalTypeId)
                {
                    case (int)GeneralTypeEnum.ContractType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.FileType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Department:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.ModeOfOperation:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Title:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.MaritalStatus:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Gender:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.DegreeOfDisability:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.BloodGroup:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.EducationStatus:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.EducationLevel:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.AccountType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.LeaveType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.GeneralStatus:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.PaymentType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.FeeType:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Currency:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Period:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.Debit:
                        return item.GeneralSubType.Description;
                    case (int)GeneralTypeEnum.EmailState:
                        return item.GeneralSubType.Description;

                    default:
                        return "";
                }

            }
            return "";

        }
    }

}
