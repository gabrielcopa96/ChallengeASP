using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WB.EntrevistaABP.EntityFrameworkCore;

namespace WB.EntrevistaABP.Pasajeros
{
    public class EfCorePasajeroRepository : EfCoreRepository<EntrevistaABPDbContext, Pasajero, Guid>, IPasajeroRepository
    {
        public EfCorePasajeroRepository(
            IDbContextProvider<EntrevistaABPDbContext> dbContextProvider
        ) : base(dbContextProvider)
        { }

        public async Task<List<Pasajero>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        )
        {
            var query = await GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(sorting))
            {
                query = sorting switch
                {
                    "nombre asc" => query.OrderBy(p => p.Nombre),
                    "nombre desc" => query.OrderByDescending(p => p.Nombre),
                    "apellido asc" => query.OrderBy(p => p.Apellido),
                    "apellido desc" => query.OrderByDescending(p => p.Apellido),
                    "dni asc" => query.OrderBy(p => p.Dni),
                    "dni desc" => query.OrderByDescending(p => p.Dni),
                    "fechaNacimiento asc" => query.OrderBy(p => p.FechaNacimiento),
                    "fechaNacimiento desc" => query.OrderByDescending(p => p.FechaNacimiento),
                    // Agrega más casos para otros campos de ordenamiento si es necesario
                    _ => query.OrderBy(p => p.Id),// Ordenamiento predeterminado (por ejemplo, por ID)
                };
            }

            query = query.PageBy(skipCount, maxResultCount);

            var result = await query.ToListAsync(cancellationToken);

            return result;

        }

        public async Task<Pasajero> FindByDniAsync(
            string dni,
            CancellationToken cancellationToken = default
        )
        {

            var query = await GetQueryableAsync();

            var result = await query
                .Where(p => p.Dni == dni)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(cancellationToken);

            return result!;

        }

        public async Task<Pasajero> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {

            var query = await GetQueryableAsync();

            var result = await query
                .Where(p => p.UserId == userId)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(cancellationToken);

            return result!;

        }

        public async Task<Pasajero> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var query = await GetQueryableAsync();

            var pasajero = await query
                .Where(x => x.Id == id)
                .Select(x => x)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException(typeof(Pasajero), id);

            return pasajero;

        }

    }

}