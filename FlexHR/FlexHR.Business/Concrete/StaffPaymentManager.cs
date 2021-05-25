using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class StaffPaymentManager : GenericManager<StaffPayment>, IStaffPaymentService
    {
        public StaffPaymentManager(IStaffPaymentDal staffPaymentDal) : base(staffPaymentDal)
        {

        }
    }
  
}
