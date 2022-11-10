using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule
{
    public class TextModule : Module
    {
        public TextModule(string description, double height, double width, int position) : base(description, height, width, position)
        {
        }
        public string Text { get; set; }
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
