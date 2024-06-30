
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

using ZhonTai.Module.Dev.Domain.DevProjectModelField;
using ZhonTai.Module.Dev.Services.DevProjectModelField.Dto;
using ZhonTai.Module.Dev.Core.Consts;


namespace ZhonTai.Module.Dev.Services.DevProjectModelField
{
    /// <summary>
    /// 项目模型字段服务
    /// </summary>
    [DynamicApi(Area = DevConsts.AreaName)]
    public class DevProjectModelFieldService : BaseService, IDevProjectModelFieldService, IDynamicApi
    {
        private IDevProjectModelFieldRepository _devProjectModelFieldRepository => LazyGetRequiredService<IDevProjectModelFieldRepository>();

        public DevProjectModelFieldService()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DevProjectModelFieldGetOutput> GetAsync(long id)
        {
            var output = await _devProjectModelFieldRepository.GetAsync<DevProjectModelFieldGetOutput>(id);
            return output;
        }
        
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevProjectModelFieldGetListOutput>> GetListAsync(DevProjectModelFieldGetListInput input)
        {
            var list = await _devProjectModelFieldRepository.Select
                .WhereIf(!string.IsNullOrEmpty(input.Name), a=>a.Name == input.Name)
                .WhereIf(input.ModelId != null, a=>a.ModelId == input.ModelId)
                .OrderByDescending(a => a.Id)
                .ToListAsync<DevProjectModelFieldGetListOutput>();
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageOutput<DevProjectModelFieldGetPageOutput>> GetPageAsync(PageInput<DevProjectModelFieldGetPageInput> input)
        {
            var filter = input.Filter;
            var list = await _devProjectModelFieldRepository.Select
                .WhereDynamicFilter(input.DynamicFilter)
                .WhereIf(filter !=null && !string.IsNullOrEmpty(filter.Name), a=>a.Name == filter.Name)
                .WhereIf(filter !=null && filter.ModelId != null, a=>a.ModelId == filter.ModelId)
                .Count(out var total)
                .OrderByDescending(c => c.Id)
                .Page(input.CurrentPage, input.PageSize)
                .ToListAsync<DevProjectModelFieldGetPageOutput>();
        

            //关联查询代码
            //数据转换-单个关联
            var modelIdRows = list.Where(s => s.ModelId > 0).ToList();
            if (modelIdRows.Any())
            {
                var modelIdRepo = LazyGetRequiredService<Domain.DevProjectModel.IDevProjectModelRepository>();
                var modelIdRowsIds = modelIdRows.Select(s => s.ModelId).Distinct().ToList();
                var modelIdRowsIdsData = await modelIdRepo.Where(s => modelIdRowsIds.Contains(s.Id)).ToListAsync(s => new { s.Id, s.Name });
                modelIdRows.ForEach(s =>
                {
                    s.ModelId_Text = modelIdRowsIdsData.FirstOrDefault(s2 => s2.Id == s.ModelId)?.Name;
                });
            }

            var data = new PageOutput<DevProjectModelFieldGetPageOutput> { List = list, Total = total };
        
            return data;
        }
        

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> AddAsync(DevProjectModelFieldAddInput input)
        {
            var entity = Mapper.Map<DevProjectModelFieldEntity>(input);
            var id = (await _devProjectModelFieldRepository.InsertAsync(entity)).Id;

            return id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task UpdateAsync(DevProjectModelFieldUpdateInput input)
        {
            var entity = await _devProjectModelFieldRepository.GetAsync(input.Id);
            if (!(entity?.Id > 0))
            {
                throw ResultOutput.Exception("项目模型字段不存在！");
            }

            Mapper.Map(input, entity);
            await _devProjectModelFieldRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _devProjectModelFieldRepository.DeleteAsync(id) > 0;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            return await _devProjectModelFieldRepository.Where(w=>ids.Contains(w.Id)).ToDelete().ExecuteAffrowsAsync() > 0;
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDeleteAsync(long id)
        {
            return await _devProjectModelFieldRepository.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> BatchSoftDeleteAsync(long[] ids)
        {
            return await _devProjectModelFieldRepository.SoftDeleteAsync(ids);
        }
    }
}