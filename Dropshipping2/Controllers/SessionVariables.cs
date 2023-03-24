using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dropshipping2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionVariables : ControllerBase
    {
        public const string SessionID = "session id";
    }
}
