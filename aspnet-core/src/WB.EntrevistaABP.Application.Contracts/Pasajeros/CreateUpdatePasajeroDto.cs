using System;

namespace WB.EntrevistaABP.Pasajeros
{
    public class CreateUpdatePasajeroDto {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}