using FluentAssertions;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Domain.Tests.UnitTests.CourseAggregateTests
{
    public class InviteCodeTests
    {
        [Fact]
        public void UpdateInformation_Success_UpdatesIsActiveCorrectly()
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

            inviteCode.IsActive
                .Should()
                .Be(newIsActive);
        }

        [Fact]
        public void UpdateInformation_Success_UpdatesUseableFromCorrectly()
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

            inviteCode.UseableFrom
                .Should()
                .Be(newUseableFrom);
        }

        [Fact]
        public void UpdateInformation_Success_UpdatesUseableToCorrectly()
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

            inviteCode.UseableTo
                .Should()
                .Be(newUseableTo);
        }
    }
}
