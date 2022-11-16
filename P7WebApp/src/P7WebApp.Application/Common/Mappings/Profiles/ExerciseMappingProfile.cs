using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.ExerciseQCRS.Commands;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            // I dont know what these do - help <3
            CreateMap<UpdateExerciseCommand, Exercise>();
            CreateMap<CreateSolutionCommand, Solution>();
            CreateMap<DeleteSolutionCommand, Solution>();
            CreateMap<CreateSubmissionCommand, Submission>();
            CreateMap<DeleteSubmissionCommand, Submission>();
        }
    }
}
