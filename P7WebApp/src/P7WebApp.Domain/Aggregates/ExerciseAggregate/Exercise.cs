using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Aggregates.ExerciseAggregate
{
    public class Exercise : EntityBase, IAggregateRoot
    {
        public Exercise() { }
        public Exercise(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, DateTime createdDate, DateTime lastModifiedDate)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
        }

        public int ExerciseGroupId { get; private set; }

        public string Title { get; private set; }
        public bool IsVisible { get; private set; }
        public int ExerciseNumber { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? VisableFrom { get; set; }
        public DateTime? VisibleTo { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public List<Module>? Modules { get; private set; }
        public List<Solution>? Solution { get; private set; }
        public List<Submission>? Submissions { get; private set; }
        public int LayoutId { get; private set; }

        public void UpdateExerciseInformation(string newTitle, bool visibility, int exerciseNumber, DateTime? newStartDate, DateTime? newEndDate)
        {
            // handle case that newstartdate or newenddate is null
            throw new NotImplementedException();
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
                    throw new Exception("Could not add modules");
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
                    Solution.Add(solution);
                }
                else
                {
                    throw new Exception("Could not create solution");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteSolution(int solutionId)
        {
            try
            {
                Solution.Remove(GetSolution(solutionId));
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public Solution GetSolution(int solutionId)
        {
            throw new NotImplementedException();
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
                    throw new Exception("Could not create submission");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteSubmission(int submissionId)
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
            throw new NotImplementedException();
        }
    }
}