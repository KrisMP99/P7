namespace P7WebApp.Application.Responses.Modules
{
    public abstract class ModuleResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Position { get; set; }
    }
}
