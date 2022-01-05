using Data.Domain;

namespace Shared.Interface
{
    public interface IVisitService
    {
        Task<Visit> AddAsync(Visit visit);
        Task<Visit> UpdateAsync(Visit visit);
    }
}