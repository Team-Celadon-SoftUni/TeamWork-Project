namespace SoftUniFAQSystem.Web.Models.Users
{
    using System;
    using System.Collections.Generic;
    using SoftUniFAQSystem.Models;

    public class UserDataModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string SoftUniStudentNumber { get; set; }

        public DateTime DateOfRegister { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}