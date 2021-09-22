using FlexHR.Business.Interface;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using FlexHR.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlexHR.Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class, new()
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

        public T AddResult(T table)
        {
            return _genericDal.AddResult(table);
        }

        public void Delete(int id)
        {
            _genericDal.Delete(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
           return  _genericDal.Get(filter,orderBy,includeProperties);
        }

        public List<T> GetAll()
        {
            return _genericDal.GetAll();
        }

        public T GetById(int id)
        {
            return _genericDal.GetById(id);
        }

        public void SaveChanges()
        {
            _genericDal.SaveChanges();
        }

        public void Update(T table)
        {
            _genericDal.Update(table);
        }

        public void UpdateNotSave(T table)
        {
            _genericDal.UpdateNotSave(table);
        }
    }
}
