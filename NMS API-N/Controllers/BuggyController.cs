using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMS_API_N.DbContext;
using NMS_API_N.Model.Entities;

namespace NMS_API_N.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var data = _context.Users.Find(-1);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var data = _context.Users.Find(-1);

            var thingsToReturn = data.ToString();

            return thingsToReturn;
        }


        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
