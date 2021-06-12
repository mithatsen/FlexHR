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

        public System.Collections.Specialized.NameValueCollection GetGeneralSubTypeByStaffGeneralSubTypeList(List<StaffGeneralSubType> subTypes)
        {
            System.Collections.Specialized.NameValueCollection generalTypeArray = new System.Collections.Specialized.NameValueCollection();

            //System.Collections.Specialized.NameValueCollection k = new System.Collections.Specialized.NameValueCollection();
            //k.Add("B", "Brown");
            //k.Add("G", "Green");
            //var x = k[0];


            for (int i = 0; i < subTypes.Count; i++)
            {
                var generalTypeId = subTypes[i].GeneralSubType.GeneralTypeId;
                var generalSubTypeDesc = subTypes[i].GeneralSubType.Description;
                switch (generalTypeId)
                {
                    case (int)GeneralTypeEnum.ContractType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;

                    case (int)GeneralTypeEnum.FileType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                 
                    case (int)GeneralTypeEnum.Department:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.ModeOfOperation:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.Title:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.MaritalStatus:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.Gender:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.DegreeOfDisability:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.BloodGroup:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.EducationStatus:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.EducationLevel:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.AccountType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.LeaveType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.GeneralStatus:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.PaymentType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.FeeType:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.Currency:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.Period:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.Debit:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        break;
                    case (int)GeneralTypeEnum.EmailState:
                        generalTypeArray.Add(generalTypeId.ToString(), generalSubTypeDesc);
                        //generalTypeArray[i, 0] = generalTypeId.ToString();
                        //generalTypeArray[i, 1] = generalSubTypeDesc;
                        break;

                    default:
                        break;
                }
            }

            return generalTypeArray;

        }
    }

}
