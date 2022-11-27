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
        public void Course_Success_CreatesCourseWithDefaultCourseRoleAndPermission()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: false);

            course.CourseRoles
                .Should()
                .HaveCount(1);
            course.CourseRoles.ElementAt(0).RoleName
                .Should()
                .BeEquivalentTo("attendee");
            course.CourseRoles.ElementAt(0).IsDefaultRole
                .Should()
                .BeTrue();
            course.CourseRoles.ElementAt(0).Permission.CourseRoleId
                .Should()
                .Be(0);
        }

        [Fact]
        public void EditInformation_Sucess_GivenNewInformationIsUpdatedCorrectly()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.GetExerciseGroup(exerciseGroupId);

            act.Should().Throw<CourseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExerciseGroup_Success_ReturnsOneExerciseGroupWithCorrectCourseIdGivenCorrectCourseIdAndExerciseGroupIdAndExerciseGroupListIsNotEmpty(int courseId)
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);    

            Action act = () => course.CreateInviteCode(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddAttendee_Success_GivenCorrectAttendee()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
            var attendee = new Attendee(courseId: course.Id, courseRoleId: 0, profileId: 0);

            course.AddAttendee(attendee);

            course.Attendees
                .Should()
                .HaveCount(1)
                .And
                .Contain(attendee);
        }

        [Fact]
        public void AddAttendee_ThrowsCourseException_GivenNull()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.AddAttendee(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(111111)]
        [InlineData(347623784)]
        public void GetAttendeeByProfileId_Success_GivenAttendeeWithCorrectProfileId(int profileId)
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
            var attendee = new Attendee(courseId: course.Id, courseRoleId: 0, profileId: profileId);

            course.AddAttendee(attendee);

            var result = course.GetAttendeeByProfileId(profileId);

            result
                .Should()
                .Be(attendee);
        }

        [Fact]
        public void GetAttendeeByProfileId_ThrowsCourseException_GivenIncorrectProfileIdAndAttendeeListIsEmtpy()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.GetAttendeeByProfileId(1);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void RemoveAttendeeByProfileId_Success_GivenAttendeeAndCorrectProfileId()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            var attendee = new Attendee(courseId: course.Id, courseRoleId: 0, profileId: 0);
            course.AddAttendee(attendee);

            course.RemoveAttendeeByProfileId(attendee.ProfileId);

            course.Attendees
                .Should()
                .NotContain(attendee)
                .And
                .HaveCount(0);
        }

        [Fact]
        public void GetAttendeeByProfileId_ThrowsCourseException_GivenAttendeeWithIncorrectProfileIdAndAttendeeListIsNotEmtpy()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
            var attendee = new Attendee(courseId: course.Id, courseRoleId: 0, profileId: 0);

            course.AddAttendee(attendee);

            Action act = () => course.GetAttendeeByProfileId(1);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddExerciseGroup_Success_ExerciseGroupIsAddedToExerciseGroupListGivenCorrectExerciseGroup()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.AddExerciseGroup(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddExerciseGroup_ThrowsCourseException_GivenNegativeExerciseGroupNumber()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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
        public void RemoveExerciseGroup_ThrowsCourseException_GivenExerciseGroupIdAndTheExerciseGroupDoesNotExistAndExerciseGroupListIsEmpty()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.RemoveExerciseGroup(1);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void RemoveExerciseGroup_ThrowsCourseException_GivenExerciseGroupIdAndTheExerciseGroupDoesNotExistAndTheExerciseGroupListIsNotEmpty()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

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

        [Fact]
        public void AddCourseRole_Success_GivenCorrectCourseRole()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            var courseRole = new CourseRole(courseId: course.Id, roleName: "Admin");
            var permission = new Permission(courseRoleId: courseRole.Id);
            courseRole.UpdatePermission(permission);
            
            course.AddCourseRole(courseRole);

            course.CourseRoles
                .Should()
                .Contain(courseRole)
                .And
                .HaveCount(2); // The default role and the new role we have created
        }

        [Fact]
        public void AddCourseRole_ThrowsCourseException_GivenNullAsParameter()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);

            Action act = () => course.AddCourseRole(null);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void AddCourseRole_ThrowsCourseException_GivenTwoDefaultRoles()
        {
            var course = new Course(ownerId: 0, title: "Test", description: "Test", isPrivate: true);
            var courseRole = course.CourseRoles.ElementAt(0); // We use a copy of the default course role

            Action act = () => course.AddCourseRole(courseRole);

            act
                .Should()
                .Throw<CourseException>();
        }
    }
}