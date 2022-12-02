namespace P7WebApp.Application.Responses.Modules
{
    public class CodeEditorModuleResponse : ModuleResponse
    {
        public string Type { get; set; } = "code";
        public string Code { get; set; }
    }
}
