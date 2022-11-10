using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot.Modules;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            // I dont know what these do - help <3
            CreateMap<UpdateExerciseCommand, Exercise>();
            CreateMap<CreateModuleCommand, Module>();
            CreateMap<DeleteModuleCommand, Module>();
            CreateMap<CreateSolutionCommand, Solution>();
            CreateMap<DeleteSolutionCommand, Solution>();
            CreateMap<CreateSubmissionCommand, Submission>();
            CreateMap<DeleteSubmissionCommand, Submission>();
        }
    }
}
