using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WB.EntrevistaABP.Pasajeros
{
    public interface IPasajeroRepository : IRepository<Pasajero, Guid>
    {
        Task<List<Pasajero>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        );

        Task<Pasajero> GetAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Pasajero> FindByDniAsync(string dni, CancellationToken cancellationToken = default);

        Task<Pasajero> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}