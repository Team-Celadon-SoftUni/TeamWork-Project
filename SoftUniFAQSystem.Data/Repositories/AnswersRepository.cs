namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public override Answer GetById(object id)
        {
            return this.Set.Include(a => a.User).FirstOrDefault(a => a.Id == (int)id);
        }
    }
}