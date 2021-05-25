using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffSalaryManager : GenericManager<StaffSalary>, IStaffSalaryService
    {
        public StaffSalaryManager(IStaffSalaryDal taffSalaryDal) : base(taffSalaryDal)
        {

        }
    }
  
}
