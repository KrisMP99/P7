using P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.CodeModule
{
    public class CreateCodeModuleCommand : CreateModuleCommand
    {
        public CreateCodeModuleCommand(string description, double height, double width, int position, string code) : base(description, height, width, position)
        {
            Code = code;
        }

        public string Code { get; }
    }
}