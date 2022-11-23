using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.ExerciseCQRS.Commands;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            CreateMap<UpdateExerciseCommand, Exercise>();
            CreateMap<CreateSolutionCommand, Solution>();
            CreateMap<CreateSubmissionCommand, Submission>();
            CreateMap<DeleteSubmissionCommand, Submission>();
            CreateMap<CreateExerciseCommand, Exercise>();
            CreateMap<Exercise, ExerciseOverviewResponse>();
            CreateMap<Exercise, ExerciseResponse>();
        }
    }
}
