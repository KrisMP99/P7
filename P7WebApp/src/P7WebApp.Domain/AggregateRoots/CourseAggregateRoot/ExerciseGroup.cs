using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class ExerciseGroup : EntityBase
    {
        public ExerciseGroup(string title, string description, bool isVisible, int exerciseGroupNumber, DateTime createdDate, DateTime lastModifiedDate, DateTime becomeVisibleAt, List<Exercise> exercises)
        {
            Title = title;
            Description = description;
            IsVisible = isVisible;
            ExerciseGroupNumber = exerciseGroupNumber;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            BecomeVisibleAt = becomeVisibleAt;
            Exercises = exercises;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseGroupNumber { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public DateTime BecomeVisibleAt { get; private set; }
        public List<Exercise> Exercises { get; private set; }

        public Exercise GetExercise(int exerciseId)
        {
            try
            {
                if (Exercises.Any(e => e.Id == exerciseId))
                {
                    return Exercises.Find(e => e.Id == exerciseId);
                }
                else
                {
                    throw new Exception("Could not find an exercise with the specified Id");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
