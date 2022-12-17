using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.CodeModule;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.QuizModule;
using P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.TextModule;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.Module
{
    public class PolymorphicUpdateModuleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UpdateModuleCommand);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            UpdateModuleCommand updateModuleCommand;

            var pt = obj["type"];

            if (pt == null)
            {
                throw new ArgumentException("Missing type", "type");
            }

            string moduleType = pt.Value<string>();

            if (moduleType == "code")
            {
                updateModuleCommand = new UpdateCodeEditorModuleCommand();
            }
            else if (moduleType == "text")
            {
                updateModuleCommand = new UpdateTextModuleCommand();
            }
            else if (moduleType == "quiz")
            {
                updateModuleCommand = new UpdateQuizModuleCommand();
            }
            else
            {
                throw new NotSupportedException("Unknown module type: " + moduleType);
            }

            serializer.Populate(obj.CreateReader(), updateModuleCommand);
            return updateModuleCommand;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}