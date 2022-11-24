using P7WebApp.Domain.Aggregates.CourseAggregate;
using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using Moq;
using P7WebApp.Domain.Exceptions;

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

        [Fact]
        public void EditInformation_ThrowsCourseException_GivenNullForTitleParameter()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);
            string newDescription = "TestNew";
            bool newVisibility = false;

            Action act = () => course.EditInformation(newTitle: null, newDescription: newDescription, newVisibility: newVisibility);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void EditInformation_ThrowsCourseException_GivenNullForDescriptionParameter()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);
            string newTitle = "TestNew";
            string newDescription = "TestNew";
            bool newVisibility = false;

            Action act = () => course.EditInformation(newTitle: newTitle, newDescription: null, newVisibility: newVisibility);

            act
                .Should()
                .Throw<CourseException>();
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
                .BeOfType<ExerciseGroup>()
                .And
                .Be(exerciseGroup);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_ThrowsCourseException_GivenExerciseGroupsListIsEmptyAndIsGivenExerciseGroupsIds(int exerciseGroupId)
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.GetExerciseGroup(exerciseGroupId);

            act.Should().Throw<CourseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_Success_ReturnsOneExerciseGroupWithCorrectCourseIdGivenCorrectCourseIdAndExerciseGroupIdAndExerciseGroupListIsNotEmpty(int courseId)
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: courseId,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: courseId,
                isVisible: true,
                DateTime.UtcNow);
            exerciseGroup.Id = courseId;

            course.AddExerciseGroup(exerciseGroup);

            var result = course.GetExerciseGroup(courseId);

            result
                .Should()
                .BeOfType<ExerciseGroup>()
                .Which
                .CourseId
                .Should()
                .Be(courseId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_Success_ReturnsOneExerciseGroupWithCorrectExerciseGroupIdGivenCorrectExerciseGroupId(int exerciseGroupId)
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
                .BeOfType<ExerciseGroup>()
                .Which
                .Id
                .Should()
                .Be(exerciseGroupId);
        }

        [Fact]
        public void GetExerciseGroup_ThrowsCourseException_GivenNoExerciseGroupIsFoundGivenWrongGroupExerciseIdAndExerciseGroupListIsNotEmpty()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);
            exerciseGroup.Id = 0;

            course.AddExerciseGroup(exerciseGroup);

            Action act = () => course.GetExerciseGroup(1);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void CreateInviteCode_Success_InviteCodeIsAddedToTheCourseGivenCorrectInviteCodeObject()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var inviteCode = new InviteCode(
                courseId: 0,
                isActive: true,
                useableFrom: DateTime.UtcNow,
                useableTo: DateTime.UtcNow);

            course.CreateInviteCode(inviteCode);

            var result = course.InviteCode;

            result
                .Should()
                .BeOfType<InviteCode>();
        }

        [Fact]
        public void CreateInviteCode_ThrowsCourseException_GivenNullAsParameter()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);    

            Action act = () => course.CreateInviteCode(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddExerciseGroup_Success_ExerciseGroupIsAddedToExerciseGroupListGivenCorrectExerciseGroup()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);

            course.AddExerciseGroup(exerciseGroup);

            course
                .ExerciseGroups
                .Should()
                .Contain(exerciseGroup);
        }

        [Fact]
        public void AddExerciseGroup_ThrowsCourseException_GivenNullAsParameter()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.AddExerciseGroup(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddExerciseGroup_ThrowsCourseException_GivenNegativeExerciseGroupNumber()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: -1,
                isVisible: true,
                DateTime.UtcNow);

            Action act = () => course.AddExerciseGroup(exerciseGroup);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddExerciseGroup_ThrowsCourseException_GivenTwoExerciseGroupsWithSameExerciseGroupNumber()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup1 = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);

            var exerciseGroup2 = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);
            course.AddExerciseGroup(exerciseGroup1);

            Action act = () => course.AddExerciseGroup(exerciseGroup2);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void RemoveExerciseGroup_Success_RemovesCorrectExerciseGroupGivenCorrectExerciseGroupIdAndTheExerciseGroupIsInTheExerciseGroupList()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);
            exerciseGroup.Id = 1;
            course.AddExerciseGroup(exerciseGroup);

            course.RemoveExerciseGroup(1);

            course
                .ExerciseGroups
                .Should()
                .NotContain(exerciseGroup);
        }

        [Fact]
        public void RemoveExerciseGroup_ThrowsCourseException_ivenExerciseGroupIdAndTheExerciseGroupDoesNotExistAndExerciseGroupListIsEmpty()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.RemoveExerciseGroup(1);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void RemoveExerciseGroup_ThrowsCourseException_GivenExerciseGroupIdAndTheExerciseGroupDoesNotExistAndTheExerciseGroupListIsNotEmpty()
        {
            var course = new Course(title: "Test", description: "Test", isPrivate: true);

            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                DateTime.UtcNow);
            exerciseGroup.Id = 0;
            course.AddExerciseGroup(exerciseGroup);

            Action act = () => course.RemoveExerciseGroup(1);

            act
                .Should()
                .Throw<CourseException>();
        }
    }
}