using Emo.Dev.Api.Core.Consts;
using ZhonTai.Admin.Core.Db.Transaction;
using ZhonTai.Admin.Core.Repositories;

namespace Emo.Dev.Api.Core.Repositories;

public class AppRepositoryBase<TEntity> : RepositoryBase<TEntity> where TEntity : class
{
    public AppRepositoryBase(UnitOfWorkManagerCloud uowm) : base(DbKeys.AppDb, uowm)
    {

    }
}