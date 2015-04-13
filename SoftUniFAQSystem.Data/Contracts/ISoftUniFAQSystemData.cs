namespace SoftUniFAQSystem.Data.Contracts
{
    using Models;

    public interface ISoftUniFAQSystemData
    {
        IUsersRepository Users { get; }

        IQuestionRepository Questions { get; }

        IAnswerRepository Answers { get; }

        int SaveChanges();
    }
}
