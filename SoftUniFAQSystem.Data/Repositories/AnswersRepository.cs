namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Contracts;
    using Models;

    public class AnswersRepository : GenericRepository<Answer>
    {
        public AnswersRepository(ISoftUniFAQSystemDbContext context)
            : base(context)
        {
        }
    }
}
