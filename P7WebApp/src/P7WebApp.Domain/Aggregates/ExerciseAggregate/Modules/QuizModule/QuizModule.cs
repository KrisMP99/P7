namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule
{
    public class QuizModule : Module
    {
        public QuizModule(string description, double height, double width, int position) : base(description, height, width, position)
        {
            Questions = new List<Question>();
        }

        public List<Question> Questions { get; private set; }

        public void AddQuestion()
        {
            throw new NotImplementedException();
        }

        public void RemoveQuestion()
        {
            throw new NotImplementedException();
        }
    }
}