using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class ExerciseGroup : EntityBase
    {
        public ExerciseGroup() { }
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

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime BecomeVisibleAt { get; set; }
        public List<Exercise> Exercises { get; set; }

        public Exercise GetExercise(int exerciseId)
        {
            try
            {
                var exercise = Exercises.Find(e => e.Id == exerciseId);

                if (exercise is not null)
                {
                    return exercise;
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

        public void UpdateInformation(string newTitle, string newDescription, int newExerciseGroupNumber, DateTime newBecomeVisibleAt)
        {
            Title = string.IsNullOrEmpty(newTitle) ? Title : Title == newTitle ? Title : newTitle;
            Description = string.IsNullOrEmpty(newDescription) ? Description : Description == newDescription ? Description : newDescription;
            ExerciseGroupNumber = newExerciseGroupNumber;
            BecomeVisibleAt = newBecomeVisibleAt;
        }
    }
}
