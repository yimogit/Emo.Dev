using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Domain.DevGroup;    
using ZhonTai.Module.Dev.Services.DevTemplate.Dto;

namespace ZhonTai.Module.Dev.Services.DevTemplate
{
    /// <summary>
    /// 模板服务
    /// </summary>
    public interface IDevTemplateService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevTemplateGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevTemplateGetPageOutput>> GetPageAsync(PageInput<DevTemplateGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevTemplateGetListOutput>> GetListAsync(DevTemplateGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevTemplateAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevTemplateUpdateInput input);
        
        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除
        /// </summary>
        Task<bool> BatchDeleteAsync(long[] ids);
        /// <summary>
        /// 软删除
        /// </summary>
        Task<bool> SoftDeleteAsync(long id);
        /// <summary>
        /// 批量软删除
        /// </summary>
        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}

namespace ZhonTai.Module.Dev.Services.DevTemplate.Dto
{
    
    /// <summary>模板列表查询结果输出</summary>
    public partial class DevTemplateGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>模板名称</summary>
        public string Name { get; set; }
        /// <summary>模板分组</summary>
        public long GroupId { get; set; }
        ///<summary>模板分组显示文本</summary>
        public string? GroupId_Text { get; set; }
        /// <summary>生成路径</summary>
        public string? OutTo { get; set; }
        /// <summary>是否禁用</summary>
        public bool IsDisable { get; set; }
        /// <summary>模板内容</summary>
        public string Content { get; set; }
    }
    /// <summary>模板列表查询条件输入</summary>
    public partial class DevTemplateGetListInput : DevTemplateGetPageInput {
    }

    /// <summary>模板查询结果输出</summary>
    public partial class DevTemplateGetOutput {
        public long Id { get; set; }
        /// <summary>模板名称</summary>
        public string Name { get; set; }
        /// <summary>模板分组</summary>
        public long GroupId { get; set; }
        ///<summary>模板分组显示文本</summary>
        public string? GroupId_Text { get; set; }
        /// <summary>生成路径</summary>
        public string? OutTo { get; set; }
        /// <summary>是否禁用</summary>
        public bool IsDisable { get; set; }
        /// <summary>模板内容</summary>
        public string Content { get; set; }
    }

    /// <summary>模板分页查询结果输出</summary>
    public partial class DevTemplateGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>模板名称</summary>
        public string Name { get; set; }
        /// <summary>模板分组</summary>
        public long GroupId { get; set; }
        ///<summary>模板分组显示文本</summary>
        public string? GroupId_Text { get; set; }
        /// <summary>生成路径</summary>
        public string? OutTo { get; set; }
        /// <summary>是否禁用</summary>
        public bool IsDisable { get; set; }
        /// <summary>模板内容</summary>
        public string Content { get; set; }
    }

    /// <summary>模板分页查询条件输入</summary>
    public partial class DevTemplateGetPageInput {

        /// <summary>模板名称</summary>       
        public string? Name { get; set; }
        /// <summary>模板分组</summary>       
        public long? GroupId { get; set; }
    }
    
    /// <summary>模板新增输入</summary>
    public partial class DevTemplateAddInput {
        /// <summary>模板名称</summary>
        [Required(ErrorMessage = "模板名称不能为空")]
        public string Name { get; set; }                                                    
        /// <summary>模板分组</summary>
        [Required(ErrorMessage = "模板分组不能为空")]
        public long GroupId { get; set; }                                                    
        /// <summary>生成路径</summary>
        public string? OutTo { get; set; }                                                    
        /// <summary>是否禁用</summary>
        [Required(ErrorMessage = "是否禁用不能为空")]
        public bool IsDisable { get; set; }                                                    
        /// <summary>模板内容</summary>
        [Required(ErrorMessage = "模板内容不能为空")]
        public string Content { get; set; }                                                    
    }


    /// <summary>模板更新数据输入</summary>
    public partial class DevTemplateUpdateInput {
        public long Id { get; set; }
        /// <summary>模板名称</summary>
        [Required(ErrorMessage = "模板名称不能为空")]
        public string Name { get; set; }
        /// <summary>模板分组</summary>
        [Required(ErrorMessage = "模板分组不能为空")]
        public long GroupId { get; set; }
        /// <summary>生成路径</summary>
        public string? OutTo { get; set; }
        /// <summary>是否禁用</summary>
        [Required(ErrorMessage = "是否禁用不能为空")]
        public bool IsDisable { get; set; }
        /// <summary>模板内容</summary>
        [Required(ErrorMessage = "模板内容不能为空")]
        public string Content { get; set; }
    }


}