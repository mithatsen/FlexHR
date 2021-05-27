using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IGenericService<T> where T : class, new()
    {
        void Add(T table);
        void Delete(int id);
        void Update(T table);
        T GetById(int id);
        List<T> GetAll();

    }
}
