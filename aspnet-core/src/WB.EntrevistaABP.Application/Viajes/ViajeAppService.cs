using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using WB.EntrevistaABP.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace WB.EntrevistaABP.Viajes
{
    public class ViajeAppService : EntrevistaABPAppService, IViajeAppService
    {
        private readonly IViajeRepository _viajeRepository;

        private readonly ViajeManager _viajeManager;


        public ViajeAppService(
            IViajeRepository viajeRepository,
            ViajeManager viajeManager
        )
        {
            _viajeRepository = viajeRepository;
            _viajeManager = viajeManager;
        }

        // [Authorize("AllViajesForPasajero")]
        // [Authorize(EntrevistaABPPermissions.Viajes.Default)]
        public async Task<PagedResultDto<ViajeDto>> GetListAsync(
                            ViajeGetListInput input
                        )
        {

            if(input.Sorting.IsNullOrEmpty())
            {
                input.Sorting = nameof(Viaje.Origen);
            }


            var viajes = await _viajeRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount
            );

            var totalCount = await _viajeRepository.CountAsync();

            return new PagedResultDto<ViajeDto>(
                totalCount,
                ObjectMapper.Map<List<ViajeWithDetails>, List<ViajeDto>>(viajes)
            );
        }

        // [Authorize(EntrevistaABPPermissions.Viajes.Default)]
        public async Task<ViajeDto> GetAsync(Guid id)
        {
            var viaje = await _viajeRepository.GetAsync(id);

            return ObjectMapper.Map<ViajeWithDetails, ViajeDto>(viaje);
        }

        // [Authorize(EntrevistaABPPermissions.Viajes.Create)]
        public async Task CreateAsync(
                    CreateUpdateViajeDto input
                )
        {
            await _viajeManager.CreateAsync(
                input.FechaSalida,
                input.FechaLlegada,
                input.Origen,
                input.Destino,
                input.MedioTransporte,
                input.PasajerosNames,
                input.Coordinador
            );
        }

        // [Authorize(EntrevistaABPPermissions.Viajes.Edit)]
        public async Task UpdateAsync(
                    Guid id,
                    CreateUpdateViajeDto input
                )
        {
            var viaje = await _viajeRepository.GetAsync(id, includeDetails: true);

            await _viajeManager.UpdateAsync(
                viaje,
                input.FechaSalida,
                input.FechaLlegada,
                input.Origen,
                input.Destino,
                input.MedioTransporte,
                input.PasajerosNames,
                input.Coordinador
            );
        }

        // [Authorize(EntrevistaABPPermissions.Viajes.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _viajeRepository.DeleteAsync(id);
        }
    }
}