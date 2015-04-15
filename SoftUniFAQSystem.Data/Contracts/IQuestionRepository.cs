namespace SoftUniFAQSystem.Data.Contracts
{
    using System.Collections.Generic;

    using Models;

    public interface IQuestionRepository : IRepository<Question>
    {
        ICollection<Question> GetAllByStatus(QuestionState state);

        ICollection<Question> GetAllClosed();
    }
}