using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;


namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<Exercise> EditExerciseInformation(Exercise exercise);
        Task<Module> AddModule(int moduleId);
        Task<Module> DeleteModule(Module module);
        Task<Solution> AddSolution(Solution solution);
        Task<Solution> DeleteSolution(Solution solution);
        Task<Submission> AddSubmission(Submission submission);
        Task<Submission> DeleteSubmission(Submission submission);
    }
}
