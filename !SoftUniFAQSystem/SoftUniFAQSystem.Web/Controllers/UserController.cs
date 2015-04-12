namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    public class UserController : BaseApiController
    {
        [HttpGet]
        [ActionName("usersDEMO")]
        public IHttpActionResult GetUsers(UserBindingModel userR)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("The User has invalid state");
            }

            var allUsers = this.Data.Users.All().ToList();

            return this.Ok(allUsers.Select(a => new
            {
                Username = a.UserName,
                FullName = a.FullName,
                SoftUniStudentNumber = a.SoftUniStudentNumber,
                DateOfRegister = a.DateOfRegister
            }));
        }
    }
}