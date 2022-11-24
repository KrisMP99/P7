using MediatR;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;
using System.Runtime.InteropServices;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise
{
    public class UpdateExerciseCommand : IRequest<int>
    {
        public UpdateExerciseCommand(int id, int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId, List<CreateModuleCommand> modules)
        {
            Id = id;
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            VisibleFrom = visibleFrom;
            VisibleTo = visibleTo;
            LayoutId = layoutId;
            Modules = modules;

        }

        public int Id { get; }
        public int ExerciseGroupId { get; }
        public string Title { get; }
        public bool IsVisible { get; }
        public int ExerciseNumber { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public DateTime? VisibleFrom { get; }
        public DateTime? VisibleTo { get; }
        public int LayoutId { get; }
        public List<CreateModuleCommand>? Modules { get; }
    }
}