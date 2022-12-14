using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using P7WebApp.Application.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace P7WebApp.API.Tests.IntegrationTests.EndpointTests
{
    public class CourseEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public CourseEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void GetCourseEndpoint()
        {
            var client = _factory.CreateClient().RequestAuthorizationCodeTokenAsync();

            var response = await client.GetAsync("api/courses/public");
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //var courses = await client.GetFromJsonAsync<IEnumerable<CourseResponse>>(client);

            //Assert.NotNull(courses);
        }
    }
}
