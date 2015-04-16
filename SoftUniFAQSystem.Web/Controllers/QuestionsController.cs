namespace SoftUniFAQSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Data.Contracts;
    using Models.Answers;
    using Models.Questions;
    using SoftUniFAQSystem.Models;
    using WebGrease.Css.Extensions;

    [RoutePrefix("api/questions")]
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
                DateOfOpen = q.DateOfOpen
                //NumberOfAnswers = q.Answers.Count()
            }));

            return Ok(bindedQuestions.OrderByDescending(q => q.DateOfOpen));
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
                NumberOfAnswers = question.Answers.Count()
            };

            return this.Ok(bindedQuestion);
        }

        [HttpGet]
        [Route("closed")]
        public IEnumerable<QuestionDataModel> GetAllClosed()
        {
            var questions = this.Data.Questions.GetAllClosed();
            var bindedQuestions = new List<QuestionDataModel>();

            foreach (var q in questions)
            {
                var answers = q.Answers.Select(a => new AnswerDataModel
                {
                    Id = a.Id, 
                    AnswerState = a.AnswerState, 
                    DateOfAnswered = a.DateOfAnswered, 
                    QuestionId = a.QuestionId, 
                    Text = a.Text, 
                    UserId = a.UserId, 
                    UpdatedOn = a.UpdatedOn
                }).ToList();

                bindedQuestions.Add(new QuestionDataModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    UserId = q.UserId,
                    QuestionState = q.QuestionState,
                    DateOfOpen = q.DateOfOpen,
                    Answers = answers
                });
            }

            return bindedQuestions;
        }
    }
}