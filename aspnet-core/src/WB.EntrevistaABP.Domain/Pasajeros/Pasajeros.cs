using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace WB.EntrevistaABP.Pasajeros
{
    public class Pasajero : FullAuditedAggregateRoot<Guid>
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public Guid? UserId { get; set;}

        private Pasajero()
        {}

        public Pasajero(
            Guid id, 
            [NotNull] string nombre, 
            [NotNull] string apellido, 
            [NotNull] string dni, 
            DateTime fechaNacimiento,
            Guid? userId
        ) : base(id)
        {
            SetNombre(nombre);
            SetApellido(apellido);
            SetDni(dni);
            FechaNacimiento = fechaNacimiento;
            UserId = userId;
        }

        public void SetNombre(string nombre)
        {
            Nombre = Check.NotNullOrWhiteSpace(
                nombre, 
                nameof(nombre),
                maxLength: PasajeroConsts.MaxNombreLength
            );
        }

        public void SetApellido(string apellido)
        {
            Apellido = Check.NotNullOrWhiteSpace(
                apellido, 
                nameof(apellido),
                maxLength: PasajeroConsts.MaxApellidoLength
            );
        }

        public void SetDni(string dni)
        {
            Dni = Check.NotNullOrWhiteSpace(
                dni, 
                nameof(dni),
                maxLength: PasajeroConsts.MaxDniLength
            ); 
        }

    }
}