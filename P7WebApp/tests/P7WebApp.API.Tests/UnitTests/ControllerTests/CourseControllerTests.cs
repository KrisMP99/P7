using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7WebApp.API.Controllers;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Repositories;

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
            var createCourseCommand = new CreateCourseCommand("test", "test", false);
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
            var createCourseCommand = new CreateCourseCommand("test", "test", false);
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(value);

            var result = await courseController.CreateCourse(createCourseCommand);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCourse_ReturnsBadRequest_ResultIsNull()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false);
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(null);

            var result = await courseController.CreateCourse(createCourseCommand);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public async Task CreateInviteCode_ReturnsOk_GivenValidCourseId(int courseId)
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            InviteCodeResponse inviteCodeResponse = new InviteCodeResponse();
            mockMediator.Setup(m => m.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(inviteCodeResponse);

            var result = await courseController.CreateInviteCode(courseId);

            result.Should().BeOfType<OkObjectResult>();
        }

        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //[InlineData(100)]
        //public async Task DeleteCourse_ReturnsOk_GivenValidCourseId(int courseId)
        //{
        //    var mockMediator = new Mock<IMediator>();
        //    var courseController = new CourseController(mockMediator.Object);
        //    int rowsAffected = 1;
        //    mockMediator.Setup(m => m.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).
        //        ReturnsAsync(rowsAffected);

        //    var result = await courseController.DeleteCourse(courseId);

        //    result.Should().BeOfType<OkObjectResult>();
        //}

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public async Task GetCourse_ReturnsOK_GivenValidCourseId(int courseId)
        {
            var mockRepo = new Mock<ICourseRepository>();
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            CourseResponse courseResponse = new CourseResponse();
            mockMediator.Setup(m => m.Send(It.IsAny<int>(), It.IsAny<CancellationToken>())).
                ReturnsAsync(courseResponse);

            var result = await courseController.GetCourse(courseId);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetListOfCourses_ReturnsOk_AmountIsOne()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            IEnumerable<CourseResponse> courseResponses = new List<CourseResponse> { new CourseResponse { Id = 1 } };
            mockMediator.Setup(m => m.Send(It.IsAny<GetListOfCoursesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(courseResponses);

            var result = await courseController.GetListOfCourses(1);

            result.Should().BeOfType<OkObjectResult>();
        }

        // Exercise Groups Related Tests
        [Fact]
        public async Task GetExerciseGroupsByCourseId_ReturnsOkObject_GivenListOfExerciseGroupResponses()
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

            resultObject
                .Should()
                .NotBeNull()
                .And
                .BeOfType<OkObjectResult>();
        }
        // Exercise Related Tests

    }
}
