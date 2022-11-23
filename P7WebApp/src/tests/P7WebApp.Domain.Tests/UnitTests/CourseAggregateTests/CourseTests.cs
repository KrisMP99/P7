using P7WebApp.Domain.Aggregates.CourseAggregate;
using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using Moq;

namespace P7WebApp.Domain.Tests.UnitTests.CourseAggregateTests
{
    public class CourseTests
    {
        [Fact]
        public void EditInformation_Sucess_GivenNewInformationIsUpdatedCorrectly()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);
            string newTitle = "TestNew";
            string newDescription = "TestNew";
            bool newVisibility = false;

            course.EditInformation(newTitle: newTitle, newDescription: newDescription, newVisibility: newVisibility);

            course.Title
                .Should()
                .BeSameAs(newTitle);
            course.Description
                .Should()
                .BeSameAs(newDescription);
            course.IsPrivate
                .Should()
                .Be(newVisibility);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_Success_ReturnsOneExerciseGroupGivenCorrectExerciseGroupId(int exerciseGroupId)
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test", 
                description: "Test",
                exerciseGroupNumber: exerciseGroupId,
                isVisible: true,
                DateTime.UtcNow);

            exerciseGroup.Id = exerciseGroupId;
            course.AddExerciseGroup(exerciseGroup);

            var result = course.GetExerciseGroup(exerciseGroupId);

            result
                .Should()
                .BeOfType<ExerciseGroup>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_Fails_ReturnsExceptionWhenExerciseGroupsListIsEmptyAndIsGivenExerciseGroupsIds(int exerciseGroupId)
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.GetExerciseGroup(exerciseGroupId);

            act.Should().Throw<Exception>();
        }

    }
}
