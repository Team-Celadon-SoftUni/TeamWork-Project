namespace SoftUniFAQSystem.Data
{
    using System.Data.Entity;
    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, ISoftUniFAQSystemDbContext
    {
        public ApplicationDbContext()
            : base("FAQSystem", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Question>().HasRequired(q => q.User).WithMany(u => u.Questions).WillCascadeOnDelete(false);
            modelBuilder.Entity<Answer>().HasRequired(a => a.Question).WithMany(q => q.Answers).WillCascadeOnDelete(false);
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
