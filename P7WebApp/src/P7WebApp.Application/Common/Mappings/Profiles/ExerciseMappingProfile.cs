using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.ExerciseAggregateRoot;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile() 
        { 
            CreateMap<UpdateExerciseCommand, Exercise>();
        }
    }
}
