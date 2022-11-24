using MediatR;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommand : IRequest<int>
    {
        public CreateExerciseCommand(int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId, List<string> hello)
        {
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            VisibleFrom = visibleFrom;
            VisibleTo = visibleTo;
            LayoutId = layoutId;
            Hellos = hello;
        }

        public int ExerciseGroupId { get; }
        public string Title { get; }
        public bool IsVisible { get; }
        public int ExerciseNumber { get; }
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public DateTime? VisibleFrom { get; }
        public DateTime? VisibleTo { get; }
        public int LayoutId { get; }
        public List<string> Hellos { get; }
        //public ICollection<CreateModuleCommand>? Modules { get; set; }
    }
}
