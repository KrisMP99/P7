using FluentAssertions;
using FluentAssertions.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7WebApp.API.Controllers;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using System.Collections.Generic;

namespace P7WebApp.Infrastructure.Tests.UnitTests.ControllerTests
{
    public class CourseControllerTests
    {
        // Course Related Tests
        [Fact]
        public async Task CreateCourse_ReturnsOK_ResultIsGreaterThanZero()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task CreateCourse_ReturnsBadRequest_ResultIsLessOrEqualToZero(int value)
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(value);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCourse_ReturnsBadRequest_ResultIsNull()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(null);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetListOfCourses_ReturnsOk_AmountIsOne()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            IEnumerable<CourseResponse> courseResponses = new List<CourseResponse> { new CourseResponse { Id = 1 } };
            mockMediator.Setup(m => m.Send(It.IsAny<GetListOfCoursesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(courseResponses);

            var result =  await courseController.GetListOfCourses();

            result.Should().BeOfType<OkObjectResult>();
        }




        // Exercise Groups Related Tests
        [Fact]
        public async Task GetExerciseGroupsByCourseId_ReturnsOkObject_And_ContainsTwoExerciseGroupReponses()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            IEnumerable<ExerciseGroupResponse> exerciseGroupResponses = new List<ExerciseGroupResponse> { new ExerciseGroupResponse(), new ExerciseGroupResponse() };
            mockMediator.Setup(m => m.Send(It.IsAny<GetExerciseGroupsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(exerciseGroupResponses);

            // Act
            var actionResult = await courseController.GetExerciseGroupsByCourseId(1);

            // Assert
            var resultObject = (OkObjectResult)actionResult;
            IEnumerable<ExerciseGroupResponse>? result = resultObject.Value as IEnumerable<ExerciseGroupResponse>;

            resultObject
                .Should()
                .NotBeNull();

            resultObject
                .Should()
                .BeOfType<OkObjectResult>();

            result
                .Should()
                .NotBeNull();

            result
                .Should()
                .BeOfType(typeof(List<ExerciseGroupResponse>));

            result
                .Should()
                .HaveCount(2);
        }


        // Exercise Related Tests

    }
}
