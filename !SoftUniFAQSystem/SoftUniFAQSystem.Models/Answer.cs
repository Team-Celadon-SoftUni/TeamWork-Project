namespace SoftUniFAQSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        public Answer()
        {
            this.Id = Guid.NewGuid();
            this.DateOfAnswered = DateTime.Now;
            this.AnswerState = AnswerState.Good;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "The answer is bigger than 200 symbols.")]
        [MinLength(1, ErrorMessage = "The answer is smaller than 1 symbol")]
        public string Title { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public DateTime DateOfAnswered { get; set; }

        public AnswerState AnswerState { get; set; }
    }
}