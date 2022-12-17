using Moq;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.CourseCQRS.CommandHandlers;
using P7WebApp.Application.CourseCQRS.Commands.CreateCourse;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using FluentAssertions;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup;

namespace P7WebApp.Application.Tests.IntegrationTests.CourseCommandHandlerTests
{
    public class CreateCourseCommandHandlerTests
    {

        //[Fact]
        //public async Task CreateExerciseGroup_IsAddedToCorrectCourse_GivenCorrectGroupAndCourse()
        //{
        //    // Arrange
        //    var command = new CreateExerciseGroupCommand(1, "group","groupdes",1,false, visibleFromDate: DateTime.Now);
        //    var course = new Course(1, "title", "des", false);
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var commandHandler = new CreateExerciseGroupCommandHandler(mockUnitOfWork.Object);
        //    mockUnitOfWork.Setup(m => m.CourseRepository.GetCourseWithExerciseGroups(1)).ReturnsAsync(course);
        //    mockUnitOfWork.Setup(m => m.CommitChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //    // Act
        //    commandHandler.Handle(command, It.IsAny<CancellationToken>());
        //    var exerciseGroups = course.ExerciseGroups.ToList();

        //    // Assert
        //    exerciseGroups[0].CourseId.Should().Be(1);
        //}
    }
}
