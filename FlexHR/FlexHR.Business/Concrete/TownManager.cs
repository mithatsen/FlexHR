using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;

namespace FlexHR.Business.Concrete
{
    public class TownManager : GenericManager<Town>, ITownService
    {
        public TownManager(ITownDal townDal) : base(townDal)
        {

        }
    }
  
}
