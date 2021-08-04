using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class StaffCareerManager : GenericManager<StaffCareer>, IStaffCareerService
    {
        private readonly IStaffCareerDal _staffCareerDal;
        public StaffCareerManager(IStaffCareerDal staffCareerDal) : base(staffCareerDal)
        {
            _staffCareerDal = staffCareerDal;
        }

       
    }
}
