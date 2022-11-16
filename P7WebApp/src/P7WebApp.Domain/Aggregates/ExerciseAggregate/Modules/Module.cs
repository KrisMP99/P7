using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;

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

        public string Description { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public int Position { get; private set; }

        public void UpdateModule()
        {
            throw new NotImplementedException();
        }
    }
}
