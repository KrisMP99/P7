using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.CourseCQRS.Commands;

namespace P7WebApp.API.Controllers
{
    public class CourseController : BaseController
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseCommand request)
        {
			try
			{
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return BadRequest("Could not create course");
                }
                else
                {
                    return Ok("Create Course");
                }
			}
			catch (Exception)
			{

				throw;
			}
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                return Ok("succes");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
