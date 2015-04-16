namespace SoftUniFAQSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Data.Contracts;

    using Microsoft.AspNet.Identity;

    using Models.Answers;
    using Models.Questions;
    using Models.Users;

    using SoftUniFAQSystem.Models;
    using WebGrease.Css.Extensions;
    using Constants = Web.Constants;

    [Authorize]
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
            var answers = new List<AnswerDataModel>();
            user.Answers.ForEach(a => answers.Add(new AnswerDataModel
            {
                Id = a.Id,
                AnswerState = a.AnswerState,
                DateOfAnswered = a.DateOfAnswered,
                QuestionId = a.QuestionId,
                Text = a.Text,
                UserId = a.UserId,
                UpdatedOn = a.UpdatedOn
            }));

            var questions = new List<QuestionDataModel>();
            user.Questions.ForEach(q => questions.Add(new QuestionDataModel
            {
                Id = q.Id,
                Title = q.Title,
                QuestionState = q.QuestionState,
                DateOfOpen = q.DateOfOpen,
                NumberOfAnswers = q.Answers.Count(),
                UserId = q.UserId
            }));

            var bindedUser = new UserDataModel
            {
                Id = user.Id,
                Username = user.UserName,
                FullName = user.FullName,
                SoftUniStudentNumber = user.SoftUniStudentNumber,
                DateOfRegister = user.DateOfRegister,
                Answers = answers,
                Questions = questions
            };

            return this.Ok(bindedUser);
        }

        [HttpGet]
        [Route("question")]
        public IHttpActionResult GetUserQuestions()
        {
            var currentUserId = this.User.Identity.GetUserId();
            var user = this.Data.Users.GetById(currentUserId);
            if (user == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            var questions = Data.Questions.All()
                .Where(q => q.UserId == currentUserId)
                .Select(q => new QuestionDataModel
                {
                    Id = q.Id,
                    DateOfOpen = q.DateOfOpen,
                    QuestionState = q.QuestionState,
                    Title = q.Title,
                    UserId = q.UserId
                });

            return Ok(questions);
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
                UserId = user.Id,
                DateOfOpen = DateTime.Now,
                QuestionState = QuestionState.Active,
                NumberOfBestAnswers = 0
            };
            question.UserId = user.Id;

            this.Data.Questions.Add(questionToAdd);
            this.Data.Questions.SaveChanges();

            return this.Created(new Uri(Url.Link("DefaultApi", new {controller = "Questions", id = questionToAdd.Id})),
                question);
        }

        [HttpPut]
        [Route("question")]
        public IHttpActionResult UpdateQuestion(int id, QuestionBindingModel model)
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

            questionToUpdate.Title = model.Title;
            if (model.QuestionState != null)
            {
                if (model.QuestionState == QuestionState.Closed)
                {
                    var bestAnswers =
                        questionToUpdate.Answers.Count(a => a.AnswerState == AnswerState.Best || a.AnswerState == AnswerState.SecondaryBest);
                    if (bestAnswers == 0)
                    {
                        return this.BadRequest(Constants.CannotClose);
                    }
                }

                questionToUpdate.QuestionState = model.QuestionState;
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

            // Inappropriate is the state of a "deleted" question. There won't be any actual deleting in the Db.
            questionToDelete.QuestionState = QuestionState.Inappropriate;

            //this.Data.Questions.Delete(questionToDelete);
            this.Data.Questions.Update(questionToDelete);
            this.Data.Questions.SaveChanges();

            return Ok(id);
        }

        [HttpPost]
        [Route("answer")]
        public IHttpActionResult PostNewAnswer(int questionId, [FromBody] AnswerBindingModels model)
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

            var answer = new Answer
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

            if (model.AnswerState != null)
            {
                if (answer.AnswerState != AnswerState.Best || answer.AnswerState != AnswerState.SecondaryBest)
                {
                    if ((model.AnswerState == AnswerState.Best) || (model.AnswerState == AnswerState.SecondaryBest))
                    {
                        var question = this.Data.Questions.GetById(answer.QuestionId);
                        if (question.NumberOfBestAnswers >= 2)
                        {
                            return this.BadRequest(Constants.BestAnswersLimitReached);
                        }

                        question.NumberOfBestAnswers++;
                        this.Data.Questions.Update(question);
                    }
                }
               
                answer.AnswerState = model.AnswerState;
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

            // Inappropriate is the state of a "deleted" answer. There won't be any actual deleting in the Db.
            answer.AnswerState = AnswerState.Inappropriate;

            //this.Data.Answers.Delete(answer);
            this.Data.Answers.Update(answer);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                message = "Answer deleted successfully!",
                AnswerId = answer.Id
            });
        }
    }
}