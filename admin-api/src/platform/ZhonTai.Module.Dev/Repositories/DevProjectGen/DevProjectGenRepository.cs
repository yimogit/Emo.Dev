using ZhonTai.Module.Dev.Domain.DevProjectGen;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevProjectGen
{
    public class DevProjectGenRepository : RepositoryBase<DevProjectGenEntity>, IDevProjectGenRepository
    {
        public DevProjectGenRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
