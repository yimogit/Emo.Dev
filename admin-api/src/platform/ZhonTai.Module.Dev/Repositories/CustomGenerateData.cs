using System.Linq;
using System.Threading.Tasks;
using ZhonTai.Admin.Core.Configs;
using ZhonTai.Admin.Domain.Tenant;
using ZhonTai.Admin.Core.Db.Data;
using ZhonTai.Module.Dev.Domain.DevProject;
using ZhonTai.Admin.Core.Entities;
using ZhonTai.Module.Dev.Domain.DevGroup;
using ZhonTai.Module.Dev.Domain.DevProjectGen;
using ZhonTai.Module.Dev.Domain.DevProjectModel;
using ZhonTai.Module.Dev.Domain.DevProjectModelField;
using ZhonTai.Module.Dev.Domain.DevTemplate;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Module.Dev.Domain.CodeGen;

namespace ZhonTai.Admin.Repositories;

public class CustomGenerateData : GenerateData, IGenerateData
{
    public virtual async Task InitModuleAsync<TEntity>(IFreeSql db, AppConfig appConfig, string path) where TEntity : EntityBase, new()
    {
        var modules = await db.Queryable<TEntity>().ToListAsync();
        //是否多租户
        var isTenant = appConfig.Tenant && typeof(TEntity).IsAssignableFrom(typeof(EntityTenant));
        if (isTenant)
        {
            var tenantIds = await db.Queryable<TenantEntity>().ToListAsync(a => a.Id);
            SaveDataToJsonFile<TEntity>(modules.Where(a => (a as EntityTenant).TenantId > 0 && tenantIds.Contains((a as EntityTenant).TenantId.Value)), isTenant, path);
        }

        SaveDataToJsonFile<TEntity>(modules, isTenant, path);
    }
    /// <summary>
    /// 生成数据到InitData/Dev 手动新建Dev目录
    /// </summary>
    /// <param name="db"></param>
    /// <param name="appConfig"></param>
    /// <returns></returns>
    public virtual async Task GenerateDataAsync(IFreeSql db, AppConfig appConfig)
    {
        //生成数据目录
        var outPath = DevConsts.InitFilePath;
        await InitModuleAsync<DevGroupEntity>(db, appConfig, outPath);
        await InitModuleAsync<DevTemplateEntity>(db, appConfig, outPath);
        await InitModuleAsync<DevProjectEntity>(db, appConfig, outPath);
        await InitModuleAsync<DevProjectModelEntity>(db, appConfig, outPath);
        await InitModuleAsync<DevProjectModelFieldEntity>(db, appConfig, outPath);
        await InitModuleAsync<DevProjectGenEntity>(db, appConfig, outPath);
        await InitModuleAsync<CodeGenEntity>(db, appConfig, outPath);
    }
}
