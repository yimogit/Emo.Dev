
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;


using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using ZhonTai.Admin.Domain.Dict;

using ZhonTai.Module.Dev.Domain.DevGroup;
using ZhonTai.Module.Dev.Services.DevGroup.Dto;
using ZhonTai.Module.Dev.Core.Consts;


namespace ZhonTai.Module.Dev.Services.DevGroup
{
    /// <summary>
    /// 模板组服务
    /// </summary>
    [DynamicApi(Area = DevConsts.AreaName)]
    public partial class DevGroupService : BaseService, IDevGroupService, IDynamicApi
    {
        private IDevGroupRepository _devGroupRepository => LazyGetRequiredService<IDevGroupRepository>();

        public DevGroupService()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DevGroupGetOutput> GetAsync(long id)
        {
            var output = await _devGroupRepository.GetAsync<DevGroupGetOutput>(id);
            return output;
        }
        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevGroupGetListOutput>> GetListAsync(DevGroupGetListInput input)
        {
            var list = await _devGroupRepository.Select
                .WhereIf(!string.IsNullOrEmpty(input.Name), a=>a.Name == input.Name)
                .OrderByDescending(a => a.Id)
                .ToListAsync<DevGroupGetListOutput>();
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DevGroupGetPageOutput>> GetPageAsync(PageInput<DevGroupGetPageInput> input)
        {
            var filter = input.Filter;
            var list = await _devGroupRepository.Select
                .WhereDynamicFilter(input.DynamicFilter)
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Name), a=> a.Name != null && a.Name.Contains(filter.Name))
                .Count(out var total)
                .OrderByDescending(c => c.Id)
                .Page(input.CurrentPage, input.PageSize)
                .ToListAsync<DevGroupGetPageOutput>();
        

            //关联查询代码

            var data = new PageOutput<DevGroupGetPageOutput> { List = list, Total = total };
        
            return data;
        }
        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> AddAsync(DevGroupAddInput input)
        {
            var entity = Mapper.Map<DevGroupEntity>(input);
            var id = (await _devGroupRepository.InsertAsync(entity)).Id;

            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(DevGroupUpdateInput input)
        {
            var entity = await _devGroupRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                throw ResultOutput.Exception("模板组不存在！");
            }

            Mapper.Map(input, entity);
            await _devGroupRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _devGroupRepository.DeleteAsync(id) > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            return await _devGroupRepository.Where(w=>ids.Contains(w.Id)).ToDelete().ExecuteAffrowsAsync() > 0;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _devGroupRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            return await _devGroupRepository.SoftDeleteAsync(ids);
        }
    }
}