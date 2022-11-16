using Microsoft.AspNetCore.Identity;
using P7WebApp.Domain.Aggregates.CourseAggregate;

namespace P7WebApp.Domain.Identity
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