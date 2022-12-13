using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Exceptions;

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
                    throw new ExerciseGroupException("Could not find an exercise with the specified id");
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

        public void EditInformation(string newTitle, string newDescription, int newExerciseGroupNumber, bool isVisible, DateTime? newBecomeVisibleAt)
        {
            Title = String.IsNullOrEmpty(newTitle) ? throw new ExerciseGroupException("Title has not been set.") : newTitle;
            Description = String.IsNullOrEmpty(newDescription) ? throw new ExerciseGroupException("Description has not been set.") : newDescription;
            ExerciseGroupNumber = newExerciseGroupNumber < 0 ? throw new ExerciseGroupException("Exercisegroup number cannot be negative.") : newExerciseGroupNumber;
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
                    throw new ExerciseGroupException("Could not add exercise (Exercise is null)");
                }

                newExercise.SetExerciseNumber(this.Exercises.Count + 1);

                if(CheckExerciseNumberIsOk(newExercise))
                {
                    Exercises.Add(newExercise);
                }
            } 
            catch (Exception)
            {
                throw;
            }
        }

        // Checks if the exercise number is correct, i.e., the order of the exercises is correct
        private bool CheckExerciseNumberIsOk(Exercise newExercise)
        {
            if (newExercise.ExerciseNumber < 0)
            {
                throw new ExerciseGroupException("The exercise number cannot be less than 0.");
            }

            var result = Exercises.Find(e => e.ExerciseNumber == newExercise.ExerciseNumber);

            if (result is not null)
            {
                throw new ExerciseGroupException($"An exercise with exercise number {newExercise.ExerciseNumber} already exists.");
            }

            return true;
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

        public void SetGroupExerciseNumber(int number)
        {
            try
            {
                if (number <= 0)
                {
                    throw new ExerciseGroupException("Exercise group number cannot be 0 or less");
                }

                this.ExerciseGroupNumber = number;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}