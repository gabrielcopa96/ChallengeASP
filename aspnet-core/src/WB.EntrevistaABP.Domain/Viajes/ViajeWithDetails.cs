
using System;
using Volo.Abp.Auditing;

namespace WB.EntrevistaABP.Viajes
{
    public class ViajeWithDetails : IHasCreationTime
    {
        public Guid Id { get; set; }
        public DateTime FechaSalida { get; set; }

        public DateTime FechaLlegada { get; set; }

        public string Origen { get; set; }

        public string Destino { get; set; }

        public string MedioTransporte { get; set; }

        public string[] PasajerosNames { get; set; }

        public string Coordinador { get; set; }
        public DateTime CreationTime { get; set; }
    }
}