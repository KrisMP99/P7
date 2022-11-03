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
                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not create course");
                }
			}
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
			}
        }

        [HttpPost]
        [Route("{courseId}/invite-code")]
        public async Task<IActionResult> CreateInviteCode(CreateInviteCodeCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if(result == 0)
                {
                    return BadRequest("Could not create invite code");
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

        [HttpPost]
        [Route("{courseId}")]
        public async Task<IActionResult> GetCourse([FromRoute]int courseId)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseQuery(courseId));
                if (result == null)
                {
                    return BadRequest($"Could not find course with id {courseId}");
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

        [HttpPost]
        [Route("{amount}")]
        public async Task<IActionResult> GetListOfCourses([FromRoute] int amount)
        {
            try
            {
                var result = await _mediator.Send(new GetListOfCoursesQuery(amount));
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Could not find course with id {amount}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("{courseId}/update")]
        public async Task<IActionResult> UpdateCourse(UpdateCourseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return BadRequest("Could not update the course");
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



        [HttpPost]
        [Route("{courseId}/exercise-groups")]
        public async Task<IActionResult> GetExerciseGroups([FromRoute]int courseId)
        {
            try
            {
                var result = await _mediator.Send(new GetExerciseGroupsQuery(courseId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{courseId}/add-exercise-group")]
        public async Task<IActionResult> AddExerciseGroup(CreateExerciseGroupCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if(result == 0)
                {
                    return BadRequest("Could not create/add the exercise group to the course.");
                }
                else
                {
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
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

        [HttpPost]
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
