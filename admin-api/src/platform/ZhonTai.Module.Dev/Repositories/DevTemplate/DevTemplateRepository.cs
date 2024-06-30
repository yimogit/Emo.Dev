using ZhonTai.Module.Dev.Domain.DevTemplate;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace ZhonTai.Module.Dev.Repositories.DevTemplate
{
    public class DevTemplateRepository : RepositoryBase<DevTemplateEntity>, IDevTemplateRepository
    {
        public DevTemplateRepository(UnitOfWorkManagerCloud muowm) : base(DbKeys.AppDb, muowm)
        {
        }
    }
}
