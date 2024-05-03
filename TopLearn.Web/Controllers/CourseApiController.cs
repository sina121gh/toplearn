using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Context;

namespace TopLearn.Web.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly TopLearnContext _context;

        public CourseApiController(TopLearnContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitles = await _context.Courses
                    .Where(c => c.Title.Contains(term))
                    .Select(c => c.Title).ToListAsync();

                return Ok(courseTitles);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
