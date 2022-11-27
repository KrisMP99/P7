using FluentAssertions;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Tests.UnitTests.CourseAggregateTests
{
    public class CourseRoleTests
    {
        [Fact]
        public void CreateDefaultCourseRole_Success_GivenCourseId()
        {
            var courseRole = CourseRole.CreateDefaultCourseRole(0);

            courseRole.IsDefaultRole
                .Should()
                .BeTrue();
            courseRole.RoleName
                .Should()
                .BeEquivalentTo("Attendee");
            courseRole.Permission.Id
                .Should()
                .Be(0);
        }

        [Fact]
        public void UpdatePermission_Success_GivenNewPermission()
        {
            var courseRole = new CourseRole(courseId: 0, roleName: "TestName");
            var newPermission = new Permission(0, true);

            courseRole.UpdatePermission(newPermission);

            courseRole.Permission
                .Should()
                .Be(newPermission);
        }

        [Fact]
        public void UpdatePermission_ThrowsCourseException_GivenNull()
        {
            var courseRole = new CourseRole(courseId: 0, roleName: "TestName");

            Action act = () => courseRole.UpdatePermission(null);

            act
                .Should()
                .Throw<CourseException>();
        }


        [Fact]
        public void EditInformation_Success_GivenNewRoleNameUpdatesRoleNameCorrectly()
        {
            var courseRole = new CourseRole(courseId: 0, roleName: "TestName");
            string newRoleName = "NewTestName";

            courseRole.EditInformation(roleName: newRoleName);

            courseRole
                .RoleName
                .Should()
                .Be(newRoleName);
        }

        [Fact]
        public void EditInformation_ThrowsCourseException_GivenEmptyRoleName()
        {

            var courseRole = new CourseRole(courseId: 0, roleName: "TestName");
            string newRoleName = "";

            Action act = () => courseRole.EditInformation(roleName: newRoleName);

            act
                .Should()
                .Throw<CourseException>();
        }

        [Fact]
        public void EditInformation_ThrowsCourseException_GivenNull()
        {

            var courseRole = new CourseRole(courseId: 0, roleName: "TestName");

            Action act = () => courseRole.EditInformation(roleName: null);

            act
                .Should()
                .Throw<CourseException>();
        }
    }
}
