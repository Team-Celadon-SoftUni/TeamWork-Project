using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniFAQSystem.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;

    public interface ISoftUniFAQSystemDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<Answer> Answers { get; set; }
 
        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
