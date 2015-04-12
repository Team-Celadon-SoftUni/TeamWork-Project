namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Models;

    public class AnswersRepository : GenericRepository<Answer>
    {
        public AnswersRepository(DbContext context)
            : base(context)
        {
        }
    }
}
