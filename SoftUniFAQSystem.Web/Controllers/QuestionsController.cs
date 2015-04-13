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
    using WebGrease.Css.Extensions;

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
            var bindedQuestions = new List<QuestionDataModel>();
            questions.ForEach(q => bindedQuestions.Add(new QuestionDataModel
            {
                Id = q.Id,
                Title = q.Title,
                UserId = q.UserId,
                QuestionState = q.QuestionState,
                DateOfOpen = q.DateOfOpen,
                //NumberOfAnswers = q.Answers.Count()
            }));

            return Ok(bindedQuestions);
        }

        [HttpGet]
        public IEnumerable<QuestionDataModel> GetAllByStatus(string state)
        {
            QuestionState questionState;
            try
            {
                Enum.TryParse(state, true, out questionState);
            }
            catch (ArgumentException)
            {
                
                return null;
            }

            var questions = this.Data.Questions.GetAllByStatus(questionState);
            var bindedQuestions = new List<QuestionDataModel>();
            questions.ForEach(q => bindedQuestions.Add(new QuestionDataModel
            {
                Id = q.Id,
                Title = q.Title,
                QuestionState = q.QuestionState,
                DateOfOpen = q.DateOfOpen,
                NumberOfAnswers = q.Answers.Count(),
                UserId = q.UserId
            }));

            return bindedQuestions;
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var question = this.Data.Questions.GetById(id);
            if (question == null)
            {
                return this.BadRequest(Constants.NoSuchQuestion);
            }
            var bindedQuestion = new QuestionDataModel
            {
                Id = question.Id,
                Title = question.Title,
                UserId = question.UserId,
                QuestionState = question.QuestionState,
                DateOfOpen = question.DateOfOpen,
            };

            return this.Ok(bindedQuestion);
        }
    }
}