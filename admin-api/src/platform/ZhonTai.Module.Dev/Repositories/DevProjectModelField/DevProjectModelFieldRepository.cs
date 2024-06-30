using ZhonTai.Module.Dev.Domain.DevProjectModelField;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevProjectModelField
{
    public class DevProjectModelFieldRepository : RepositoryBase<DevProjectModelFieldEntity>, IDevProjectModelFieldRepository
    {
        public DevProjectModelFieldRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
