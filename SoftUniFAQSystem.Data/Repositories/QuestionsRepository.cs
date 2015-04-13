namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Contracts;
    using Models;

    public class QuestionsRepository : GenericRepository<Question>
    {
        public QuestionsRepository(ISoftUniFAQSystemDbContext context)
            : base(context)
        {
        }
    }
}
