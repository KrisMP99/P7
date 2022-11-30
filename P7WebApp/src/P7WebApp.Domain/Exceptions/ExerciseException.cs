namespace P7WebApp.Domain.Exceptions
{
    public class ExerciseException : Exception
    {
        private string _newDescription;

        public ExerciseException(string? message) : base(message)
        {
        }
    }
}
