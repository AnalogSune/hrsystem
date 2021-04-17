using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public int RetrieveUserId() => int.Parse(User.Claims.FirstOrDefault().Value);
    }
}