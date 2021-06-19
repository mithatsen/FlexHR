using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface ITownDal : IGenericDal<Town>
    {
        public List<Town> GetTownListByCityId(int id);
    }
}
