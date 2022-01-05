namespace Shared.Interface
{
    public interface IDatabaseService
    {
        Task Migrate();
    }
}