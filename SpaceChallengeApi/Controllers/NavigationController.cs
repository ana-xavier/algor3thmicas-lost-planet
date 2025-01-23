using Microsoft.AspNetCore.Mvc;
using SpaceChallengeApi.Services;

namespace SpaceChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly SpaceNavigator _spaceNavigator;

        public NavigationController(SpaceNavigator spaceNavigator)
        {
            _spaceNavigator = spaceNavigator;
        }

        [HttpGet("navigate")]
        public IActionResult GetShortestPath()
        {
            var path = _spaceNavigator.FindShortestPath();
            if (path.Count == 0)
            {
                return NotFound("No path found.");
            }
            return Ok(path);
        }
    }
}
