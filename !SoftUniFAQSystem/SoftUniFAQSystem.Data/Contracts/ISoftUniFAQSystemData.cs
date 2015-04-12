namespace SoftUniFAQSystem.Data.Contracts
{
    using Models;

    public interface ISoftUniFAQSystemData
    {
        IUsersRepository Users { get; }

        IRepository<Question> Questions { get; }

        IRepository<Answer> Answers { get; }

        int SaveChanges();
    }
}
