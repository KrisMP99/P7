using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules
{
    public abstract class Module : EntityBase, IAggregateRoot
    {
        public int BelongsToId { get; private set; }
        public string Description { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public int Posititon { get; private set; }

        public void UpdateModule()
        {
            throw new NotImplementedException();
        }
    }
}
