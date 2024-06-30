using ZhonTai.Module.Dev.Domain.DevProject;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevProject
{
    public class DevProjectRepository : RepositoryBase<DevProjectEntity>, IDevProjectRepository
    {
        public DevProjectRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
