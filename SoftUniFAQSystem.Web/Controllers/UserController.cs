using SoftUniFAQSystem.Web.Models.Questions;

namespace SoftUniFAQSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Data.Contracts;

    using Models.Answers;
    using Models.Users;

    using SoftUniFAQSystem.Models;

    [Authorize]
    public class UserController : BaseApiController
    {
        public UserController()
            : this(new SoftUniFaqSystemData())
        {
        }

        public UserController(ISoftUniFAQSystemData data)
            : base(data)
        {
        }

        [HttpGet]
        [ActionName("usersDEMO")]
        public IEnumerable<UserDataModel> GetUsers()
        {
            var allUsers = this.Data.Users.All().Select(a => new
            {
                a.Id,
                Username = a.UserName,
                a.FullName,
                a.SoftUniStudentNumber,
                a.DateOfRegister
            }).ToList();

            var bindedUsers = new List<UserDataModel>();
            allUsers.ForEach(u => bindedUsers.Add(new UserDataModel
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                SoftUniStudentNumber = u.SoftUniStudentNumber,
                DateOfRegister = u.DateOfRegister
            }));


            return bindedUsers;
        }

        [HttpGet]
        public IHttpActionResult GetUserById(string id)
        {
            var user = this.Data.Users.GetById(id);
            if (user == null)
            {
                return this.BadRequest(Constants.NoSuchUser);
            }

            var bindedUser = new UserDataModel
            {
                Id = user.Id,
                Username = user.UserName,
                FullName = user.FullName,
                SoftUniStudentNumber = user.SoftUniStudentNumber,
                DateOfRegister = user.DateOfRegister,
                Answers = user.Answers,
                Questions = user.Questions
            };

            return this.Ok(bindedUser);
        }

        [HttpPost]
        public IHttpActionResult PostNewQuestion(QuestionBindingModel question)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var questionToAdd = new Question
            {
                Title = question.Title,
                UserId = question.UserId
            };

            this.Data.Questions.Add(questionToAdd);
            this.Data.Questions.SaveChanges();

            return this.Created(new Uri(Url.Link("DefaultApi", new { id = questionToAdd.Id })), questionToAdd);
        }

        [HttpPut]
        public IHttpActionResult UpdateQuestion(Guid id, QuestionBindingModel question)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionToUpdate = this.Data.Questions
                .All()
                .FirstOrDefault(q => q.Id == id);
            if (questionToUpdate == null)
            {
                return BadRequest(Constants.NoSuchQuestion);
            }

            questionToUpdate.Title = question.Title;
            if (question.QuestionState != 0)
            {
                questionToUpdate.QuestionState = question.QuestionState;
            }
            this.Data.Questions.SaveChanges();

            return this.Ok(new
            {
                message = "Question updated successfully!",
                QuestionId = questionToUpdate.Id
            });
        }

        [HttpDelete]
        public IHttpActionResult DeleteQuestion(Guid id)
        {
            var questionToDelete = this.Data.Questions.All().FirstOrDefault(q => q.Id == id);
            if (questionToDelete == null)
            {
                return BadRequest(Constants.NoSuchQuestion);
            }

            this.Data.Questions.Delete(questionToDelete);
            this.Data.Questions.SaveChanges();

            return Ok(id);
        }

        [HttpPost]
        public IHttpActionResult PostNewAnswer(AnswerBindingModels model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var answer = new Answer()
            {
                Text = model.Text,
                UserId = model.UserId,
                AnswerState = AnswerState.Good,
                DateOfAnswered = DateTime.Now
            };

            this.Data.Answers.Add(answer);
            this.Data.SaveChanges();

            return this.Created(new Uri(Url.Link("DefaultApi", new {id = answer.Id})), answer);
        }

        [HttpPut]
        public IHttpActionResult UpdateAnswer(int id, AnswerBindingModels model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.BadRequest(Constants.NoSuchAnswer);
            }

            answer.Text = model.Text;
            answer.UpdatedOn = DateTime.Now;

            this.Data.Answers.Update(answer);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                message = "Answer updated successfully!",
                AnswerId = answer.Id
            });
        }
    }
}