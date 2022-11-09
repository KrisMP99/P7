using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule
{
    public class TestCase : EntityBase
    {
        public int CodeEditorModuleId { get; private set; }
        public string Test { get; set; }
    }
}
