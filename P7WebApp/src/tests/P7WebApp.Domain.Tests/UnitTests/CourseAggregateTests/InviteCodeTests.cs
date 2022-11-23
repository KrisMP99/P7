using FluentAssertions;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Domain.Tests.UnitTests.CourseAggregateTests
{
    public class InviteCodeTests
    {
        [Fact]
        public void UpdateInformation_Success_UpdatesInviteCodeCorrectlyCorrespondingToNewCorrectInformation()
        {
            var inviteCode = new InviteCode(
                courseId: 0,
                isActive: true,
                useableFrom: DateTime.UtcNow,
                useableTo: DateTime.UtcNow);
            bool newIsActive = false;
            DateTime newUseableFrom = DateTime.UtcNow;
            DateTime newUseableTo = DateTime.UtcNow;

            inviteCode.UpdateInformation(newIsActive, newUseableFrom, newUseableTo);

            inviteCode
                .IsActive
                .Should()
                .Be(newIsActive);
            inviteCode
                .UseableFrom
                .Should()
                .Be(newUseableFrom);
            inviteCode
                .UseableTo
                .Should()
                .Be(newUseableTo);      
        }
    }
}
