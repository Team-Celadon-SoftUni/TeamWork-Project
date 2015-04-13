namespace SoftUniFAQSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Web.Http;
    using Data;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.Answers;
    using Models.Questions;
    using Models.Users;
    using SoftUniFAQSystem.Models;
    using Constants = Web.Constants;

    //[Authorize]
    [RoutePrefix("api/user")]
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
        [Route("question")]
        public IHttpActionResult PostNewQuestion(QuestionBindingModel question)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            var currentUserId = this.User.Identity.GetUserId();
            var user = this.Data.Users.GetById(currentUserId);
            if (user == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            var questionToAdd = new Question
            {
                Title = question.Title,
                UserId = user.Id
            };
            question.UserId = user.Id;

            this.Data.Questions.Add(questionToAdd);
            this.Data.Questions.SaveChanges();

            return this.Created(new Uri(Url.Link("DefaultApi", new { controller = "Questions", id = questionToAdd.Id })), question);
        }

        [HttpPut]
        [Route("question")]
        public IHttpActionResult UpdateQuestion(int id, QuestionBindingModel question)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionToUpdate = this.Data.Questions.GetById(id);
            if (questionToUpdate == null)
            {
                return BadRequest(Constants.NoSuchQuestion);
            }

            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (questionToUpdate.UserId != currentUserId)
            {
                return this.Unauthorized();
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
        [Route("question")]
        public IHttpActionResult DeleteQuestion(int id)
        {
            var questionToDelete = this.Data.Questions.GetById(id);
            if (questionToDelete == null)
            {
                return BadRequest(Constants.NoSuchQuestion);
            }

            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (questionToDelete.UserId != currentUserId)
            {
                return this.Unauthorized();
            }

            this.Data.Questions.Delete(questionToDelete);
            this.Data.Questions.SaveChanges();

            return Ok(id);
        }

        [HttpPost]
        [Route("answer")]
        public IHttpActionResult PostNewAnswer(int questionId, [FromBody]AnswerBindingModels model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var currentUserId = this.User.Identity.GetUserId();
            var user = this.Data.Users.GetById(currentUserId);
            if (user == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            var question = this.Data.Questions.GetById(questionId);
            if (question == null)
            {
                return this.BadRequest(Constants.NoSuchQuestion);
            }

            var answer = new Answer()
            {
                Text = model.Text,
                UserId = user.Id,
                AnswerState = AnswerState.Good,
                DateOfAnswered = DateTime.Now,
                QuestionId = questionId
            };
            model.UserId = user.Id;

            this.Data.Answers.Add(answer);
            this.Data.SaveChanges();

            return this.Created(new Uri(Url.Link("DefaultApi", new {controller = "Answer", id = answer.Id})), model);
        }

        [HttpPut]
        [Route("answer")]
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
            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (answer.UserId != currentUserId)
            {
                return this.Unauthorized();
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

        [HttpDelete]
        [Route("answer")]
        public IHttpActionResult DeleteAnswer(int id)
        {
            var answer = this.Data.Answers.GetById(id);
            if (answer == null)
            {
                return this.BadRequest(Constants.NoSuchAnswer);
            }

            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (answer.UserId != currentUserId)
            {
                return this.Unauthorized();
            }

            this.Data.Answers.Delete(answer);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                message = "Answer deleted successfully!",
                AnswerId = answer.Id
            });
        }
    }
}