using MediatR;


namespace P7WebApp.Application.ExerciseQCRS.Commands
{
    public class DeleteSolutionCommand : IRequest<int>
    {
        public int SolutionId { get; set; }
    }
}
