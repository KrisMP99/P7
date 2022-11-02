using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.SharedKernel;


namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Solution : EntityBase
    {
        public Solution(bool isVisible, DateTime visibleFromDate) 
        { 
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate;
        }

        public bool IsVisible { get; set; }
        public DateTime VisibleFromDate { get; set; }
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
