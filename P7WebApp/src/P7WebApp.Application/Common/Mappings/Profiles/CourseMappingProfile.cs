using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseResponse>();
            CreateMap<ExerciseGroup, ExerciseGroupResponse>();
            CreateMap<CreateInviteCodeCommand, InviteCode>();
            CreateMap<CreateExerciseGroupCommand, ExerciseGroup>();
            CreateMap<Course, CourseOverviewResponse>();
            CreateMap<String, CourseOverviewResponse>().
                ForMember(dest => dest.OwnerName, src => src.ToString());
        }
    }
}
