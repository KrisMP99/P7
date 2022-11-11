﻿using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.User.Commands.CreateUser;

namespace P7WebApp.Application.User.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                (Result result, string userId) = await _identityService.CreateUserAsync(firstName: request.FirstName, lastName: request.LastName, username: request.Username, email: request.Email, password: request.Password);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}