

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace WB.EntrevistaABP.Pasajeros
{
    public class PasajeroManager : DomainService
    {
        private readonly IPasajeroRepository _pasajeroRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<PasajeroManager> _logger;

        public PasajeroManager(
            IPasajeroRepository iPasajeroRepository,
            IGuidGenerator guidGenerator,
            UserManager<IdentityUser> userManager,
            ILogger<PasajeroManager> logger
        )
        {
            _pasajeroRepository = iPasajeroRepository;
            _guidGenerator = guidGenerator;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task CreateAsync(
            string nombre,
            string apellido,
            string dni,
            DateTime fechaNacimiento
        )
        {

            var findPasajero = await _pasajeroRepository.FindByDniAsync(dni);

            _logger.LogInformation(findPasajero != null ? "Pasajero ya existe" : "Pasajero no existe");

            if (findPasajero != null)
            {
                var findUser = await _userManager.FindByIdAsync(findPasajero.UserId.ToString()!);

                _logger.LogInformation(findUser != null ? "User ya existe" : "User no existe");

                if (findUser != null)
                {

                    await _userManager.SetLockoutEnabledAsync(findUser, false);
                    // await _userManager.UpdateAsync(findUser);

                    findPasajero.SetNombre(nombre);
                    findPasajero.SetApellido(apellido);
                    findPasajero.FechaNacimiento = fechaNacimiento;
                    findPasajero.IsDeleted = false;

                    await _pasajeroRepository.UpdateAsync(findPasajero);
                }

            }
            else
            {
                var user = new IdentityUser(
                _guidGenerator.Create(),
                dni,
                GenerateStringRandom() + "@gmail.com"
            );

                await _userManager.CreateAsync(user, "1q2w3E*");

                var pasajero = new Pasajero(
                   GuidGenerator.Create(),
                   nombre,
                   apellido,
                   dni,
                   fechaNacimiento,
                   user.Id
                );

                await _pasajeroRepository.InsertAsync(pasajero);
            }

        }

        public async Task UpdateAsync(
            Pasajero pasajero,
            string nombre,
            string apellido,
            string dni,
            DateTime fechaNacimiento
        )
        {
            pasajero.SetNombre(nombre);
            pasajero.SetApellido(apellido);
            pasajero.SetDni(dni);
            pasajero.FechaNacimiento = fechaNacimiento;

            await _pasajeroRepository.UpdateAsync(pasajero);
        }

        private string GenerateStringRandom(int length = 8)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789._-";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);


        }

    }
}