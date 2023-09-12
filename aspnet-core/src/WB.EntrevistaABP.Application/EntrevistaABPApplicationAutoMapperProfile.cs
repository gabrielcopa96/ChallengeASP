using AutoMapper;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP;

public class EntrevistaABPApplicationAutoMapperProfile : Profile
{
    public EntrevistaABPApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Pasajeros
        CreateMap<Pasajero, PasajeroDto>();
        CreateMap<CreateUpdatePasajeroDto, Pasajero>();

        // Viajes Detail
        CreateMap<ViajeWithDetails, ViajeDto>();
    }
}
