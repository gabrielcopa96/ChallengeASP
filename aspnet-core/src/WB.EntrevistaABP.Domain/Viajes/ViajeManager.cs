

using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using WB.EntrevistaABP.Pasajeros;

namespace WB.EntrevistaABP.Viajes
{
    public class ViajeManager : DomainService
    {
        private readonly IViajeRepository _viajeRepository;

        private readonly IRepository<Pasajero, Guid> _pasajeroRepository;

        public ViajeManager(
            IViajeRepository viajeRepository,
            IRepository<Pasajero, Guid> pasajeroRepository
        )
        {
            _viajeRepository = viajeRepository;
            _pasajeroRepository = pasajeroRepository;
        }

        public async Task CreateAsync(
            DateTime fechaSalida,
            DateTime fechaLlegada,
            string origen,
            string destino,
            string medioTransporte,
            [CanBeNull] string[] pasajerosNames,
            string? coodinador
        )
        {
            var viaje = new Viaje(
                GuidGenerator.Create(),
                fechaSalida,
                fechaLlegada,
                origen,
                destino,
                medioTransporte,
                coodinador
            );

            await SetPasajerosAsync(viaje, pasajerosNames);

            await _viajeRepository.InsertAsync(viaje);
        }

        public async Task UpdateAsync(
            Viaje viaje,
            DateTime fechaSalida,
            DateTime fechaLlegada,
            string origen,
            string destino,
            string medioTransporte,
            [CanBeNull] string[] pasajerosNames,
            string? coordinador
        )
        {
            viaje.FechaSalida = fechaSalida;
            viaje.FechaLlegada = fechaLlegada;
            viaje.Origen = origen;
            viaje.Destino = destino;
            viaje.MedioTransporte = medioTransporte;
            viaje.Coordinador = coordinador!;

            await SetPasajerosAsync(viaje, pasajerosNames);

            await _viajeRepository.UpdateAsync(viaje);
        }



        private async Task SetPasajerosAsync(Viaje viaje, [CanBeNull] string[] pasajerosNames)
        {
            if (pasajerosNames == null || !pasajerosNames.Any())
            {
                viaje.EliminarPasajeros();
                return;
            }

            var query = (await _pasajeroRepository.GetQueryableAsync())
                .Where(x => pasajerosNames.Contains(x.Dni))
                .Select(x => x.Id)
                .Distinct();

            var pasajeroIds = await AsyncExecuter.ToListAsync(query);

            if (!pasajeroIds.Any())
            {
                return;
            }

            viaje.EliminarPasajerosExceptGivenIds(pasajeroIds);

            foreach (var pasajeroId in pasajeroIds)
            {
                viaje.AgregarPasajero(pasajeroId);
            }
        }

    }
}