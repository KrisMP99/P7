using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class DeleteModuleCommand : IRequest<int>
    {

        public int ModuleId { get; set; }
        
    }
}
