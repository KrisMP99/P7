﻿using Microsoft.AspNetCore.Identity;

namespace P7WebApp.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void SetIdentity(string username, string email, string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            base.UserName = username;
            base.Email = email;
        }
        public void SetLoginCredentials(string username)
        {
            base.UserName = username;
        }
    }
}