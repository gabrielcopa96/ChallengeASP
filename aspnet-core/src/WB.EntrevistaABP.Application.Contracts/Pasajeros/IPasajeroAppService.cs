

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WB.EntrevistaABP.Pasajeros
{
    public interface IPasajeroAppService : IApplicationService
    {
        Task<PagedResultDto<PasajeroDto>> GetListAsync(PasajeroGetListInput input);

        Task<PasajeroDto> GetAsync(Guid id);

        Task CreateAsync(CreateUpdatePasajeroDto input);

        Task UpdateAsync(Guid id, CreateUpdatePasajeroDto input);

        Task DeleteAsync(Guid id);

    }
}