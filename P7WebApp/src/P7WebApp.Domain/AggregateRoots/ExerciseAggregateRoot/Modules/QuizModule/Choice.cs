using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.QuizModule
{
    public class Choice : EntityBase
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public void UpdateInformation()
        {
            throw new NotImplementedException();
        }
    }
}
