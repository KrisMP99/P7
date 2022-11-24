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
    }
}
