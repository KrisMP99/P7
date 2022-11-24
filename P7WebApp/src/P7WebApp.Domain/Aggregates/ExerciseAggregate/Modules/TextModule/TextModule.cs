using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule
{
    public class TextModule : Module
    {
        public TextModule(string description, double height, double width, int position, string title, string content) : base(description, height, width, position)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public List<Image> Images { get; private set; }

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