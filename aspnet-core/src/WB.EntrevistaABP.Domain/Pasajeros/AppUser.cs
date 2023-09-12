
using System;
using Volo.Abp.Identity;

namespace WB.EntrevistaABP.Pasajeros
{
    public class ApplicationUser : IdentityUser
    {
        public Guid PasajeroId { get; set; }
        public Pasajero Pasajero { get; set; }
    }
}