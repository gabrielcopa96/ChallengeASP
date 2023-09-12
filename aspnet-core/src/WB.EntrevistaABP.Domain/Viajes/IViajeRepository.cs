
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WB.EntrevistaABP.Viajes
{
    public interface IViajeRepository : IRepository<Viaje, Guid>
    {
        Task<List<ViajeWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        );

        Task<ViajeWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default);

    }
}