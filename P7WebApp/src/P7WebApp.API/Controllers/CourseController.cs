using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;

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
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
			}
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                var result = _mediator.Send(new GetCourseQuery(id));
                if (result == null)
                {
                    return BadRequest("Didn't find course");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return BadRequest("Couldn't update the course");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
                
        [HttpGet]
        [Route("{id}/exercise-groups")]
        public async Task<IActionResult> GetExerciseGroups(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetExerciseGroupsQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}")]
        public async Task<IActionResult> UpdateExercise(UpdateExerciseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not update the exercise");
                }
                else
                {
                return Ok(result);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{courseId}/exercise-groups/{exerciseGroupId}")]
        public async Task<IActionResult> UpdateExerciseGroup(UpdateExerciseGroupCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not update the exercise group");
                }
                else
                {
                    return Ok(result);

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
