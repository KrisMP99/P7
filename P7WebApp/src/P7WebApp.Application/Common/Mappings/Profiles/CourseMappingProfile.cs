﻿using AutoMapper;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}