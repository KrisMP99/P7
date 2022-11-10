namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule
{
    public class CodeEditorModule : Module
    {
        public CodeModule(int exerciseId, string description, double height, double width, int position) : base(exerciseId, description, height, width, position)
        {
        }

        public string Code { get; set; }
        public List<TestCase> TestCases { get; private set; }

        public void RunCode()
        {
            throw new NotImplementedException();
        }

        public void RunTestCase()
        {
            throw new NotImplementedException();
        }

        public void CreateTestCase()
        {
            throw new NotImplementedException();
        }

        public void DeleteTestCase()
        {
            throw new NotImplementedException();
        }
    }
}
