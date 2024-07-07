
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

using ZhonTai.Module.Dev.Domain.DevTemplate;
using ZhonTai.Module.Dev.Services.DevTemplate.Dto;
using ZhonTai.Module.Dev.Core.Consts;


namespace ZhonTai.Module.Dev.Services.DevTemplate
{
    /// <summary>
    /// 模板服务
    /// </summary>
    [DynamicApi(Area = DevConsts.AreaName)]
    public partial class DevTemplateService : BaseService, IDevTemplateService, IDynamicApi
    {
        private IDevTemplateRepository _devTemplateRepository => LazyGetRequiredService<IDevTemplateRepository>();

        public DevTemplateService()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DevTemplateGetOutput> GetAsync(long id)
        {
            var output = await _devTemplateRepository.GetAsync<DevTemplateGetOutput>(id);
            return output;
        }
        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevTemplateGetListOutput>> GetListAsync(DevTemplateGetListInput input)
        {
            var list = await _devTemplateRepository.Select
                .WhereIf(!string.IsNullOrEmpty(input.Name), a=>a.Name == input.Name)
                .WhereIf(input.GroupId != null, a=>a.GroupId == input.GroupId)
                .OrderByDescending(a => a.Id)
                .ToListAsync<DevTemplateGetListOutput>();
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DevTemplateGetPageOutput>> GetPageAsync(PageInput<DevTemplateGetPageInput> input)
        {
            var filter = input.Filter;
            var list = await _devTemplateRepository.Select
                .WhereDynamicFilter(input.DynamicFilter)
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Name), a=> a.Name != null && a.Name.Contains(filter.Name))
                .WhereIf(filter !=null && filter.GroupId != null, a=>a.GroupId == filter.GroupId)
                .Count(out var total)
                .OrderByDescending(c => c.Id)
                .Page(input.CurrentPage, input.PageSize)
                .ToListAsync<DevTemplateGetPageOutput>();
        

            //关联查询代码
            //数据转换-单个关联
            var groupIdRows = list.Where(s => s.GroupId > 0).ToList();
            if (groupIdRows.Any())
            {
                var groupIdRepo = LazyGetRequiredService<Domain.DevGroup.IDevGroupRepository>();
                var groupIdRowsIds = groupIdRows.Select(s => s.GroupId).Distinct().ToList();
                var groupIdRowsIdsData = await groupIdRepo.Where(s => groupIdRowsIds.Contains(s.Id)).ToListAsync(s => new { s.Id, s.Name });
                groupIdRows.ForEach(s =>
                {
                    s.GroupId_Text = groupIdRowsIdsData.FirstOrDefault(s2 => s2.Id == s.GroupId)?.Name;
                });
            }

            var data = new PageOutput<DevTemplateGetPageOutput> { List = list, Total = total };
        
            return data;
        }
        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> AddAsync(DevTemplateAddInput input)
        {
            var entity = Mapper.Map<DevTemplateEntity>(input);
            var id = (await _devTemplateRepository.InsertAsync(entity)).Id;

            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(DevTemplateUpdateInput input)
        {
            var entity = await _devTemplateRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                throw ResultOutput.Exception("模板不存在！");
            }

            Mapper.Map(input, entity);
            await _devTemplateRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _devTemplateRepository.DeleteAsync(id) > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            return await _devTemplateRepository.Where(w=>ids.Contains(w.Id)).ToDelete().ExecuteAffrowsAsync() > 0;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _devTemplateRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            return await _devTemplateRepository.SoftDeleteAsync(ids);
        }
    }
}