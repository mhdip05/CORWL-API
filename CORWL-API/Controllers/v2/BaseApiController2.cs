using CORWL_API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    // [InheritedApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class BaseApiController2 : ControllerBase
    {

    }
}
