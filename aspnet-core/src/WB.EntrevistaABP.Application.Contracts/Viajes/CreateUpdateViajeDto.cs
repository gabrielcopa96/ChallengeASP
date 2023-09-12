
using System;

namespace WB.EntrevistaABP.Viajes
{
    public class CreateUpdateViajeDto
    {
        public DateTime FechaSalida { get; set; }

        public DateTime FechaLlegada { get; set; }

        public string Origen { get; set; }

        public string Destino { get; set; }

        public string MedioTransporte { get; set; }

        public string[] PasajerosNames { get; set; }

        public string Coordinador { get; set; }
    }
}