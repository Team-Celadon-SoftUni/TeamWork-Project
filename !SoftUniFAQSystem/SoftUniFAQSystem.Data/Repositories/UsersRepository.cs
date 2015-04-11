namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using Models;

    public class UsersRepository : GenericRepository<ApplicationUser>
    {
        public UsersRepository(DbContext context)
            : base(context)
        {
        }
    }
}
