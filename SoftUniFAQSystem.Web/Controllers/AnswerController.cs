namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Data.Contracts;

    using Models.Answers;

    public class AnswerController : BaseApiController
    {
        public AnswerController()
            : this(new SoftUniFaqSystemData())
        {
        }

        public AnswerController(ISoftUniFAQSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        public IEnumerable<AnswerDataModel> GetAllAnswers()
        {
            var answers = this.Data.Answers.All().ToList();
            var bindedAnswers = new List<AnswerDataModel>();
            answers.ForEach(a => bindedAnswers.Add(new AnswerDataModel
            {
                Id = a.Id,
                Text = a.Text,
                UserId = a.UserId,
                AnswerState = a.AnswerState,
                DateOfAnswered = a.DateOfAnswered
            }));

            return bindedAnswers;
        }

        [HttpGet]
        public IHttpActionResult GetAnswerById(int id)
        {
            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.BadRequest("Couldn't find answer with such id. Please try again.");
            }
            var bindedAnswer = new AnswerDataModel
            {
                Id = answer.Id,
                Text = answer.Text,
                UserId = answer.UserId,
                AnswerState = answer.AnswerState,
                DateOfAnswered = answer.DateOfAnswered,
                User = answer.User
            };

            return this.Ok(bindedAnswer);
        }
    }
}