namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Web.Http;
    using Data;

    public class BaseApiController : ApiController
    {
        protected BaseApiController()
            : this(new SoftUniFaqSystemData(new ApplicationDbContext()))
        {
        }

        protected BaseApiController(SoftUniFaqSystemData data)
        {
            this.Data = data;
        }

        public SoftUniFaqSystemData Data { get; set; }
    }
}