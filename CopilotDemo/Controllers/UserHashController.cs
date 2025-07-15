using Microsoft.AspNetCore.Mvc;

namespace CopilotDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserHashController : ControllerBase
    {
        private readonly UserHashService _userHashService;

        public UserHashController(UserHashService userHashService)
        {
            _userHashService = userHashService;
        }

        // GET: api/UserHash/{uid}
        [HttpGet("{uid}")]
        public async Task<ActionResult<UserHash>> GetUserHash(string uid)
        {
            var userHash = await _userHashService.GetUserHashAsync(uid);
            if (userHash == null)
            {
                return NotFound();
            }
            return Ok(userHash);
        }
    }
}