using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Domain.Repositories
{
    public interface IExerciseRepository
    {
        Task<int> UpdateExercise(Exercise exercise);
        Task<Exercise> GetExerciseById(int id);


        Task<int> CreateModule(Module module);
        Task<int> DeleteModule(Module module);
        Task<Exercise> GetExerciseFromModuleId(int moduleId); 


        Task<int> CreateSolution(Solution solution);
        Task<int> DeleteSolution(Solution solution);
        Task<Exercise> GetExerciseFromSolutionId(int id);


        Task<int> Createsubmission(Submission submission);
        Task<int> DeleteSubmission(Submission submission);
        Task<Exercise> GetExerciseFromSubmissionId(int id);


    }
}
