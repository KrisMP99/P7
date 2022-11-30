using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.CodeModule
{
    public class UpdateCodeEditorModuleCommand : UpdateModuleCommand
    {
        public string Code { get; set; }
    }
}