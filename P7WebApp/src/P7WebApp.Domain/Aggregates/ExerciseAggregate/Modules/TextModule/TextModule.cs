using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule
{
    public class TextModule : Module
    {
        public TextModule(string description, double height, double width, int position, string text) : base(description, height, width, position)
        {
            Text = text;
        }
        public string Text { get; private set; }
        public List<Image> Images;

        public void AddImage()
        {
            throw new NotImplementedException();
        }

        public void RemoveImage()
        {
            throw new NotImplementedException();
        }
    }
}