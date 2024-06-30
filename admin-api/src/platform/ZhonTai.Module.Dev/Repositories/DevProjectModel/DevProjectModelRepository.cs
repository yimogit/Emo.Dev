using ZhonTai.Module.Dev.Domain.DevProjectModel;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevProjectModel
{
    public class DevProjectModelRepository : RepositoryBase<DevProjectModelEntity>, IDevProjectModelRepository
    {
        public DevProjectModelRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
