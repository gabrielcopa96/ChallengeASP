using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WB.EntrevistaABP.Viajes
{
    public interface IViajeAppService : IApplicationService
    {
        Task<PagedResultDto<ViajeDto>> GetListAsync(ViajeGetListInput input);

        Task<ViajeDto> GetAsync(Guid id);

        Task CreateAsync(CreateUpdateViajeDto input);

        Task UpdateAsync(Guid id, CreateUpdateViajeDto input);

        Task DeleteAsync(Guid id);
    }
}