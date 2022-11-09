using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule
{
    public class Question : EntityBase
    {
        public int QuizModuleId { get; private set; }
        public string Text { get; private set; }
        public List<Choice> Choices { get; private set; }
    }
}
