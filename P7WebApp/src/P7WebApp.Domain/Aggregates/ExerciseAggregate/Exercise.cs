using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Exercise : EntityBase, IAggregateRoot
    {
        public Exercise(int exerciseGroupId, string title, bool isVisible, int exerciseNumber, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId)
        {
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate ?? DateTime.UtcNow;
            EndDate = endDate ?? DateTime.MaxValue;
            VisibleFrom = visibleFrom ?? DateTime.UtcNow;
            VisibleTo = visibleTo ?? DateTime.MaxValue;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
            LayoutId = layoutId;
            Modules = new List<Module>();
            Solutions = new List<Solution>();
            Submissions = new List<Submission>();
        }

        public int ExerciseGroupId { get; private set; }
        public string Title { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseNumber { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? VisibleFrom { get; private set; }
        public DateTime? VisibleTo { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public List<Module> Modules { get; private set; }
        public List<Solution> Solutions { get; private set; }
        public List<Submission> Submissions { get; private set; }
        public int LayoutId { get; private set; }

        public void EditInformation(string newTitle, bool newIsVisible, int newExerciseNumber, DateTime? newStartDate, DateTime? newEndDate, int newLayoutId)
        {
            Title = String.IsNullOrEmpty(newTitle) ? throw new ExerciseException("Title has not been set.") : newTitle;
            IsVisible = newIsVisible;
            ExerciseNumber = newExerciseNumber < 0 ? throw new ExerciseException("Exercise number cannot be negative.") : newExerciseNumber;
            VisibleFrom = newStartDate ?? VisibleFrom;
            VisibleTo = newEndDate ?? VisibleTo;
            LastModifiedDate = DateTime.Now;
            LayoutId = ExerciseLayout.FromId(newLayoutId).Id;
        }

        public void AddModule(Module module)
        {
            try
            {
                if (module is not null)
                {
                    Modules.Add(module);
                } 
                else
                {
                    throw new ExerciseException("Could not add modules");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteModule(int moduleId)
        {
            try
            {
                Modules.Remove(GetModule(moduleId));
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public Module GetModule(int moduleId)
        {
            throw new NotImplementedException();
        }

        public void AddSolution(Solution solution)
        {
            try
            {
                if (solution is not null)
                {
                    Solutions.Add(solution);
                }
                else
                {
                    throw new ExerciseException("Could not create solution");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveSolution(int solutionId)
        {
            try
            {
                Solutions.Remove(GetSolution(solutionId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Solution GetSolution(int solutionId)
        {
            try
            {
                var result = Solutions.Where(s => s.Id == solutionId).FirstOrDefault();
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddSubmission(Submission submission)
        {
            try
            {
                if (submission is not null)
                {
                    Submissions.Add(submission);
                }
                else
                {
                    throw new ExerciseException("Could not create submission");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void RemoveSubmission(int submissionId)
        {
            try
            {
                Submissions.Remove(GetSubmission(submissionId));
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public Submission GetSubmission(int submissionId)
        {
            try
            {
                var result = Submissions.Where(s => s.Id == submissionId).FirstOrDefault();
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}