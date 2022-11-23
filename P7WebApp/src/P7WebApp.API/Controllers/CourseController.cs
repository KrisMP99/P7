﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.ExerciseCQRS.Commands;
using P7WebApp.Application.ExerciseGroupCQRS.Commands;

namespace P7WebApp.API.Controllers
{
    [Route("api/courses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
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

        [HttpPost]
        [Route("invite-code")]
        public async Task<IActionResult> CreateInviteCode([FromBody] CreateInviteCodeCommand request)
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

        [HttpDelete]
        [Route("{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCourseCommand(courseId));
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

        [HttpGet]
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

        [HttpGet]
        [Route("get-courses/{amount}")]
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


        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseCommand request)
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


        [HttpGet]
        [Route("{courseId}/exercise-groups")]
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

        [HttpPost]
        [Route("exercise-group")]
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
        
        [HttpPost]
        [Route("exercise-groups/update")]
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

        [HttpDelete]
        [Route("course/{courseId}/exercise-group/{exerciseGroupId}")]
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

        [HttpPost]
        [Route("exercise-groups/exercises")]
        public async Task<IActionResult> AddExercise([FromBody] CreateExerciseCommand request)
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
        
        [HttpDelete]
        [Route("exercise-groups/{exerciseGroupId}/exercises/{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int exerciseGroupId, [FromRoute] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteExerciseCommand(id, exerciseGroupId));
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

        [HttpPost]
        [Route("exercise-groups/exercises/update")]
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
        [Route("exercise-groups/exercises/solution")]
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

        [HttpDelete]
        [Route("exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-solution/{solutionId}")]
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

        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/solutions/{solutionId}/update")]
        public async Task<IActionResult> UpdateSoltuion(UpdateSolutionCommand request)
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

        [HttpDelete]
        [Route("{id}/exercise-groups/{exerciseGroupId}/exercises/{exerciseId}/delete-submission/{submissionId}")]
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
