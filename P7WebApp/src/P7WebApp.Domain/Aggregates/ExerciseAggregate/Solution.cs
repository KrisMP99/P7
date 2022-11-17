using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Common;


namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Solution : EntityBase
    {
        public Solution(int exerciseId, bool isVisible, DateTime? visibleFromDate)
        {
            ExerciseId = exerciseId;
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate ?? DateTime.UtcNow;
        }

        public int ExerciseId { get; private set; }
        public bool IsVisible { get; set; }
        public DateTime? VisibleFromDate { get; set; }
        public List<Module> Modules { get; set; }

        public void ChangeVisibility(bool isVisible)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisibilityDate(DateTime visisbleFromDate)
        {
            throw new NotImplementedException();
        }

        public void AddModule()
        {
            throw new NotImplementedException();
        }

        public void DeleteModule()
        {
            throw new NotImplementedException();
        }

    }
}