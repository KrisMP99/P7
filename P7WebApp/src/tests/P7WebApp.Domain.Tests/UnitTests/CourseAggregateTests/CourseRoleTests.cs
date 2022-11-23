using FluentAssertions;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Domain.Tests.UnitTests.CourseAggregateTests
{
    public class CourseRoleTests
    {
        [Fact]
        public void EditInformation_Success_GivenNewRoleNameUpdatesRoleNameCorrectly()
        {
            var courseRole = new CourseRole(roleName: "TestName");
            string newRoleName = "NewTestName";

            courseRole.EditInformation(name: newRoleName);

            courseRole
                .RoleName
                .Should()
                .Be(newRoleName);
        }
    }
}
