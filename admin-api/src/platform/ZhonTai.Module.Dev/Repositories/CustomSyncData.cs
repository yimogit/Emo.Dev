using System.Threading.Tasks;
using ZhonTai.Admin.Core.Configs;
using ZhonTai.Module.Dev.Domain.DevGroup;
using ZhonTai.Module.Dev.Domain.DevTemplate;
using ZhonTai.Module.Dev.Domain.DevProject;
using ZhonTai.Module.Dev.Domain.DevProjectModel;
using ZhonTai.Module.Dev.Domain.DevProjectModelField;
using ZhonTai.Admin.Core.Db.Data;
using FreeSql;
using System.Linq;
using System;
using ZhonTai.Admin.Core.Entities;
using ZhonTai.Module.Dev.Domain.DevProjectGen;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Domain.Tenant;
using ZhonTai.Module.Dev.Domain.CodeGen;

namespace ZhonTai.Admin.Repositories;

public class CustomSyncData : SyncData, ISyncData
{
    /// <summary>
    /// 同步模板数据
    /// </summary>
    /// <param name="db"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="dbConfig"></param>
    /// <param name="appConfig"></param>
    /// <param name="path">同步目录</param>
    /// <returns></returns>
    private async Task InitModuleAsync<TEntity>(IFreeSql db, IRepositoryUnitOfWork unitOfWork, DbConfig dbConfig, AppConfig appConfig, string path) where TEntity : EntityBase, new()
    {
        var tableName = GetTableName<TEntity>();
        try
        {
            if (!IsSyncData(tableName, dbConfig))
            {
                return;
            }
            var isTenant = appConfig.Tenant && typeof(TEntity).IsAssignableFrom(typeof(EntityTenant));
            var rep = db.GetRepository<TEntity>();
            rep.UnitOfWork = unitOfWork;

            //数据列表
            var dataList = GetData<TEntity>(isTenant, path);

            if (!(dataList?.Length > 0))
            {
                Console.WriteLine($"table: {tableName} import data []");
                return;
            }

            //查询
            var ids = dataList.Select(e => e.Id).ToList();
            var recordList = await rep.Where(a => ids.Contains(a.Id)).ToListAsync();

            //新增
            var recordIds = recordList.Select(a => a.Id).ToList();
            var insertDataList = dataList.Where(a => !recordIds.Contains(a.Id));
            if (insertDataList.Any())
            {
                await rep.InsertAsync(insertDataList);
            }

            //修改
            if (dbConfig.SysUpdateData && recordList?.Count > 0)
            {
                var updateDataList = dataList.Where(a => recordIds.Contains(a.Id));
                await rep.UpdateAsync(updateDataList);
            }

            Console.WriteLine($"table: {tableName} sync data succeed");
        }
        catch (Exception ex)
        {
            var msg = $"table: {tableName} sync data failed.\n{ex.Message}";
            Console.WriteLine(msg);
            throw new Exception(msg);
        }
    }
    /// <summary>
    /// 同步InitData/Dev/*.json数据
    /// </summary>
    /// <param name="db"></param>
    /// <param name="dbConfig"></param>
    /// <param name="appConfig"></param>
    /// <returns></returns>
    public virtual async Task SyncDataAsync(IFreeSql db, DbConfig dbConfig = null, AppConfig appConfig = null)
    {
        using var unitOfWork = db.CreateUnitOfWork();

        try
        {
            //读取数据目录
            var readPath = DevConsts.InitFilePath;
            await InitModuleAsync<DevGroupEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<DevTemplateEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<DevProjectEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<DevProjectModelEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<DevProjectModelFieldEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<DevProjectGenEntity>(db, unitOfWork, dbConfig, appConfig, readPath);
            await InitModuleAsync<CodeGenEntity>(db, unitOfWork, dbConfig, appConfig, readPath);

            unitOfWork.Commit();
        }
        catch (Exception)
        {
            unitOfWork.Rollback();
            //throw;
        }
    }
}
