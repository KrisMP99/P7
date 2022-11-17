using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule
{
    public class Choice : EntityBase
    {
        public int QuestionId { get; private set; }
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }

        public void UpdateInformation()
        {
            throw new NotImplementedException();
        }
    }
}
