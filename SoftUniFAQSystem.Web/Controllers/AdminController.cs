namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Data.Contracts;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SoftUniFAQSystem.Models;
    using Constants = Web.Constants;

    [Authorize(Roles = "Achkov, Moderator")]
    public class AdminController : BaseApiController
    {
        private ApplicationUserManager userManager;

        public AdminController()
            : this(new SoftUniFaqSystemData(new ApplicationDbContext()))
        {
        }

        public AdminController(ISoftUniFAQSystemData data)
            : base(data)
        {
            this.userManager = new ApplicationUserManager(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllUsersByRole(string role)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (this.User.IsInRole("Achkov, Moderator"))
            {
                return this.Unauthorized();
            }

            var users = this.Data.Users.All().Include(u => u.Roles.Any(r => r.ToString() == role)).ToList();

            return this.Ok(users);
        }

        [HttpDelete]
        public IHttpActionResult DeleteApprovedQuestion(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (this.User.IsInRole("Achkov, Moderator"))
            {
                return this.Unauthorized();
            }

            var question = this.Data.Questions.GetById(id);
            if (question == null)
            {
                return this.BadRequest(Constants.NoSuchQuestion);
            }

            question.QuestionState = QuestionState.Inappropriate;
            this.Data.Questions.Update(question);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                message = "Question status successfully set to deleted!",
                QuestionId = question.Id
            });
        }

        [HttpPut]
        public IHttpActionResult ChangeUserRole(string userId, string role)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return this.BadRequest(Constants.NotLoggedOn);
            }

            if (this.User.IsInRole("Achkov, Moderator"))
            {
                return this.Unauthorized();
            }

            var user = this.Data.Users.GetById(userId);
            if (user == null)
            {
                return this.BadRequest(Constants.NoSuchUser);
            }

            this.UserManager.AddToRole(userId, role);
            this.Data.SaveChanges();

            return this.Ok(new
            {
                message = "User role changed successfully!",
                UserId = user.Id
            });
        }
    }
}