namespace SoftUniFAQSystem.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts;

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly IDbSet<T> set;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IDbSet<T> Set
        {
            get { return this.set; }
        }

        public ICollection<T> Find(Expression<Func<T, bool>> expression)
        {
            return this.Set.Where(expression).ToList();
        }

        public virtual T GetById(object id)
        {
            return this.Set.Find(id);
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public T Get(object id)
        {
            return this.set.Find(id);
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public T Update(object id)
        {
            var entity = this.Get(id);
            this.Update(entity);
            return entity;
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public T Delete(object id)
        {
            var entity = this.Get(id);
            this.Delete(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
