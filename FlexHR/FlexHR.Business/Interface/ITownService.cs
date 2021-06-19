using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface ITownService : IGenericService<Town>
    {
        public List<Town> GetTownListByCityId(int id);
    }
}
