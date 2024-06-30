﻿using System.Threading.Tasks;
using Xunit;
using ZhonTai.Admin.Core.Dto;
using Emo.Dev.Api.Services.Module.Input;
using Emo.Dev.Api.Services.Module.Output;
using Emo.Dev.Api.Domain.Module.Dto;

namespace Emo.Dev.Tests.Controllers;

/// <summary>
/// 模块Api测试
/// </summary>
public class ModuleControllerTest : BaseControllerTest
{
    public ModuleControllerTest() : base()
    {
    }

    [Fact]
    public async Task Add()
    {
        var input = new ModuleAddInput
        {
            Name = "new-module"
        };

        var res = await PostResult($"/api/app/module/add", input);
        Assert.True(res.Success);
    }

    [Fact]
    public async Task Update()
    {
        var output = await GetResult<ResultOutput<ModuleGetOutput>>("/api/app/module/get?id=278518195769413");
        var res = await PutResult($"/api/app/module/update", output.Data);
        Assert.True(res.Success);
    }

    [Fact]
    public async Task Get()
    {
        var res = await GetResult<ResultOutput<ModuleGetOutput>>("/api/app/module/get?id=278518195769413");
        Assert.True(res.Success);
    }

    [Fact]
    public async Task GetPage()
    {
        await Login();
        var input = new PageInput<ModuleGetPageDto>
        {
            CurrentPage = 1,
            PageSize = 20,
            Filter = new ModuleGetPageDto
            {
                Name = "module"
            }
        };

        var res = await PostResult($"/api/app/module/get-page", input);
        Assert.True(res.Success);
    }

    [Fact]
    public async Task Delete()
    {
        var res = await DeleteResult($"/api/app/module/soft-delete?{ToParams(new { id = 278551714857029 })}");
        Assert.True(res.Success);
    }
}