using Newtonsoft.Json;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module
{
    [JsonConverter(typeof(PolymorphicUpdateModuleConverter))]
    public abstract class UpdateModuleCommand
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
    }
}