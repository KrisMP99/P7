using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.TextModule
{
    public class UpdateTextModuleCommand : UpdateModuleCommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}