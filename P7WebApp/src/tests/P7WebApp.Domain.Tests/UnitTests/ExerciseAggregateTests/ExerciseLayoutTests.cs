using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;

namespace P7WebApp.Domain.Tests.UnitTests.ExerciseAggregateTests
{
    public class ExerciseLayoutTests
    {
        [Theory]
        [InlineData("Single")]
        [InlineData("TwoVertical")]
        [InlineData("TwoHorizontal")]
        [InlineData("TwoLeftOneRight")]
        [InlineData("OneLeftTwoRight")]
        [InlineData("TwoLeftTwoRight")]
        public void FromName_Success_GivenCorrectLayoutNames(string layoutName)
        {
            var result = ExerciseLayout.FromName(layoutName);

            result
                .Should()
                .BeOfType<ExerciseLayout>();
            result.Name
                .Should()
                .BeLowerCased(layoutName);
        }

        [Theory]
        [InlineData(1, "Single")]
        [InlineData(2, "TwoVertical")]
        [InlineData(3, "TwoHorizontal")]
        [InlineData(4, "TwoLeftOneRight")]
        [InlineData(5, "OneLeftTwoRight")]
        [InlineData(6, "TwoLeftTwoRight")]
        public void FromId_Success_GivenCorrectLayoutIds(int layoutId, string layoutName)
        {
            var result = ExerciseLayout.FromId(layoutId);

            result
                .Should()
                .BeOfType<ExerciseLayout>();
            result.Name
                .Should()
                .BeLowerCased(layoutName);

        }
    }
}
