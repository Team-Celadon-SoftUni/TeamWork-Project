namespace SoftUniFAQSystem.Web.Models.Answers
{
    using System;

    using SoftUniFAQSystem.Models;

    public class AnswerDataModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public DateTime DateOfAnswered { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public AnswerState AnswerState { get; set; }
    }
}