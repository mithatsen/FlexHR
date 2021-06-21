using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfGenericRepository<T> : IGenericDal<T> where T : class, ITable, new()
    {
        private readonly FlexHRContext _context;
        public EfGenericRepository(FlexHRContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            var temp = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(temp);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
           
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Add(T table)
        {

            _context.Set<T>().Add(table);
            _context.SaveChanges();


        }
        public T AddResult(T table)
        {

            _context.Set<T>().Add(table);
            _context.SaveChanges();
            return table;


        }

        public void Update(T table)
        {
            _context.Set<T>().Update(table);
            _context.SaveChanges();
        }
    }
}
