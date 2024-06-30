using ZhonTai.Module.Dev.Domain.DevGroup;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevGroup
{
    public class DevGroupRepository : RepositoryBase<DevGroupEntity>, IDevGroupRepository
    {
        public DevGroupRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
