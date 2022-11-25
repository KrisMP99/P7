using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
using P7WebApp.Domain.Exceptions;

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


        public void EditInformation(string newDescription, double newHeight, double newWidth, int newPosition, string newTitle, string newContent)
        {
            base.Description = !string.IsNullOrEmpty(newDescription) ? newDescription : throw new ExerciseException("Cannot edit to invalid description.");
            base.Height = newHeight != 0 ? newHeight : throw new ExerciseException("Cannot edit to invalid height.");
            base.Width = newWidth != 0 ? newWidth : throw new ExerciseException("Cannot edit to invalid width.");
            base.Position = newPosition != 0 ? newPosition : throw new ExerciseException("Cannot edit to invalid position."); ;

            this.Title = !string.IsNullOrEmpty(newTitle)  ? newTitle : throw new ExerciseException("Cannot edit to invalid title.");
            this.Content = !string.IsNullOrEmpty(newContent) ? newContent : throw new ExerciseException("Cannot edit to invalid content.");
        }

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