using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule
{
    public class CodeEditorModule : Module
    {
        public CodeEditorModule(string description, double height, double width, int position, string code) : base(description, height, width, position)
        {
            Code = code;
        }
        public string Code { get; private set; }
        public List<TestCase> TestCases { get; private set; }

        public void EditInformation(string newDescription, double newHeight, double newWidth, int newPosition, string newCode)
        {
            base.Description = !string.IsNullOrEmpty(newDescription) ? newDescription : throw new ExerciseException("Cannot edit to invalid description.");
            base.Height = newHeight != 0 ? newHeight : throw new ExerciseException("Cannot edit to invalid height.");
            base.Width = newWidth != 0 ? newWidth : throw new ExerciseException("Cannot edit to invalid width.");
            base.Position = newPosition != 0 ? newPosition : throw new ExerciseException("Cannot edit to invalid position."); ;
            this.Code = !string.IsNullOrEmpty(newCode) ? newCode : throw new ExerciseException("Cannot edit to invalid code.");
        }

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
