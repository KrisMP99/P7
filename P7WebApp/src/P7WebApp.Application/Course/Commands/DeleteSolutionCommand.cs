using MediatR;


namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class DeleteSolutionCommand : IRequest<int>
    {
        public int SolutionId { get; set; }

    }
}
