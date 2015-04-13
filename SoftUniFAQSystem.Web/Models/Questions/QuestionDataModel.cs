namespace SoftUniFAQSystem.Web.Models.Questions
{
    using System;
    using System.Collections.Generic;
    using SoftUniFAQSystem.Models;

    public class QuestionDataModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public QuestionState QuestionState { get; set; }

        public DateTime DateOfOpen { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}