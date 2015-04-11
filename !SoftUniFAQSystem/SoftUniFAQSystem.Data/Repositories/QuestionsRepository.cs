namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Models;

    public class QuestionsRepository : GenericRepository<Question>
    {
        protected QuestionsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
