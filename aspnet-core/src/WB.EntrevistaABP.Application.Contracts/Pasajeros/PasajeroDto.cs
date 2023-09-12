

using System;
using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Pasajeros
{
    public class PasajeroDto : EntityDto<Guid>
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }
    }
}