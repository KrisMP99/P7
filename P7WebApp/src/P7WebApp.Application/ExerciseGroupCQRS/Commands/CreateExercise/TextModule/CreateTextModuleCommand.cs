using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule
{
    public class CreateTextModuleCommand : CreateModuleCommand
    {
        public CreateTextModuleCommand(string description, double height, double width, int position, string text) : base(description, height, width, position)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
