namespace P7WebApp.Application.Responses.Profile
{
    public class TokenResponse
    {
        public TokenResponse(string token, int userId, string firstname, string lastname, string email, string username)
        {
            Token = token;
            UserId = userId;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Username = username;
        }

        public string Token { get; private set; }
        public int UserId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
    }
}