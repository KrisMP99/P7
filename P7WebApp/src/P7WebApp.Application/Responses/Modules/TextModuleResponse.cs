namespace P7WebApp.Application.Responses.Modules
{
    public class TextModuleResponse : ModuleResponse
    {
        public string Type { get; set; } = "text";
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
