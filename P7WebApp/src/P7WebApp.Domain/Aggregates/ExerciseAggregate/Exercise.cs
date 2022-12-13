using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;
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
        public ICollection<Module> Modules { get; private set; } = new List<Module>();
        public ICollection<Solution> Solutions { get; private set; } = new List<Solution>();
        public ICollection<Submission> Submissions { get; private set; } = new List<Submission>();
        public int LayoutId { get; private set; }

        public void EditInformation(string newTitle, bool newIsVisible, int newExerciseNumber, DateTime? newStartDate, DateTime? newEndDate, int newLayoutId, List<Module> newModules)
        {
            try
            {
                Title = String.IsNullOrEmpty(newTitle) ? throw new ExerciseException("Title has not been set.") : newTitle;
                IsVisible = newIsVisible;
                ExerciseNumber = newExerciseNumber < 0 ? throw new ExerciseException("Exercise number cannot be negative.") : newExerciseNumber;
                VisibleFrom = newStartDate ?? VisibleFrom;
                VisibleTo = newEndDate ?? VisibleTo;
                LastModifiedDate = DateTime.UtcNow;
                LayoutId = ExerciseLayout.FromId(newLayoutId).Id;

                // update existing modules
                var modulesToUpdate = newModules.Where(nm => nm.Id != 0).ToList();
                if (modulesToUpdate is not null && modulesToUpdate.Count != 0)
                {
                    foreach (var module in modulesToUpdate)
                    {
                        if (module is TextModule)
                        {
                            TextModule newTextModule = module as TextModule;
                            var updatedModule = GetModuleById(module.Id) as TextModule;
                            updatedModule.EditInformation(newDescription: newTextModule.Description, newHeight: newTextModule.Height, newWidth: newTextModule.Width, newPosition: newTextModule.Position, newTitle: newTextModule.Title, newTextModule.Content);
                        }
                        else if (module is CodeEditorModule)
                        {
                            CodeEditorModule newCodeEditorModule = module as CodeEditorModule;
                            var updatedModule = GetModuleById(module.Id) as CodeEditorModule;
                            updatedModule.EditInformation(newDescription: newCodeEditorModule.Description, newHeight: newCodeEditorModule.Height, newWidth: newCodeEditorModule.Width, newPosition: newCodeEditorModule.Position, newCode: newCodeEditorModule.Code);
                        }
                        else if (module is QuizModule)
                        {
                            QuizModule newQuizModule = module as QuizModule;
                            var updatedModule = GetModuleById(module.Id) as QuizModule;
                            updatedModule.EditInformation(newDescription: newQuizModule.Description, newHeight: newQuizModule.Height, newWidth: newQuizModule.Width, newPosition: newQuizModule.Position);
                        }
                        else
                        {
                            throw new ExerciseException("Could not recognize the module type.");
                        }
                    }
                }

                // if modules have been deleted
                if (Modules.Count > newModules.Count)
                {
                    var module = Modules.Where(m => !newModules.Exists(nm => nm.Id == m.Id)).FirstOrDefault();

                    if (module is not null)
                    {
                        Modules.Remove(module);

                    }
                }

                // add new modules
                if (newModules.Any(nm => nm.Id == 0))
                {
                    var modules = newModules.Where(nm => nm.Id == 0).ToList();
                    AddModules((ICollection<Module>)(modules));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddModule(Module module)
        {
            try
            {
                if (module is null)
                {
                    throw new ExerciseException("Could not add modules");
                }

                if (CanModuleBeAddedToExercise(module))
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
                if (modules is null)
                {
                    throw new ExerciseException("The modules collection is null.");

                }

                if (modules.Count() > 4)
                {
                    throw new ExerciseException("An exercise can at most contain 4 modules.");
                }

                if (modules.Count() == 0)
                {
                    throw new ExerciseException("The module collection to add is empty.");
                }

                foreach (var module in modules)
                {
                    if (CanModuleBeAddedToExercise(module))
                    {
                        Modules.Add(module);
                    }
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

            if (numOfModules > numberOfModulesAllowed)
            {
                throw new ExerciseException("");
            }

            if (module.Position < 1 || module.Position > 4)
            {
                throw new ExerciseException($"The modules position '{module.Position}' is invalid. Allowed values are: 1-4.");
            }

            var result = Modules.Where(m => m.Position == module.Position).FirstOrDefault();

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
                var module = Modules.Where(m => m.Id == moduleId).FirstOrDefault();

                if (module is not null)
                {
                    return module;
                }
                else
                {
                    throw new ExerciseException("Could not find the module with the specified id.");
                }
            }
            catch (Exception)
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
                var result = Solutions.Where(s => s.Id == solutionId).FirstOrDefault();
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
                var result = Submissions.Where(s => s.Id == submissionId).FirstOrDefault();

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

        public void SetExerciseNumber(int number)
        {
            try
            {
                if (number <= 0)
                {
                    throw new ExerciseException("Exercise number cannot be 0 or less");
                }

                this.ExerciseNumber = number;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}