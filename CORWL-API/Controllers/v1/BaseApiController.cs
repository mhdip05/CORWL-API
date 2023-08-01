using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BaseApiController : ControllerBase
    {

    }
}
