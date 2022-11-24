namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateExercise.Module
{
    public abstract class CreateModuleCommand
    {
        protected CreateModuleCommand(string description, double height, double width, int position)
        {
            Description = description;
            Height = height;
            Width = width;
            Position = position;
        }

        public string Description { get; }
        public double Height { get; }
        public double Width { get; }
        public int Position { get; }
    }
}