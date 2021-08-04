using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IGenericDal<T> where T : class, new()
    {
        void Add(T table);
        void Delete(int id);
        void Update(T table);
        T GetById(int id);
        List<T> GetAll();
        T AddResult(T table);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    }
}
