using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;

namespace P7WebApp.Domain.Aggregates.ExerciseGroupAggregate
{
    public class ExerciseGroup : EntityBase, IAggregateRoot
    {
        public ExerciseGroup(int courseId, string title, string description, int exerciseGroupNumber, bool isVisible, DateTime? visibleFromDate)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            ExerciseGroupNumber = exerciseGroupNumber;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate ?? DateTime.UtcNow;
        }

        public int CourseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ExerciseGroupNumber { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public bool IsVisible { get; private set; }
        public DateTime? VisibleFromDate { get; private set; }
        public List<Exercise> Exercises { get; private set; } = new List<Exercise>();

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
                    throw new Exception("Could not find an exercise with the specified id");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Exercise> GetAllExercises()
        {
            return Exercises;
        }

        // should we have null checks here, or is that done in the validators?
        public void EditInformation(string newTitle, string newDescription, int newExerciseGroupNumber, bool isVisible, DateTime newBecomeVisibleAt)
        {
            Title = String.IsNullOrEmpty(newTitle) ? throw new ArgumentNullException("Title has not been set.") : newTitle;
            Description = String.IsNullOrEmpty(newDescription) ? throw new ArgumentNullException("Description has not been set.") : newDescription;
            ExerciseGroupNumber = ExerciseGroupNumber < 0 ? throw new ArgumentOutOfRangeException("Exercisegroup number cannot be negative.") : newExerciseGroupNumber;
            IsVisible = isVisible;
            VisibleFromDate = newBecomeVisibleAt;
            LastModifiedDate = DateTime.Now;
        }

        public void AddExercise(Exercise newExercise)
        {
            try
            {
                if(newExercise == null)
                {
                    throw new Exception("Could not add exercise (Exercise is null)");
                } 
                else
                {
                    Exercises.Add(newExercise);
                }
            } 
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveExerciseById(int exerciseId)
        {
            try
            {
                Exercises.Remove(GetExercise(exerciseId));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
