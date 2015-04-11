namespace SoftUniFAQSystem.Data
{
    using Models;
    using Repositories;

    public interface ISoftUniFAQSystemData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Question> Questions { get; }

        IRepository<Answer> Answers { get; }

        int SaveChanges();
    }
}
