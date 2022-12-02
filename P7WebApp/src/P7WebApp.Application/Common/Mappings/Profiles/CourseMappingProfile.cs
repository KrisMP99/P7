using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands.CreateCourse;
using P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup;
using P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise;
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
            CreateMap<Exercise, ExerciseOverviewResponse>();
            CreateMap<Attendee, AttendeeResponse>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(src => src.ProfileId))
            .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.Profile.FirstName))
            .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.Profile.LastName))
            .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.Profile.UserName));
            CreateMap<ExerciseGroup, ExerciseGroupResponse>()
                .ForMember(dest => dest.Exercises, src => src.MapFrom(src => src.Exercises));
            CreateMap<Course, CourseResponse>()
                .ForMember(dest => dest.ExerciseGroups, src => src.MapFrom(src => src.ExerciseGroups))
                .ForMember(dest => dest.Attendees, src => src.MapFrom(src => src.Attendees));
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<CreateInviteCodeCommand, InviteCode>();
            CreateMap<CreateExerciseGroupCommand, ExerciseGroup>();
            CreateMap<Course, CourseOverviewResponse>();
            CreateMap<CreateExerciseCommand, Exercise>();
        }
    }
}