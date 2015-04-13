namespace SoftUniFAQSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SoftUniFAQSystem.Data;
    using SoftUniFAQSystem.Data.Contracts;
    using SoftUniFAQSystem.Web.Models.Answers;
    using System.Web.Http;
    using SoftUniFAQSystem.Models;
    using SoftUniFAQSystem.Web.Models.Questions;

    public class QuestionsController : BaseApiController
    {
        public QuestionsController()
            : this(new SoftUniFaqSystemData())
        {
        }

        public QuestionsController(ISoftUniFAQSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var questions = this.Data.Questions.All().ToList();
            var bindedQuestions = new List<QuestionBindingModel>();
            questions.ForEach(q => bindedQuestions.Add(new QuestionBindingModel
            {
                Title = q.Title,
                UserId = q.UserId,
                QuestionState = q.QuestionState,
                DateOfOpen = q.DateOfOpen
            }));

            return Ok(bindedQuestions);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            var question = this.Data.Questions.All().FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return this.BadRequest(Constants.NoSuchQuestion);
            }
            var bindedQuestion = new QuestionBindingModel
            {
                Title = question.Title,
                UserId = question.UserId,
                QuestionState = question.QuestionState,
                DateOfOpen = question.DateOfOpen,
            };

            return this.Ok(bindedQuestion);
        }
    }
}