using FlexHR.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlexHR.DataAccess.Interface
{
    public interface IGenericDal<T> where T : class, ITable, new()
    {
        void Add(T table);
        void Delete(int id);
        void Update(T table);
        T GetById(int id);
        List<T> GetAll();


    }
}
