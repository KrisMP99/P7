﻿using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public async Task<int> AddCourse(Course course)
        {
            return 1;
        }

        public async Task<Course> GetCourse(int id)
        {
            return new Course() { Id = id};
        }
    }
}
