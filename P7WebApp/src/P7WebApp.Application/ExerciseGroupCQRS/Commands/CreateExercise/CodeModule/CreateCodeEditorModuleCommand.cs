using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.CodeModule
{
    public class CreateCodeEditorModuleCommand : CreateModuleCommand
    {
        public CreateCodeEditorModuleCommand(string description, double height, double width, int position, string code) : base(description, height, width, position)
        {
            Code = code;
        }

        public string Code { get; }
    }
}