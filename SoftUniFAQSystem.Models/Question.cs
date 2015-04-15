namespace SoftUniFAQSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Question
    {
        private HashSet<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
            //this.QuestionState = QuestionState.Active;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "The question is bigger than 150 symbols.")]
        [MinLength(10, ErrorMessage = "The answer is smaller than 10 symbols")]
        public string Title { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public QuestionState QuestionState { get; set; }

        public DateTime DateOfOpen { get; set; }

        public int NumberOfBestAnswers { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}