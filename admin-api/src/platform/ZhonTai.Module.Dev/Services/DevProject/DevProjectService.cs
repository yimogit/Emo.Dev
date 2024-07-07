
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

using ZhonTai.Module.Dev.Domain.DevProject;
using ZhonTai.Module.Dev.Services.DevProject.Dto;
using ZhonTai.Module.Dev.Core.Consts;


namespace ZhonTai.Module.Dev.Services.DevProject
{
    /// <summary>
    /// 项目服务
    /// </summary>
    [DynamicApi(Area = DevConsts.AreaName)]
    public partial class DevProjectService : BaseService, IDevProjectService, IDynamicApi
    {
        private IDevProjectRepository _devProjectRepository => LazyGetRequiredService<IDevProjectRepository>();

        public DevProjectService()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DevProjectGetOutput> GetAsync(long id)
        {
            var output = await _devProjectRepository.GetAsync<DevProjectGetOutput>(id);
            return output;
        }
        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevProjectGetListOutput>> GetListAsync(DevProjectGetListInput input)
        {
            var list = await _devProjectRepository.Select
                .WhereIf(!string.IsNullOrEmpty(input.Name), a=>a.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code), a=>a.Code == input.Code)
                .OrderByDescending(a => a.Id)
                .ToListAsync<DevProjectGetListOutput>();
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DevProjectGetPageOutput>> GetPageAsync(PageInput<DevProjectGetPageInput> input)
        {
            var filter = input.Filter;
            var list = await _devProjectRepository.Select
                .WhereDynamicFilter(input.DynamicFilter)
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Name), a=> a.Name != null && a.Name.Contains(filter.Name))
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Code), a=> a.Code != null && a.Code.Contains(filter.Code))
                .Count(out var total)
                .OrderByDescending(c => c.Id)
                .Page(input.CurrentPage, input.PageSize)
                .ToListAsync<DevProjectGetPageOutput>();
        

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
            var data = new PageOutput<DevProjectGetPageOutput> { List = list, Total = total };
        
            return data;
        }
        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> AddAsync(DevProjectAddInput input)
        {
            var entity = Mapper.Map<DevProjectEntity>(input);
            var id = (await _devProjectRepository.InsertAsync(entity)).Id;

            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(DevProjectUpdateInput input)
        {
            var entity = await _devProjectRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                throw ResultOutput.Exception("项目不存在！");
            }

            Mapper.Map(input, entity);
            await _devProjectRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _devProjectRepository.DeleteAsync(id) > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            return await _devProjectRepository.Where(w=>ids.Contains(w.Id)).ToDelete().ExecuteAffrowsAsync() > 0;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _devProjectRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            return await _devProjectRepository.SoftDeleteAsync(ids);
        }
    }
}