using Xunit;
using Emo.Dev.Api.Services.Module;
using Emo.Dev.Api.Domain.Module.Dto;
using ZhonTai.Admin.Core.Dto;

namespace Emo.Dev.Tests.Services;

/// <summary>
/// 模块服务测试
/// </summary>
public class ModuleServiceTest : BaseTest
{
    private readonly IModuleService _moduleService;

    public ModuleServiceTest()
    {
        _moduleService = GetService<IModuleService>();
    }

    [Fact]
    public async void GetPageAsync()
    {
        var input = new PageInput<ModuleGetPageDto>() { };
        var output = await _moduleService.GetPageAsync(input);
        Assert.True(output?.Total >= 0);
    }
}