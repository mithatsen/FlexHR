using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.Business.Interface
{
    public interface IGenericService<T> where T : class, new()
    {
        void Save(T work);
        void Delete(int id);
        void Update(T param);
        T GetWithId(int id);
        List<T> GetAll();

    }
}
