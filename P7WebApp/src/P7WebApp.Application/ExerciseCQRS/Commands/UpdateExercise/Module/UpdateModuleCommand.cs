using Newtonsoft.Json;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module
{
    [JsonConverter(typeof(PolymorphicModuleConverter))]
    public abstract class UpdateModuleCommand
    {
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
    }
}