namespace SoftUniFAQSystem.Web.Models.Answers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SoftUniFAQSystem.Models;

    public class AnswerBindingModels
    {
        [Required]
        [MaxLength(200, ErrorMessage = "The answer is bigger than 200 symbols.")]
        [MinLength(1, ErrorMessage = "The answer is smaller than 1 symbol")]
        public string Text { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime DateOfAnswered { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public AnswerState AnswerState { get; set; }
    }
}