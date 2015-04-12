namespace SoftUniFAQSystem.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
    {
        ICollection<T> Find(Expression<Func<T, bool>> expression);

        T GetById(object id);

        //Create
        void Add(T entity);

        //Read
        T Get(object id);
        IQueryable<T> All();

        //Update
        void Update(T entity);
        T Update(object id);

        //Delete
        T Delete(object id);
        void Delete(T entity);

        //SaveChanges
        int SaveChanges();
    }
}
