using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules
{
    public abstract class Module : EntityBase
    {
        protected Module(string description, double height, double width, int position)
        {
            Description = description;
            Height = height;
            Width = width;
            Position = position;
        }

        public string Description { get; protected set; }
        public double Height { get; protected set; }
        public double Width { get; protected set; }
        public int Position { get; protected set; }

        public virtual void EditInformation(string newDescription, double newHeight, double newWidth, int newPosition)
        {
            Description = !string.IsNullOrEmpty(newDescription) ? newDescription : throw new ExerciseException("Cannot edit to invalid description.");
            Height = newHeight != 0 ? newHeight : throw new ExerciseException("Cannot edit to invalid height.");
            Width = newWidth != 0 ? newWidth : throw new ExerciseException("Cannot edit to invalid width.");
            Position = newPosition != 0 ? newPosition : throw new ExerciseException("Cannot edit to invalid position."); ;
        }
    }
}