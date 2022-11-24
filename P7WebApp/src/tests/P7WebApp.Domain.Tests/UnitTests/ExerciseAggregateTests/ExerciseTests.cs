using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Tests.UnitTests.ExerciseAggregateTests
{
    public class ExerciseTests
    {
        [Fact]
        public void EditInformation_Success_GivenNewInformationIsUpdatedCorrectly()
        {
            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow,
                visibleFrom: DateTime.UtcNow,
                visibleTo: DateTime.UtcNow,
                layoutId: 1);

            string newTitle = "NewTitle";
            bool newIsVisible = false;
            int newExerciseNumber = 1;
            DateTime newStartDate = DateTime.UtcNow;
            DateTime newEndDate = DateTime.UtcNow;
            int newLayoutId = 2;

            exercise.EditInformation(
                newTitle: newTitle,
                newIsVisible: newIsVisible,
                newExerciseNumber: newExerciseNumber,
                newStartDate: newStartDate,
                newEndDate: newEndDate,
                newLayoutId: newLayoutId
                );

            exercise.Title
                .Should()
                .BeSameAs(newTitle);
            exercise.IsVisible
                .Should()
                .Be(newIsVisible);
            exercise.ExerciseNumber
                .Should()
                .Be(newExerciseNumber);
            exercise.StartDate
                .Should()
                .BeSameDateAs(newStartDate);
            exercise.EndDate
                .Should()
                .BeSameDateAs(newEndDate);
            exercise.LayoutId
                .Should()
                .Be(newLayoutId);
        }

        [Fact]
        public void EditInformation_Fails_ThrowsExerciseExceptionGivenNullAsTitleParameter()
        {
            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow,
                visibleFrom: DateTime.UtcNow,
                visibleTo: DateTime.UtcNow,
                layoutId: 1);

            string newTitle = null;
            bool newIsVisible = false;
            int newExerciseNumber = 1;
            DateTime newStartDate = DateTime.UtcNow;
            DateTime newEndDate = DateTime.UtcNow;
            int newLayoutId = 2;

            Action act = () => exercise.EditInformation(
                                newTitle: newTitle,
                                newIsVisible: newIsVisible,
                                newExerciseNumber: newExerciseNumber,
                                newStartDate: newStartDate,
                                newEndDate: newEndDate,
                                newLayoutId: newLayoutId
                                );

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void EditInformation_Fails_ThrowsExerciseExceptionGivenNegativeNumberAsExerciseNumberParameter()
        {
            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow,
                visibleFrom: DateTime.UtcNow,
                visibleTo: DateTime.UtcNow,
                layoutId: 1);

            string newTitle = "NewTitle";
            bool newIsVisible = false;
            int newExerciseNumber = -1;
            DateTime newStartDate = DateTime.UtcNow;
            DateTime newEndDate = DateTime.UtcNow;
            int newLayoutId = 2;

            Action act = () => exercise.EditInformation(
                                newTitle: newTitle,
                                newIsVisible: newIsVisible,
                                newExerciseNumber: newExerciseNumber,
                                newStartDate: newStartDate,
                                newEndDate: newEndDate,
                                newLayoutId: newLayoutId
                                );

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Theory]
        [InlineData(7)]
        [InlineData(10)]
        [InlineData(100)]
        public void EditInformation_Fails_ThrowsExerciseExceptionGivenInvalidLayoutIdNumber(int layoutId)
        {
            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow,
                visibleFrom: DateTime.UtcNow,
                visibleTo: DateTime.UtcNow,
                layoutId: 1);

            string newTitle = "NewTitle";
            bool newIsVisible = false;
            int newExerciseNumber = 1;
            DateTime newStartDate = DateTime.UtcNow;
            DateTime newEndDate = DateTime.UtcNow;
            int newLayoutId = layoutId;

            Action act = () => exercise.EditInformation(
                                newTitle: newTitle,
                                newIsVisible: newIsVisible,
                                newExerciseNumber: newExerciseNumber,
                                newStartDate: newStartDate,
                                newEndDate: newEndDate,
                                newLayoutId: newLayoutId
                                );

            act
                .Should()
                .Throw<ExerciseException>();
        }




    }
}
