using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace WB.EntrevistaABP
{
    internal class DataSeedContributor : IDataSeedContributor, ITransientDependency
    {

        // private readonly IGuidGenerator _guidGenerator;
        // private readonly IRepository<Pasajero, Guid> _pasajeroRepository;

        public DataSeedContributor(
            // IGuidGenerator guidGenerator,
            // IRepository<Pasajero, Guid> pasajeroRepository
        )
        {
            // _guidGenerator = guidGenerator;
            // _pasajeroRepository = pasajeroRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Datos a rellenar en backend
            // Pista: Aca se pueden crear usuarios, roles, asignar permisos, etc.
            // await SeedPasajerosAsync();
        }

        // private async Task SeedPasajerosAsync()
        // {
        //     if (await _pasajeroRepository.GetCountAsync() <= 0)
        //     {
        //         await _pasajeroRepository.InsertAsync(
        //             new Pasajero(
        //                 _guidGenerator.Create(),
        //                 "Juan",
        //                 "Perez",
        //                 "12345678",
        //                 new DateTime(1998, 06, 12)
        //             )
        //         );
            
        //     }

        // }

    }
}