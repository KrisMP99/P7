using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.QuizModule
{
    public class CreateQuizModuleCommand : CreateModuleCommand
    {
        public CreateQuizModuleCommand(string description, double height, double width, int position) : base(description, height, width, position)
        {
        }
    }
}
