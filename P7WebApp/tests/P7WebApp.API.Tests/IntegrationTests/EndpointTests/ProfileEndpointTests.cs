using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.API.Tests.IntegrationTests.EndpointTests
{
    public class ProfileEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public ProfileEndpointTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateProfile()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync("/api/profiles",
                new CreateProfileCommand("integrationtest1", "password", "email@emaul.dk", "firstname", "lastname"));

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

    }
}
