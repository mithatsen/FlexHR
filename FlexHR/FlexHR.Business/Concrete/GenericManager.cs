using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class, ITable, new()
    {

        private readonly IGenericDal<T> _genericDal;
        public GenericManager(IGenericDal<T> genericDal)
        {
            _genericDal = genericDal;
        }

        public void Add(T table)
        {
            _genericDal.Add(table);
        }

        public void Delete(int id)
        {
            _genericDal.Delete(id);
        }

        public List<T> GetAll()
        {
            return _genericDal.GetAll();
        }

        public T GetById(int id)
        {
            return _genericDal.GetById(id);
        }

        public void Update(T table)
        {
            _genericDal.Update(table);
        }
    }
}
