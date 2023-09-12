using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace WB.EntrevistaABP.Viajes
{
    public class Viaje : FullAuditedAggregateRoot<Guid>
    {
        public DateTime FechaSalida { get; set; }

        public DateTime FechaLlegada { get; set; }

        public string Origen { get; set; }

        public string Destino { get; set; }

        public string MedioTransporte { get; set; }

        public ICollection<DetalleViaje> DetalleViajes { get; set; }

        public string Coordinador { get; set; }

        private Viaje()
        { }

        public Viaje(
            Guid id,
            DateTime fechaSalida,
            DateTime fechaLlegada,
            string origen,
            string destino,
            string medioTransporte,
            string? coordinador
        ) : base(id)
        {
            FechaSalida = fechaSalida;
            FechaLlegada = fechaLlegada;
            Origen = origen;
            Destino = destino;
            MedioTransporte = medioTransporte;
            Coordinador = coordinador!;

            DetalleViajes = new Collection<DetalleViaje>();
        }

        public void AgregarPasajero(Guid pasajeroId)
        {
            Check.NotNull(pasajeroId, nameof(pasajeroId));

            // entra FALSE porq no existe dentro de la base de datos
            // entonces al negarlo entra en TRUE, osea que agrego el nuevo pasajero,
            // si entra en TRUE entonces no agrego el nuevo pasajero,
            // directamente no hago nada

            if(!IsExistsPasajero(pasajeroId)) {
                DetalleViajes.Add(new DetalleViaje(viajeId: Id, pasajeroId));
            }

        }

        public void ElimnarPasajero(Guid pasajeroId)
        {
            Check.NotNull(pasajeroId, nameof(pasajeroId));

            if (!IsExistsPasajero(pasajeroId))
            {
                throw new UserFriendlyException("El pasajero no se encuentra en el viaje");
            }

            DetalleViajes.RemoveAll(DetalleViajes => DetalleViajes.PasajeroId == pasajeroId);
        }

        public void EliminarPasajerosExceptGivenIds(List<Guid> pasajeroIds)
        {
            Check.NotNullOrEmpty(pasajeroIds, nameof(pasajeroIds));

            DetalleViajes.RemoveAll(x => !pasajeroIds.Contains(x.PasajeroId));

        }

        public void EliminarPasajeros()
        {
            DetalleViajes.RemoveAll(x => x.ViajeId == Id);
        }


        private bool IsExistsPasajero(Guid pasajeroId)
        {
                return DetalleViajes.Any(x => x.PasajeroId == pasajeroId && x.ViajeId == Id);
        }

    }

}