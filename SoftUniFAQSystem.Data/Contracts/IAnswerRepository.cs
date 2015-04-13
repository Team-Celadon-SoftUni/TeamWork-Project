namespace SoftUniFAQSystem.Data.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IAnswerRepository : IRepository<Answer>
    {
        ICollection<Answer> GetAllByQuestionId(int id);
    }
}