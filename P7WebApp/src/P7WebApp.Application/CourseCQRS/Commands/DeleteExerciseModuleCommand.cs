using MediatR;

namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class DeleteExerciseModuleCommand : IRequest<int>
    {
        public DeleteExerciseModuleCommand(int moduleId, int exerciseId)
        {
            ModuleId = moduleId;
        }
        public int ModuleId { get; set; }
        
    }
}
