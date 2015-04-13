namespace SoftUniFAQSystem.Web.Controllers
{
    using System.Web.Http;

    using Data;
    using Data.Contracts;

    public class BaseApiController : ApiController
    {
        protected BaseApiController()
            : this(new SoftUniFaqSystemData())
        {
        }

        protected BaseApiController(ISoftUniFAQSystemData data)
        {
            this.Data = data;
        }

        public ISoftUniFAQSystemData Data { get; private set; }
    }
}