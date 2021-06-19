using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System.Collections.Generic;

namespace FlexHR.Business.Concrete
{
    public class TownManager : GenericManager<Town>, ITownService
    {
        private readonly ITownDal _townDal;

        public TownManager(ITownDal townDal) : base(townDal)
        {
            _townDal = townDal;
        }

        public List<Town> GetTownListByCityId(int id)
        {
            return _townDal.GetTownListByCityId(id);
        }
    }
  
}
