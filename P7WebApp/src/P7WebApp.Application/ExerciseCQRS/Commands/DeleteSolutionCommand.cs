using MediatR;


namespace P7WebApp.Application.ExerciseCQRS.Commands
{
    public class DeleteSolutionCommand : IRequest<int>
    {
        public int SolutionId { get; set; }
    }
}
