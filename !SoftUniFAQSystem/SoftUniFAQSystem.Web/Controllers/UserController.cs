namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Models;

    public class UserController : BaseApiController
    {
        [HttpGet]
        [ActionName("usersDEMO")]
        public IHttpActionResult GetUsers(UserBindingModel user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
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