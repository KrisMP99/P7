using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule
{
    public class CreateTextModuleCommand : CreateModuleCommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
