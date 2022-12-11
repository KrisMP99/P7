using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Commands.CreateCourse;
using P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup;
using P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode;
using P7WebApp.Application.CourseCQRS.Commands.DeleteCourse;
using P7WebApp.Application.CourseCQRS.Commands.DeleteExerciseGroup;
using P7WebApp.Application.CourseCQRS.Commands.UpdateCourse;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSolution;
using P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.DeleteSolution;
using P7WebApp.Application.ExerciseCQRS.Commands.DeleteSubmission;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateSolution;
using P7WebApp.Application.ExerciseCQRS.Queries.Get;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.DeleteExercise;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.UpdateExerciseGroup;

namespace P7WebApp.API.Controllers
{
    [Route("api/courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CourseController : BaseController
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand request)
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

        [HttpGet("{id}/invite-code")]
        public async Task<IActionResult> CreateInviteCode([FromRoute] int id)
        {
            try
            {
                var result = await _mediator.Send(new CreateInviteCodeCommand(id, true));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("invite-code/{code}")]
        public async Task<IActionResult> GetCourseFromInviteCode([FromRoute] int code)
        {
            try
            {
                int result = await _mediator.Send(new GetCourseIdFromInviteCodeQuery(code));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollToCourse([FromBody]EnrollToCourseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{courseId}/leave")]
        public async Task<IActionResult> LeaveCourse([FromRoute] int courseId)
        {
            try
            {
                var result = await _mediator.Send(new LeaveCourseCommand(courseId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCourseCommand(courseId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{courseId}")]
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

        [HttpGet("get-courses/{amount}")]
        public async Task<IActionResult> GetListOfCourses([FromRoute] int amount)
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

        [HttpGet("public")]
        public async Task<IActionResult> GetPublicCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetPublicCoursesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("No changes to the course were made, can therefore not update");
            }
        }

        [HttpGet("{courseId}/exercise-groups")]
        public async Task<IActionResult> GetExerciseGroupsByCourseId([FromRoute]int courseId)
        {
            try
            {
                var result = await _mediator.Send(new GetExerciseGroupsQuery(courseId));
                if (result is not null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("exercise-groups")]
        public async Task<IActionResult> AddExerciseGroup([FromBody] CreateExerciseGroupCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if(result != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not create/add the exercise group to the course.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("exercise-groups/update")]
        public async Task<IActionResult> UpdateExerciseGroup([FromBody]UpdateExerciseGroupCommand request)
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

        [HttpDelete("{courseId}/exercise-group/{exerciseGroupId}")]
        public async Task<IActionResult> DeleteExerciseGroup([FromRoute] int courseId, [FromRoute] int exerciseGroupId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteExerciseGroupCommand(courseId, exerciseGroupId));

                if (result == 0)
                {
                    return BadRequest("Could not create/add the exercise group to the course.");
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

        [HttpPost("exercise-groups/exercises")]
        public async Task<IActionResult> AddExercise([FromBody]CreateExerciseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result != 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Could not create exercise");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpDelete("exercise-groups/{exerciseGroupId}/exercises/{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int exerciseGroupId, [FromRoute] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteExerciseCommand(id, exerciseGroupId));

                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Could not find the exercise and delete it");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("exercise-groups/exercises")]
        public async Task<IActionResult> UpdateExercise(UpdateExerciseCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok("Updated");  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{courseId}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}")]
        public async Task<IActionResult> GetExercise([FromRoute]int courseId, [FromRoute] int exerciseGroupId, [FromRoute] int exerciseId)
        {
            try
            {
                var result = await _mediator.Send(new GetExerciseQuery(courseId: courseId, exerciseGroupId: exerciseGroupId, exerciseId: exerciseId));

                if (result is null)
                {
                    return BadRequest("Could not find an exercise with the specified id");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("exercise-groups/exercises/solution")]
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

        [HttpDelete("exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-solution/{solutionId}")]
        public async Task<IActionResult> DeleteSolution([FromRoute] int solutionId, [FromRoute] int exerciseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteSolutionCommand() { SolutionId = solutionId, ExerciseId = exerciseId});

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

        [HttpPut("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/solutions/{solutionId}/update")]
        public async Task<IActionResult> UpdateSolution(UpdateSolutionCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result != 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Could not update the exercise");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/create-submission")]
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

        [HttpDelete("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-submission/{submissionId}")]
        public async Task<IActionResult> DeleteSubmission([FromRoute] int submissionId, [FromRoute] int exerciseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteSubmissionCommand() { SubmissionId = submissionId, ExerciseId = exerciseId });

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
    }
}
