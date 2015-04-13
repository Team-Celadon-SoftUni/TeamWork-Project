namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    public class AnswersRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswersRepository(ISoftUniFAQSystemDbContext context)
            : base(context)
        {
        }

        public ICollection<Answer> GetAllByQuestionId(int questionId)
        {
            return this.Set.Where(a => a.QuestionId == questionId).ToList();
        }
    }
}