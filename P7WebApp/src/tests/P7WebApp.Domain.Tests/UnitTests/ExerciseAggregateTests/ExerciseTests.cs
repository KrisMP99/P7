using FluentAssertions;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
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

        [Fact]
        public void AddModule_Success_AddModulesToModuleListGivenCodeEditorModule()
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

            var module = new CodeEditorModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                code: "int main()");

            exercise.AddModule(module);

            exercise.Modules
                .Should()
                .Contain(module);
        }

        [Fact]
        public void AddModule_Success_AddModulesToModuleListGivenQuizModule()
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

            var module = new QuizModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1);

            exercise.AddModule(module);

            exercise.Modules
                .Should()
                .Contain(module);
        }

        [Fact]
        public void AddModule_Success_AddModulesToModuleListGivenTextModule()
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

            var module = new TextModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                text: "test");

            exercise.AddModule(module);

            exercise.Modules
                .Should()
                .Contain(module);
        }

        [Fact]
        public void AddModule_Fails_ThrowsExerciseExceptionWhenModuleIsNull()
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
            
            Action act = () => exercise.AddModule(null);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void RemoveModuleById_Success_RemovesCodeEditorModuleFromModuleListGivenCodeEditorModuleIdAndCodeEditorModuleIsInModuleList(int moduleId)
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

            var module = new CodeEditorModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                code: "test");
            module.Id = moduleId;

            exercise.AddModule(module);
            exercise.RemoveModuleById(module.Id);

            exercise.Modules
                .Should()
                .NotContain(module);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void RemoveModuleById_Success_RemovesQuizModuleFromModuleListGivenCodeEditorModuleIdAndCodeEditorModuleIsInModuleList(int moduleId)
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

            var module = new QuizModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1);
            module.Id = moduleId;

            exercise.AddModule(module);
            exercise.RemoveModuleById(module.Id);

            exercise.Modules
                .Should()
                .NotContain(module);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void RemoveModuleById_Success_RemovesTextModuleFromModuleListGivenCodeEditorModuleIdAndCodeEditorModuleIsInModuleList(int moduleId)
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

            var module = new TextModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                text: "test");
            module.Id = moduleId;

            exercise.AddModule(module);
            exercise.RemoveModuleById(module.Id);

            exercise.Modules
                .Should()
                .NotContain(module);
        }

        [Fact]
        public void RemoveModuleById_Fails_ThrowsExerciseExceptionGivenIncorrectModuleIdAndModuleListIsEmpty()
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

            Action act = () => exercise.RemoveModuleById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void RemoveModuleById_Fails_ThrowsExerciseExceptionGivenIncorrectModuleIdAndModuleListIsNotEmpty()
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

            var module = new TextModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                text: "test");

            exercise.AddModule(module);

            Action act = () => exercise.RemoveModuleById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetModuleId_Success_ReturnsCorrectModuleGivenModuleIsAddedToTheModuleList(int moduleId)
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

            var module = new TextModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                text: "test");
            module.Id = moduleId;

            exercise.AddModule(module);

            var result = exercise.GetModuleById(moduleId);

            result
                .Should()
                .Be(module);
            result.Id
                .Should()
                .Be(moduleId);
        }

        [Fact]
        public void GetModuleId_Fails_ThrowsExerciseExceptionGivenModuleIdIsNotInModuleListAndModuleListIsEmpty()
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

            Action act = () => exercise.GetModuleById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void GetModuleId_Fails_ThrowsExerciseExceptionGivenModuleIdIsNotInModuleListAndModuleListIsNotEmpty()
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

            var module = new TextModule(
                description: "Test",
                height: 10.00,
                width: 10.00,
                position: 1,
                text: "test");
            module.Id = 0;
            exercise.AddModule(module);

            Action act = () => exercise.GetModuleById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void AddSolution_Success_GivenCorrectSolutionAndSolutionIsAddedToSolutionList()
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

            var solution = new Solution(
                exerciseId: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow);

            exercise.AddSolution(solution);

            exercise.Solutions
                .Should()
                .Contain(solution)
                .And
                .HaveCount(1);
        }

        [Fact]
        public void AddSolution_Fails_ThrowsExerciseExceptionGivenSolutionIsNull()
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

            Action act = () => exercise.AddSolution(null);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void RemoveSolutionById_Success_RemovesSolutionFromSolutionListGivenSolutionIsInSolutionList()
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

            var solution = new Solution(
                exerciseId: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow);
            solution.Id = 0;
            exercise.AddSolution(solution);

            exercise.RemoveSolutionById(0);

            exercise.Solutions
                .Should()
                .NotContain(solution);
        }

        [Fact]
        public void RemoveSolutionById_Fails_ThrowsExerciseExceptionGivenSolutionIdWhichDoesNotExistAndSolutionsListIsEmpty()
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

            Action act = () => exercise.RemoveSolutionById(0);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void RemoveSolutionById_Fails_ThrowsExerciseExceptionGivenSolutionIdWhichDoesNotExistAndSolutionsListIsNotEmpty()
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

            var solution = new Solution(
                exerciseId: 0,
                isVisible: true,
                visibleFromDate: DateTime.UtcNow);
            solution.Id = 0;
            exercise.AddSolution(solution);

            Action act = () => exercise.RemoveSolutionById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void AddSubmission_Success_GivenCorrectSubmission()
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

            var submission = new Submission(
                userId: 0,
                exerciseId: 0,
                isSubmitted: true,
                title: "Test");

            exercise.AddSubmission(submission);

            exercise.Submissions
                .Should()
                .Contain(submission)
                .And
                .HaveCount(1);
        }

        [Fact]
        public void AddSubmission_Fails_GivenSubmissionIsNull()
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

            Action act = () => exercise.AddSubmission(null);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void RemoveSubmissionById_Success_GivenCorrectSubmissionIdAndSubmissionIsAddedToSubmissionList(int submissionId)
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

            var submission = new Submission(
                userId: 0,
                exerciseId: 0,
                isSubmitted: true,
                title: "Test");
            submission.Id = submissionId;
            exercise.AddSubmission(submission);

            exercise.RemoveSubmissionById(submissionId);

            exercise.Submissions
                .Should()
                .NotContain(submission);
        }

        [Fact]
        public void RemoveSubmissionById_Fails_ThrowsExerciseExceptionGivenSubmissionIdDoesNotExistAndSubmissionListIsEmpty()
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

            Action act = () => exercise.RemoveSubmissionById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void RemoveSubmissionById_Fails_ThrowsExerciseExceptionGivenSubmissionIdDoesNotExistAndSubmissionListIsNotEmpty()
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

            var submission = new Submission(
                userId: 0,
                exerciseId: 0,
                isSubmitted: true,
                title: "Test");
            submission.Id = 0;
            exercise.AddSubmission(submission);

            Action act = () => exercise.RemoveSubmissionById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetSubmissionById_Success_GivenCorrectSubmissionIdWhicIsInSubmissionList(int submissionId)
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

            var submission = new Submission(
                userId: 0,
                exerciseId: 0,
                isSubmitted: true,
                title: "Test");
            submission.Id = submissionId;
            exercise.AddSubmission(submission);

            var result = exercise.GetSubmissionById(submissionId);

            result
                .Should()
                .Be(result);
            result.Id
                .Should()
                .Be(submissionId);
        }

        [Fact]
        public void GetSubmissionById_Fails_ThrowsExerciseExceptionGivenSubmissionIdDoesNotExistInSubmissionListAndSubmissionListIsEmpty()
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

            Action act = () => exercise.GetSubmissionById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }

        [Fact]
        public void GetSubmissionById_Fails_ThrowsExerciseExceptionGivenSubmissionIdDoesNotExistInSubmissionListAndSubmissionListIsNotEmpty()
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

            var submission = new Submission(
                userId: 0,
                exerciseId: 0,
                isSubmitted: true,
                title: "Test");
            submission.Id = 0;
            exercise.AddSubmission(submission);

            Action act = () => exercise.GetSubmissionById(1);

            act
                .Should()
                .Throw<ExerciseException>();
        }
    }
}
