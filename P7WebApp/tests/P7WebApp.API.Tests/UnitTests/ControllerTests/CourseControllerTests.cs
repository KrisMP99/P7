﻿using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7WebApp.API.Controllers;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Domain.AggregateRoots.CourseAggregateRoot;

namespace P7WebApp.Infrastructure.Tests.UnitTests.ControllerTests
{
    public class CourseControllerTests
    {
        [Fact]
        public async Task CreateCourse_ReturnsOK_ResultIsGreaterThanZero()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<OkResult>();

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task CreateCourse_ReturnsBadRequest_ResultIsLessOrEqualToZero(int value)
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(value);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateCourse_ReturnsBadRequest_ResultIsNull()
        {
            var mockMediator = new Mock<IMediator>();
            var courseController = new CourseController(mockMediator.Object);
            var createCourseCommand = new CreateCourseCommand("test", "test", false, new List<Exercise> { });
            mockMediator.Setup(m => m.Send(createCourseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(null);

            var result = await courseController.CreateCourse(createCourseCommand);
            
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}