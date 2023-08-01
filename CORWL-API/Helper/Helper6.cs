using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Helper
{
    
    
    [InheritedApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class Helper6 : ControllerBase
    {
      
    }
}
