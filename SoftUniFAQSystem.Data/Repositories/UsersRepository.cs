namespace SoftUniFAQSystem.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using Contracts;
    using Models;

    public class UsersRepository : GenericRepository<ApplicationUser>, IUsersRepository
    {
        public UsersRepository(DbContext context)
            : base(context)
        {
        }

        public override ApplicationUser GetById(object id)
        {
            return this.Set
                .Include(u => u.Answers)
                .Include(u => u.Questions)
                .FirstOrDefault(u => u.Id == (string)id);
        }

        public bool CheckEmailUniqueness(string email)
        {
            ApplicationUser matchingUsersWithThisEmail = this.Find(u => u.Email == email).FirstOrDefault();
            if (matchingUsersWithThisEmail == null)
            {
                return true;
            }

            return false;
        }

        public bool CheckUsernameUniqueness(string username)
        {
            ApplicationUser matchingUsersWithThiUsername = this.Find(u => u.UserName == username).FirstOrDefault();
            if (matchingUsersWithThiUsername == null)
            {
                return true;
            }

            return false;
        }
    }
}
