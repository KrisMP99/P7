using P7WebApp.Domain.Identity;

namespace P7WebApp.Application.Responses
{
    public class TokenResponse
    {
        public TokenResponse(ApplicationUser user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}