namespace SoftUniFAQSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        private HashSet<Question> questions;
        private HashSet<Answer> answers;

        public ApplicationUser()
        {
            this.questions = new HashSet<Question>();
            this.answers = new HashSet<Answer>();
            this.DateOfRegister = DateTime.Now;
        }

        public string FullName { get; set; }

        public string SoftUniStudentNumber { get; set; }

        public DateTime DateOfRegister { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
