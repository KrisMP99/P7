using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Domain.Identity;

namespace P7WebApp.API.Controllers
{
    [Route("api/courses")]
    [Authorize]
    public class CourseController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
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
        [Route("get-courses/{amount}")]
        public async Task<IActionResult> GetListOfCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetListOfCoursesQuery());
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest($"Could not find course with id");
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
        public async Task<IActionResult> GetExerciseGroupsByCourseId([FromRoute]int courseId)
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
        [Route("{courseId}/delete-exercise-group/{exerciseGroupId}")]
        public async Task<IActionResult> DeleteExerciseGroup([FromRoute] int exerciseGroupId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteExerciseGroupCommand(exerciseGroupId));

                if (result == 0)
                {
                    return BadRequest("Could not create/add the exercise group to the course.");
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/add-module")]
        public async Task<IActionResult> AddModule(CreateModuleCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not create modules");
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-module/{moduleId}")]
        public async Task<IActionResult> DeleteModule(DeleteModuleCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not delete modules");
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/create-solution")]
        public async Task<IActionResult> CreateSolution(CreateSolutionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not create solution");
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-solution/{solutionId}")]
        public async Task<IActionResult> DeleteSolution(CreateSolutionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not delete solution");
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/create-submission")]
        public async Task<IActionResult> CreateSubmission(CreateSubmissionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not create submission");
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
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-submission/{submissionId}")]
        public async Task<IActionResult> DeleteSubmission(DeleteSubmissionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == 0)
                {
                    return BadRequest("Could not delete submission");
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
