namespace P7WebApp.Application.Responses
{
    public class InviteCodeResponse
    {
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime UseableFrom { get; set; }
        public DateTime UseableTo { get; set; }
    }
}