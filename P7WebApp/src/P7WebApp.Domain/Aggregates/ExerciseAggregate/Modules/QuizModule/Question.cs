using P7WebApp.Domain.Common;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule
{
    public class Question : EntityBase
    {
        public Question(int quizModuleId, string text)
        {
            QuizModuleId = quizModuleId;
            Text = text;
        }

        public int QuizModuleId { get; private set; }
        public string Text { get; private set; }
        public List<Choice> Choices { get; private set; }
    }
}