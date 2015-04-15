namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Contracts;
    using Models;

    public class QuestionsRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionsRepository(ISoftUniFAQSystemDbContext context)
            : base(context)
        {
        }

        public ICollection<Question> GetAllByStatus(QuestionState state)
        {
            return this.Set
                .Include(q => q.Answers)
                .Where(q => q.QuestionState == state)
                .ToList();
        }

        public ICollection<Question> GetAllClosed()
        {
            return this.Set
                       .Include(q => q.Answers)
                       .Where(q => q.QuestionState == QuestionState.Closed && 
                           q.Answers.Any(a => a.AnswerState == AnswerState.Best || 
                               a.AnswerState == AnswerState.SecondaryBest))
                       .ToList();
        }

        public ICollection<Question> GetAllClosedWithBestAnswers()
        {
            return this.Set
                       .Include(q => q.Answers)
                       .Where(q => q.NumberOfBestAnswers > 0 && q.QuestionState == QuestionState.Closed)
                       .ToList();
        }
    }
}
