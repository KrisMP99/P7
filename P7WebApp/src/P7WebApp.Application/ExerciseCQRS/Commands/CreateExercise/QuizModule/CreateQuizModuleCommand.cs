using P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.QuixModule
{
    public class CreateQuizModuleCommand : CreateModuleCommand
    {
        public CreateQuizModuleCommand(string description, double height, double width, int position) : base(description, height, width, position)
        {
        }
    }
}
