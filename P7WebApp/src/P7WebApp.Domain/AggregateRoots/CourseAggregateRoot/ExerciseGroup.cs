using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class ExerciseGroup : EntityBase
    {

        public ExerciseGroup() { }
        public ExerciseGroup(string title, string description, bool isVisible, int exerciseGroupNumber, DateTime createdDate, DateTime lastModifiedDate, DateTime visibleFromDate, List<Exercise> exercises)
        {
            Title = title;
            Description = description;
            ExerciseGroupNumber = exerciseGroupNumber;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate;
            Exercises = exercises;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ExerciseGroupNumber { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public bool IsVisible { get; private set; }
        public DateTime VisibleFromDate { get; private set; }
        public List<Exercise> Exercises { get; private set; }

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

        public void EditInformation(string newTitle, string newDescription, int newExerciseGroupNumber, DateTime newBecomeVisibleAt)
        {
            throw new NotImplementedException();
        }

        public void AddExercise(Exercise newExercise)
        {
            throw new NotImplementedException();
        }

        public void RemoveExercise(int exerciseId)
        {
            throw new NotImplementedException();
        }
    }
}
