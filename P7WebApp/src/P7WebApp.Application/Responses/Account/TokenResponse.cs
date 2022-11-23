namespace P7WebApp.Application.Responses.Account
{
    public class TokenResponse
    {
        public TokenResponse(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}