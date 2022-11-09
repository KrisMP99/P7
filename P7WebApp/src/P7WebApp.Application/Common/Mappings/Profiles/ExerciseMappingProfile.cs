using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            // I dont know what these do - help <3
            CreateMap<UpdateExerciseCommand, Exercise>();
            CreateMap<CreateExerciseModuleCommand, ExerciseResponse>();
            CreateMap<DeleteExerciseModuleCommand, ExerciseResponse>();
            CreateMap<CreateSolutionCommand, ExerciseResponse>();
            CreateMap<DeleteSolutionCommand, ExerciseResponse>();
            CreateMap<CreateSubmissionCommand, ExerciseResponse>();
            CreateMap<DeleteSubmissionCommand, ExerciseResponse>();
        }
    }
}
