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
            StartDate = startDate ?? null;
            EndDate = endDate ?? null;
            VisibleFrom = visibleFrom ?? null;
            VisibleTo = visibleTo ?? null;
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
                if (module is null)
                {
                    throw new ExerciseException("Could not add modules");
                } 
                
                if(CanModuleBeAddedToExercise(module))
                {
                    Modules.Add(module);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddModules(ICollection<Module> modules)
        {
            try
            {
                if (modules is not null && modules.Any())
                {
                    Modules.AddRange(modules);
                    
                }
                else
                {
                    throw new Exception("Could not add modules");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CanModuleBeAddedToExercise(Module module)
        {
            var numOfModules = Modules.Count();
            var numberOfModulesAllowed = ExerciseLayout.GetNumberOfModulesAllowed(this.LayoutId);

            if(numOfModules > numberOfModulesAllowed)
            {
                throw new ExerciseException("");
            }

            if(module.Position < 1 || module.Position > 4)
            {
                throw new ExerciseException($"The modules position '{module.Position}' is invalid. Allowed values are: 1-4.");
            }

            var result = Modules.Find(m => m.Position == module.Position);

            if (result is not null)
            {
                throw new ExerciseException($"Module position '{module.Position}' is already assigned to another module.");
            }
            
            return true;
        }

        public void RemoveModuleById(int moduleId)
        {
            try
            {
                Modules.Remove(GetModuleById(moduleId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Module GetModuleById(int moduleId)
        {
            try
            {
                var module = Modules.Find(m => m.Id == moduleId);

                if(module is not null)
                {
                    return module;
                }
                else
                {
                    throw new ExerciseException("Could not find the module with the specified id.");
                }
            }
            catch(Exception)
            {
                throw;
            }
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

        public void RemoveSolutionById(int solutionId)
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
                var result = Solutions.Find(s => s.Id == solutionId);
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    throw new ExerciseException("Could not find a solution with the given solution id.");
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
                    throw new ExerciseException("Could not create submission.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveSubmissionById(int submissionId)
        {
            try
            {
                Submissions.Remove(GetSubmissionById(submissionId));
            }
            catch (Exception)
            {
                throw new ExerciseException("Could not remove submission with the given submission id, as it does not exist in the submission list.");
            }
        }

        public Submission GetSubmissionById(int submissionId)
        {
            try
            {
                var result = Submissions.Find(s => s.Id == submissionId);
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    throw new ExerciseException("Could not find submission with the given submission id.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}