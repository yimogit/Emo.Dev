using ZhonTai.Admin.Core.Dto;
using System.Threading.Tasks;
using Emo.Dev.Api.Domain.Module.Dto;
using Emo.Dev.Api.Services.Module.Input;
using Emo.Dev.Api.Services.Module.Output;

namespace Emo.Dev.Api.Services.Module;

/// <summary>
/// 模块接口
/// </summary>
public interface IModuleService
{
    Task<ModuleGetOutput> GetAsync(long id);

    Task<PageOutput<ModuleListOutput>> GetPageAsync(PageInput<ModuleGetPageDto> input);

    Task<long> AddAsync(ModuleAddInput input);

    Task UpdateAsync(ModuleUpdateInput input);

    Task DeleteAsync(long id);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);
}