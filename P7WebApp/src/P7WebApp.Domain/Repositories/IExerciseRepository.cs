using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;


namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<int> UpdateExercise(Exercise exercise);
        Task<Exercise> GetExerciseById(int id);


        Task<int> CreateModule(Module module);
        Task<int> DeleteModule(Module module);


        Task<int> CreateSolution(Solution solution);
        Task<int> DeleteSolution(Solution solution);


        Task<int> Createsubmission(Submission submission);
        Task<int> DeleteSubmission(Submission submission);  


    }
}
