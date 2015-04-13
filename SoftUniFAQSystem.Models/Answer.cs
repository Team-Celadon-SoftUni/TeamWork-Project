namespace SoftUniFAQSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Permissions;

    public class Answer
    {
        public Answer()
        {
            // this.Id = Guid.NewGuid();
            this.DateOfAnswered = DateTime.Now;
            this.AnswerState = AnswerState.Good;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The answer is bigger than 200 symbols.")]
        [MinLength(1, ErrorMessage = "The answer is smaller than 1 symbol")]
        public string Text { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public DateTime DateOfAnswered { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public AnswerState AnswerState { get; set; }
    }
}