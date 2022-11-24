using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.CodeModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.QuizModule;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.TextModule;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module
{
    public class PolymorphicModuleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CreateModuleCommand);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            CreateModuleCommand createModuleCommand;

            var pt = obj["type"];

            if (pt == null)
            {
                throw new ArgumentException("Missing type", "type");
            }

            string moduleType = pt.Value<string>();

            if (moduleType == "code")
            {
                createModuleCommand = new CreateCodeEditorModuleCommand();
            }
            else if (moduleType == "text")
            {
                createModuleCommand = new CreateTextModuleCommand();
            }
            else if (moduleType == "quiz")
            {
                createModuleCommand = new CreateQuizModuleCommand();
            }
            else
            {
                throw new NotSupportedException("Unknown module type: " + moduleType);
            }

            serializer.Populate(obj.CreateReader(), createModuleCommand);
            return createModuleCommand;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
