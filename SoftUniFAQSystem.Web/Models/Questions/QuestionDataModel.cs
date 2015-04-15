namespace SoftUniFAQSystem.Web.Models.Questions
{
    using System;
    using System.Collections.Generic;
    using SoftUniFAQSystem.Models;

    public class QuestionDataModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }

        public QuestionState QuestionState { get; set; }

        public DateTime DateOfOpen { get; set; }

        public int NumberOfBestAnswers { get; set; }

        public int? NumberOfAnswers { get; set; }
    }
}