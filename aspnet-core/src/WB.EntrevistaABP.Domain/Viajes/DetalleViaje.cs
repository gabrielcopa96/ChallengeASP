

using System;
using Volo.Abp.Domain.Entities;

namespace WB.EntrevistaABP.Viajes
{
    public class DetalleViaje : Entity
    {
        public Guid ViajeId { get; set; }

        public Guid PasajeroId { get; set; }

        private DetalleViaje()
        {}

        public DetalleViaje(
            Guid viajeId,
            Guid pasajeroId
        ) : base()
        {
            ViajeId = viajeId;
            PasajeroId = pasajeroId;
        }

        public override object[] GetKeys()
        {
            return new object[] { ViajeId, PasajeroId };
        }
    
    }
        
    
}