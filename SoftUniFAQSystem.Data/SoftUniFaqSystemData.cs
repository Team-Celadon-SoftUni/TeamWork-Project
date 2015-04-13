namespace SoftUniFAQSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Contracts;
    using Models;
    using Repositories;

    public class SoftUniFaqSystemData : ISoftUniFAQSystemData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public SoftUniFaqSystemData()
            : this(new ApplicationDbContext())
        {
        }

        public SoftUniFaqSystemData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IUsersRepository Users
        {
            get { return (IUsersRepository)this.GetRepository<ApplicationUser>(); }
        }

        public IQuestionRepository Questions
        {
            get { return (QuestionsRepository)this.GetRepository<Question>(); }
        }

        public IAnswerRepository Answers
        {
            get { return (AnswersRepository)this.GetRepository<Answer>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepo = typeof(GenericRepository<T>);
                if (type.IsAssignableFrom(typeof(ApplicationUser)))
                {
                    typeOfRepo = typeof(UsersRepository);
                }
                else if (type.IsAssignableFrom(typeof(Question)))
                {
                    typeOfRepo = typeof(QuestionsRepository);
                }
                else if (type.IsAssignableFrom(typeof(Answer)))
                {
                    typeOfRepo = typeof(AnswersRepository);
                }

                var repo = Activator.CreateInstance(typeOfRepo, this.context);
                this.repositories.Add(type, repo);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
