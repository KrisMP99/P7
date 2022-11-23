using MediatR;


namespace P7WebApp.Application.ExerciseCQRS.Commands.DeleteSolution
{
    public class DeleteSolutionCommand : IRequest<int>
    {
        public int SolutionId { get; set; }
        public int ExerciseId { get; set; }
    }
}
