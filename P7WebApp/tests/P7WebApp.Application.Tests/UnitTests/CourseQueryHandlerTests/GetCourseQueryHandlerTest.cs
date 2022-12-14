using FluentAssertions;
using Moq;
using P7WebApp.Application.Common.Exceptions;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.CourseCQRS.QueryHandlers;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Application.Tests.UnitTests.CourseQueryHandlerTests
{
    public class GetCourseQueryHandlerTest
    {
        [Fact]
        public async Task Handle_Success_CourseResponseIsNotNull()
        {
            var query = new GetCourseQuery(id: 1);
            var course = new Course(ownerId: 1, title: "test", description: "description", isPrivate: false);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(muow => muow.CourseRepository.GetCourseWithExerciseGroupsAndExercisesAndAttendess(query.Id)).ReturnsAsync(new Course(ownerId: 1, title: "test", description: "description", isPrivate: false));
            var queryHandler = new GetCourseQueryHandler(mockUnitOfWork.Object);

            var actual = await queryHandler.Handle(query, CancellationToken.None);

            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_Success_CourseResponseWithCorrectMapping()
        {
            var query = new GetCourseQuery(id: 1);
            var course = new Course(ownerId: 1, title: "test", description: "description", isPrivate: false);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(muow => muow.CourseRepository.GetCourseWithExerciseGroupsAndExercisesAndAttendess(query.Id)).ReturnsAsync(new Course(ownerId: 1, title: "test", description: "description", isPrivate: false));
            var queryHandler = new GetCourseQueryHandler(mockUnitOfWork.Object);

            var actual = await queryHandler.Handle(query, CancellationToken.None);

            actual.Should().BeOfType<CourseResponse>();
        }

        [Fact]
        public async Task Handle_Fail_ThrowsNotFoundExceptionGivenInvalidId()
        {
            var query = new GetCourseQuery(id: 2);
            var course = new Course(ownerId: 1, title: "test", description: "description", isPrivate: false);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(muow => muow.CourseRepository.GetCourseWithExerciseGroupsAndExercisesAndAttendess(query.Id)).ReturnsAsync(It.IsAny<Course>);
            var queryHandler = new GetCourseQueryHandler(mockUnitOfWork.Object);

            Task action() => queryHandler.Handle(query, CancellationToken.None);

            await Assert.ThrowsAsync<NotFoundException>(action);
        }
    }
}