namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Models;

    public class QuestionsRepository : GenericRepository<Question>
    {
        public QuestionsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
