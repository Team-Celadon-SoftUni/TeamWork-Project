namespace SoftUniFAQSystem.Data.Contracts
{
    using Models;

    public interface IUsersRepository : IRepository<ApplicationUser>
    {
        bool CheckEmailUniqueness(string email);

        bool CheckUsernameUniqueness(string username);
    }
}
