
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

using ZhonTai.Module.Dev.Domain.DevProjectModel;
using ZhonTai.Module.Dev.Services.DevProjectModel.Dto;
using ZhonTai.Module.Dev.Core.Consts;


namespace ZhonTai.Module.Dev.Services.DevProjectModel
{
    /// <summary>
    /// 项目模型服务
    /// </summary>
    [DynamicApi(Area = DevConsts.AreaName)]
    public partial class DevProjectModelService : BaseService, IDevProjectModelService, IDynamicApi
    {
        private IDevProjectModelRepository _devProjectModelRepository => LazyGetRequiredService<IDevProjectModelRepository>();

        public DevProjectModelService()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DevProjectModelGetOutput> GetAsync(long id)
        {
            var output = await _devProjectModelRepository.GetAsync<DevProjectModelGetOutput>(id);
            return output;
        }
        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevProjectModelGetListOutput>> GetListAsync(DevProjectModelGetListInput input)
        {
            var list = await _devProjectModelRepository.Select
                .WhereIf(input.ProjectId != null, a=>a.ProjectId == input.ProjectId)
                .WhereIf(!string.IsNullOrEmpty(input.Name), a=>a.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code), a=>a.Code == input.Code)
                .OrderByDescending(a => a.Id)
                .ToListAsync<DevProjectModelGetListOutput>();
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DevProjectModelGetPageOutput>> GetPageAsync(PageInput<DevProjectModelGetPageInput> input)
        {
            var filter = input.Filter;
            var list = await _devProjectModelRepository.Select
                .WhereDynamicFilter(input.DynamicFilter)
                .WhereIf(filter !=null && filter.ProjectId != null, a=>a.ProjectId == filter.ProjectId)
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Name), a=> a.Name != null && a.Name.Contains(filter.Name))
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Code), a=> a.Code != null && a.Code.Contains(filter.Code))
                .Count(out var total)
                .OrderByDescending(c => c.Id)
                .Page(input.CurrentPage, input.PageSize)
                .ToListAsync<DevProjectModelGetPageOutput>();
        

            //关联查询代码
            //数据转换-单个关联
            var projectIdRows = list.Where(s => s.ProjectId > 0).ToList();
            if (projectIdRows.Any())
            {
                var projectIdRepo = LazyGetRequiredService<Domain.DevProject.IDevProjectRepository>();
                var projectIdRowsIds = projectIdRows.Select(s => s.ProjectId).Distinct().ToList();
                var projectIdRowsIdsData = await projectIdRepo.Where(s => projectIdRowsIds.Contains(s.Id)).ToListAsync(s => new { s.Id, s.Name });
                projectIdRows.ForEach(s =>
                {
                    s.ProjectId_Text = projectIdRowsIdsData.FirstOrDefault(s2 => s2.Id == s.ProjectId)?.Name;
                });
            }

            var data = new PageOutput<DevProjectModelGetPageOutput> { List = list, Total = total };
        
            return data;
        }
        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> AddAsync(DevProjectModelAddInput input)
        {
            var entity = Mapper.Map<DevProjectModelEntity>(input);
            var id = (await _devProjectModelRepository.InsertAsync(entity)).Id;

            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(DevProjectModelUpdateInput input)
        {
            var entity = await _devProjectModelRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                throw ResultOutput.Exception("项目模型不存在！");
            }

            Mapper.Map(input, entity);
            await _devProjectModelRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _devProjectModelRepository.DeleteAsync(id) > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            return await _devProjectModelRepository.Where(w=>ids.Contains(w.Id)).ToDelete().ExecuteAffrowsAsync() > 0;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _devProjectModelRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            return await _devProjectModelRepository.SoftDeleteAsync(ids);
        }
    }
}