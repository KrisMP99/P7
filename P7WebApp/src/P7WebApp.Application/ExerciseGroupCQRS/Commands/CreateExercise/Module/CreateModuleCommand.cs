using Newtonsoft.Json;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module
{
    [JsonConverter(typeof(PolymorphicModuleConverter))]
    public abstract class CreateModuleCommand
    {
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
    }
}