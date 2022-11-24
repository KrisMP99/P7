using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.CodeModule
{
    public class CreateCodeEditorModuleCommand : CreateModuleCommand
    {
        public string Code { get; set; }
    }
}