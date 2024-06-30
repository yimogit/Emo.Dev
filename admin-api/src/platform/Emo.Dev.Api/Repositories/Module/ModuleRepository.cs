using Emo.Dev.Api.Core.Repositories;
using Emo.Dev.Api.Domain.Module;
using ZhonTai.Admin.Core.Db.Transaction;

namespace Emo.Dev.Api.Repositories;

/// <summary>
/// ģ��ִ�
/// </summary>
public class ModuleRepository : AppRepositoryBase<ModuleEntity>, IModuleRepository
{
    public ModuleRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}