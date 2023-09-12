using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace WB.EntrevistaABP.Pasajeros
{
    public class PasajeroAppService : EntrevistaABPAppService, IPasajeroAppService
    {

        private readonly IPasajeroRepository _pasajeroRepository;
        private readonly PasajeroManager _pasajeroManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PasajeroAppService(
            IPasajeroRepository pasajeroRepository,
            PasajeroManager pasajeroManager,
            UserManager<IdentityUser> userManager
        )
        {
            _pasajeroManager = pasajeroManager;
            _pasajeroRepository = pasajeroRepository;
            _userManager = userManager;
        }

        // [Authorize(EntrevistaABPPermissions.Pasajeros.Default)]
        public async Task<PagedResultDto<PasajeroDto>> GetListAsync(PasajeroGetListInput input)
        {
            var pasajeros = await _pasajeroRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount
            );

            var totalCount = await _pasajeroRepository.CountAsync();

            var pasajerosDtos = pasajeros.Select(pasajero => ConvertToDto(pasajero)).ToList();

            return new PagedResultDto<PasajeroDto>(
                totalCount,
                pasajerosDtos
            );
        }

        // [Authorize(EntrevistaABPPermissions.Pasajeros.Default)]
        public async Task<PasajeroDto> GetAsync(Guid id)
        {
            var pasajero = await _pasajeroRepository.GetAsync(id);

            return ConvertToDto(pasajero);
        }

        // [Authorize(EntrevistaABPPermissions.Pasajeros.Create)]
        public async Task CreateAsync(CreateUpdatePasajeroDto input)
        {
            await _pasajeroManager.CreateAsync(
            input.Nombre,
            input.Apellido,
            input.Dni,
            input.FechaNacimiento
            );

        }

        // [Authorize(EntrevistaABPPermissions.Pasajeros.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdatePasajeroDto input)
        {
            var pasajero = await _pasajeroRepository.GetAsync(id, includeDetails: true);

            pasajero.Nombre = input.Nombre;
            pasajero.Apellido = input.Apellido;
            pasajero.Dni = input.Dni;
            pasajero.FechaNacimiento = input.FechaNacimiento;

            await _pasajeroRepository.UpdateAsync(pasajero);
        }

        // [Authorize(EntrevistaABPPermissions.Pasajeros.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var pasajero = await _pasajeroRepository.GetAsync(id, includeDetails: true);

            var user = await _userManager.FindByIdAsync(pasajero.UserId.ToString()!);

            // await _userManager.DeleteAsync(user!);
            await _userManager.SetLockoutEnabledAsync(user!, true);

            await _pasajeroRepository.DeleteAsync(id);
        }

        private static PasajeroDto ConvertToDto(Pasajero pasajero)
        {

            return new PasajeroDto
            {
                Id = pasajero.Id,
                Nombre = pasajero.Nombre,
                Apellido = pasajero.Apellido,
                Dni = pasajero.Dni,
                FechaNacimiento = pasajero.FechaNacimiento,
            };

        }
    }
}