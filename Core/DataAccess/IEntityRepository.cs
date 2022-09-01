using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // Generic constraint 
    // class = reference type
    // IEntity = IEntity olabilir ve IEntity implemente eden bir nesne olabilir
    // new() = new'lenebilir olmalı 
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        // filtre zorunlu değil
        List<T> GetAll(Expression<Func<T, bool>> filter = null);               // Filtreleme için ayrı ayrı metodlar yazmamak için kullanırız.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
