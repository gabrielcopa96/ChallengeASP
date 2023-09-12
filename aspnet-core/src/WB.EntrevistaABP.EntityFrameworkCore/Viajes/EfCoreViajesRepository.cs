
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using WB.EntrevistaABP.EntityFrameworkCore;
using WB.EntrevistaABP.Pasajeros;

namespace WB.EntrevistaABP.Viajes
{
    public class EfCoreViajeRepository : EfCoreRepository<EntrevistaABPDbContext, Viaje, Guid>, IViajeRepository
    {

        private readonly ICurrentUser _currentUser;

        private readonly IPasajeroRepository _pasajeroRepository;

        private readonly ILogger<EfCoreViajeRepository> _logger;

        public EfCoreViajeRepository(
            IDbContextProvider<EntrevistaABPDbContext> dbContextProvider,
            ICurrentUser currentUser,
            IPasajeroRepository pasajeroRepository,
            ILogger<EfCoreViajeRepository> logger
        ) : base(dbContextProvider)
        {
            _currentUser = currentUser;
            _pasajeroRepository = pasajeroRepository;
            _logger = logger;
        }


        public async Task<List<ViajeWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();

            if (!string.IsNullOrWhiteSpace(sorting))
            {

                query = sorting switch
                {
                    "fechaSalida asc" => query.OrderBy(p => p.FechaSalida),
                    "fechaSalida desc" => query.OrderByDescending(p => p.FechaSalida),
                    "fechaLlegada asc" => query.OrderBy(p => p.FechaLlegada),
                    "fechaLlegada desc" => query.OrderByDescending(p => p.FechaLlegada),
                    "origen asc" => query.OrderBy(p => p.Origen),
                    "origen desc" => query.OrderByDescending(p => p.Origen),
                    "destino asc" => query.OrderBy(p => p.Destino),
                    "destino desc" => query.OrderByDescending(p => p.Destino),
                    "medioTransporte asc" => query.OrderBy(p => p.MedioTransporte),
                    "medioTransporte desc" => query.OrderByDescending(p => p.MedioTransporte),
                    // Agrega más casos para otros campos de ordenamiento si es necesario
                    _ => query.OrderBy(p => p.Id),// Ordenamiento predeterminado (por ejemplo, por ID)
                };
            }

            return await query
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

        }

        public async Task<ViajeWithDetails> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();

            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken)) ?? throw new EntityNotFoundException(typeof(Viaje), id);
        }



        private async Task<IQueryable<ViajeWithDetails>> ApplyFilterAsync()
        {

            var dbContext = await GetDbContextAsync();

            if (_currentUser.IsInRole("admin"))
            {
                return (await GetDbSetAsync())
                .Include(x => x.DetalleViajes)
                .Select(x => new ViajeWithDetails
                {
                    Id = x.Id,
                    FechaSalida = x.FechaSalida,
                    FechaLlegada = x.FechaLlegada,
                    Origen = x.Origen,
                    Destino = x.Destino,
                    MedioTransporte = x.MedioTransporte,
                    PasajerosNames = (
                        from pasajeros in x.DetalleViajes
                        join pasajero in dbContext.Pasajeros on pasajeros.PasajeroId equals pasajero.Id
                        select $"{pasajero.Nombre} {pasajero.Apellido}"
                    ).ToArray(),
                    Coordinador = x.Coordinador
                });
            }
            else
            {

                var convertGuid = _currentUser.Id ?? Guid.Empty;

                var pasajero = await _pasajeroRepository.FindByUserIdAsync(convertGuid);

                return (await GetDbSetAsync())
                 .Include(x => x.DetalleViajes)
                 .Where(x => x.DetalleViajes.Any(y => y.PasajeroId == pasajero.Id))
                 .Select(x => new ViajeWithDetails
                 {
                     Id = x.Id,
                     FechaSalida = x.FechaSalida,
                     FechaLlegada = x.FechaLlegada,
                     Origen = x.Origen,
                     Destino = x.Destino,
                     MedioTransporte = x.MedioTransporte,
                     PasajerosNames = (
                         from pasajeros in x.DetalleViajes
                         join pasajero in dbContext.Pasajeros on pasajeros.PasajeroId equals pasajero.Id
                         select $"{pasajero.Nombre} {pasajero.Apellido}"
                     ).ToArray(),
                     Coordinador = x.Coordinador
                 });
            }


        }

        public override Task<IQueryable<Viaje>> WithDetailsAsync()
        {
            return base.WithDetailsAsync(x => x.DetalleViajes);
        }

    }
}