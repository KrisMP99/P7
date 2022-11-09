using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;
using P7WebApp.Domain.AggregateRoots.ExerciseGroupAggregateRoot;
using P7WebApp.Domain.Common;
using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;
using System.Reflection;
using Module = P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules.Module;

namespace P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot
{
    public class Exercise : EntityBase, IAggregateRoot
    {
        public Exercise() { }
        public Exercise(string title, bool isVisible, int exerciseNumber, DateTime startDate, DateTime endDate, DateTime createdDate, DateTime lastModifiedDate, ExerciseLayout layout, List<Solution> solution)
        {
            Title = title;
            IsVisible = isVisible;
            ExerciseNumber = exerciseNumber;
            StartDate = startDate;
            EndDate = endDate;
            CreatedDate = createdDate;
            LastModifiedDate = lastModifiedDate;
            Layout = layout;
            Solution = solution;
        }

        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public ExerciseLayout Layout { get; set; }
        public ExerciseType Type { get; set; }
        public List<Module> Modules { get; set; }
        public List<Solution> Solution { get; set; }
        public List<Submission> Submission { get; set; }

        public void UpdateExerciseInformation(string newTitle, bool visibility, int exerciseNumber, DateTime newStartDate, DateTime newEndDate)
        {
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
                    Submission.Add(submission);
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
                Submission.Remove(GetSubmission(submissionId));
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