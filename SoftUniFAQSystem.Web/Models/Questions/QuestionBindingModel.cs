using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftUniFAQSystem.Models;

namespace SoftUniFAQSystem.Web.Models.Questions
{
    public class QuestionBindingModel
    {
        [Required]
        [MaxLength(150, ErrorMessage = "The question is bigger than 150 symbols.")]
        [MinLength(10, ErrorMessage = "The answer is smaller than 10 symbols")]
        public string Title { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public QuestionState QuestionState { get; set; }

        public DateTime DateOfOpen { get; set; }
    }
}