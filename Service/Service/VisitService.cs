using Data;
using Data.Domain;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Interface;

namespace Shared.Service
{
    public class VisitService : IVisitService
    {
        public readonly VisitContext _orderContext;
        public VisitService(VisitContext orderContext)
        {
            _orderContext = orderContext;
        }


        public async Task<Visit> AddAsync(Visit visit)
        {
            await _orderContext.Visit.AddAsync(visit);
            await _orderContext.SaveChangesAsync();
            return visit;
        }

        public async Task<Visit> UpdateAsync(Visit visit)
        {
            var entry = _orderContext.Visit.Update(visit);
            await _orderContext.SaveChangesAsync();
            await entry.ReloadAsync();
            return visit;
        }
    }
}