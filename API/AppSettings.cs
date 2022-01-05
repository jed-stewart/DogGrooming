namespace API
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = default!;
    }
    public class ConnectionStrings
    {
        public string Orders { get; set; } = "";
    }
}