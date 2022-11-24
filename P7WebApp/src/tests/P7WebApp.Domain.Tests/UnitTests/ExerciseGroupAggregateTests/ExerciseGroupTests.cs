﻿using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Tests.UnitTests.ExerciseGroupAggregateTests
{
    public class ExerciseGroupTests
    {
        [Fact]
        public void GetExercise_Success_ReturnsOneExerciseGivenCorrectExerciseIdAndExerciseIsAddedToTheExerciseList()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            exerciseGroup.Id = 0;

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = 0;

            exerciseGroup.AddExercise(exercise);

            var result = exerciseGroup.GetExercise(0);

            result
                .Should()
                .BeOfType<Exercise>()
                .And
                .Be(exercise);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExercise_Fails_ThrowsExerciseGroupExceptionWhenExerciseListIsEmptyAndIsGivenExerciseId(int exerciseId)
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            exerciseGroup.Id = 0;

            Action act = () => exerciseGroup.GetExercise(exerciseId);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExercise_Success_ReturnsOneExerciseWithCorrectExerciseGroupIdGivenCorrectExerciseGroupIdAndExerciseIdAndExerciseListIsNotEmpty(int exerciseGroupId)
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            exerciseGroup.Id = exerciseGroupId;

            var exercise = new Exercise(
                exerciseGroupId: exerciseGroupId,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = exerciseGroupId;

            exerciseGroup.AddExercise(exercise);

            var result = exerciseGroup.GetExercise(exerciseGroupId);

            result
                .Should()
                .BeOfType<Exercise>()
                .Which
                .ExerciseGroupId
                .Should()
                .Be(exerciseGroupId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetExercise_Success_ReturnOneExerciseWithCorrectExerciseIdGivenCorrectExerciseIdAndExerciseListIsNotEmpty(int exerciseId)
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = exerciseId;

            exerciseGroup.AddExercise(exercise);

            var result = exerciseGroup.GetExercise(exerciseId);

            result
                .Should()
                .BeOfType<Exercise>()
                .Which
                .Id
                .Should()
                .Be(exerciseId);
        }

        [Fact]
        public void GetExercise_Fail_ThrowsExerciseGroupExceptionWhenExerciseCouldNotBefoundGivenCorrectExerciseIdAndExerciseListIsEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            Action act = () => exerciseGroup.GetExercise(0);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void GetExercise_Fail_ThrowsExerciseGroupExceptionWhenExerciseCouldNotBefoundGivenCorrectExerciseIdAndExerciseListIsNotEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = 1;

            exerciseGroup.AddExercise(exercise);

            Action act = () => exerciseGroup.GetExercise(0);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }


        [Fact]
        public void GetAllExercises_Success_ReturnsExerciseListObjectAndExerciseListIsEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var result = exerciseGroup.GetAllExercises();

            result
                .Should()
                .BeOfType<List<Exercise>>();
        }

        [Fact]
        public void GetAllExercises_Success_ReturnsExerciseListObjectAndExerciseListIsNotEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exerciseGroup.AddExercise(exercise);

            var result = exerciseGroup.GetAllExercises();

            result
                .Should()
                .HaveCountGreaterThanOrEqualTo(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void GetAllExercises_Success_ReturnsExerciseListObjectAndExerciseListContainsXNumberOfCorrectExercisesGivenTenExercisesIsAddedToTheExerciseGroup(int count)
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            for (int i = 0; i < count; i++)
            {
                var exercise = new Exercise(
                    exerciseGroupId: 0,
                    title: "Test",
                    isVisible: true,
                    exerciseNumber: i,
                    null,
                    null,
                    null,
                    null,
                    1
                    );
                exerciseGroup.AddExercise(exercise);
            }

            var result = exerciseGroup.GetAllExercises();

            result
                .Should()
                .HaveCount(count);
        }

        [Fact]
        public void EditInformation_Success_GivenNewInformationIsUpdatedCorrectly()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            string newTitle = "NewTest";
            string newDescription = "NewTest";
            int newExerciseGroupNumber = 999;
            bool newIsVisible = false;
            DateTime newVisibleFromDate = DateTime.UtcNow;

            exerciseGroup.EditInformation(
                newTitle: newTitle,
                newDescription: newDescription,
                newExerciseGroupNumber,
                newIsVisible,
                newBecomeVisibleAt: newVisibleFromDate);

            exerciseGroup.Title
                .Should()
                .BeSameAs(newTitle);
            exerciseGroup.Description
                .Should()
                .BeSameAs(newDescription);
            exerciseGroup.ExerciseGroupNumber
                .Should()
                .Be(newExerciseGroupNumber);
            exerciseGroup.IsVisible
                .Should()
                .Be(newIsVisible);
            exerciseGroup.VisibleFromDate
                .Should()
                .BeSameDateAs(newVisibleFromDate);
        }

        [Fact]
        public void EditInformation_Fails_ThrowsExerciseGroupExceptionGivenNullAsTitleParameter()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            string newTitle = null;
            string newDescription = "NewTest";
            int newExerciseGroupNumber = 999;
            bool newIsVisible = false;
            DateTime newVisibleFromDate = DateTime.UtcNow;

            Action act = () => exerciseGroup.EditInformation(
                                newTitle: newTitle,
                                newDescription: newDescription,
                                newExerciseGroupNumber,
                                newIsVisible,
                                newBecomeVisibleAt: newVisibleFromDate);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void EditInformation_Fails_ThrowsExerciseGroupExceptionGivenNullAsDescriptionParameter()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            string newTitle = "NewTest";
            string newDescription = null;
            int newExerciseGroupNumber = 999;
            bool newIsVisible = false;
            DateTime newVisibleFromDate = DateTime.UtcNow;

            Action act = () => exerciseGroup.EditInformation(
                                newTitle: newTitle,
                                newDescription: newDescription,
                                newExerciseGroupNumber,
                                newIsVisible,
                                newBecomeVisibleAt: newVisibleFromDate);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void EditInformation_Fails_ThrowsExerciseGroupExceptionGivenNegativeNumberAsExerciseGroupNumber()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );
            string newTitle = "NewTest";
            string newDescription = "NewTest";
            int newExerciseGroupNumber = -1;
            bool newIsVisible = false;
            DateTime newVisibleFromDate = DateTime.UtcNow;

            Action act = () => exerciseGroup.EditInformation(
                                newTitle: newTitle,
                                newDescription: newDescription,
                                newExerciseGroupNumber,
                                newIsVisible,
                                newBecomeVisibleAt: newVisibleFromDate);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void AddExercise_Success_GivenExerciseIsCorrectlyAddedToTheExercisListGivenCorrectExercise()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exerciseGroup.AddExercise(exercise);

            exerciseGroup
                .Exercises
                .Should()
                .Contain(exercise);
        }

        [Fact]
        public void AddExercise_Fails_ThrowsExceptionGivenNullAsParameter()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            Action act = () => exerciseGroup.AddExercise(null);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void RemoveExerciseById_Success_RemovesCorrectExerciseGivenCorrectExerciseIdAndTheExerciseIsInTheExerciseList()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = 1;

            exerciseGroup.AddExercise(exercise);

            exerciseGroup.RemoveExerciseById(1);

            exerciseGroup
                .Exercises
                .Should()
                .NotContain(exercise);

        }

        [Fact]
        public void RemoveExerciseById_Fails_ThrowsExceptionGivenExerciseIdButTheExerciseIsNotInExerciseListAndTheExerciseListIsEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            Action act = () => exerciseGroup.RemoveExerciseById(1);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

        [Fact]
        public void RemoveExerciseById_Fails_ThrowsExceptionGivenExerciseIdButTheExerciseIsNotInExerciseListAndTheExerciseListIsNotEmpty()
        {
            var exerciseGroup = new ExerciseGroup(
                courseId: 0,
                title: "Test",
                description: "Test",
                exerciseGroupNumber: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow
                );

            var exercise = new Exercise(
                exerciseGroupId: 0,
                title: "Test",
                isVisible: true,
                exerciseNumber: 0,
                null,
                null,
                null,
                null,
                1
                );
            exercise.Id = 0;

            exerciseGroup.AddExercise(exercise);

            Action act = () => exerciseGroup.RemoveExerciseById(1);

            act
                .Should()
                .Throw<ExerciseGroupException>();
        }

    }
}
