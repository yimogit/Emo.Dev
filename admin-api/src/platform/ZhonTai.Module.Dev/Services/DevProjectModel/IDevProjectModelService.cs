using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Domain.DevProject;    
using ZhonTai.Module.Dev.Services.DevProjectModel.Dto;

namespace ZhonTai.Module.Dev.Services.DevProjectModel
{
    /// <summary>
    /// 项目模型服务
    /// </summary>
    public interface IDevProjectModelService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevProjectModelGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevProjectModelGetPageOutput>> GetPageAsync(PageInput<DevProjectModelGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevProjectModelGetListOutput>> GetListAsync(DevProjectModelGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevProjectModelAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevProjectModelUpdateInput input);
        
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

namespace ZhonTai.Module.Dev.Services.DevProjectModel.Dto
{
    
    /// <summary>项目模型列表查询结果输出</summary>
    public partial class DevProjectModelGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模型名称</summary>
        public string Name { get; set; }
        /// <summary>模型编码</summary>
        public string Code { get; set; }
        /// <summary>是否启用</summary>
        public bool IsEnable { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }
    /// <summary>项目模型列表查询条件输入</summary>
    public partial class DevProjectModelGetListInput : DevProjectModelGetPageInput {
    }

    /// <summary>项目模型查询结果输出</summary>
    public partial class DevProjectModelGetOutput {
        public long Id { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模型名称</summary>
        public string Name { get; set; }
        /// <summary>模型编码</summary>
        public string Code { get; set; }
        /// <summary>是否启用</summary>
        public bool IsEnable { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>项目模型分页查询结果输出</summary>
    public partial class DevProjectModelGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模型名称</summary>
        public string Name { get; set; }
        /// <summary>模型编码</summary>
        public string Code { get; set; }
        /// <summary>是否启用</summary>
        public bool IsEnable { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>项目模型分页查询条件输入</summary>
    public partial class DevProjectModelGetPageInput {

        /// <summary>所属项目</summary>       
        public long? ProjectId { get; set; }
        /// <summary>模型名称</summary>       
        public string? Name { get; set; }
        /// <summary>模型编码</summary>       
        public string? Code { get; set; }
    }
    
    /// <summary>项目模型新增输入</summary>
    public partial class DevProjectModelAddInput {
        /// <summary>所属项目</summary>
        [Required(ErrorMessage = "所属项目不能为空")]
        public long ProjectId { get; set; }                                                    
        /// <summary>模型名称</summary>
        [Required(ErrorMessage = "模型名称不能为空")]
        public string Name { get; set; }                                                    
        /// <summary>模型编码</summary>
        [Required(ErrorMessage = "模型编码不能为空")]
        public string Code { get; set; }                                                    
        /// <summary>是否启用</summary>
        [Required(ErrorMessage = "是否启用不能为空")]
        public bool IsEnable { get; set; }                                                    
        /// <summary>备注</summary>
        public string? Remark { get; set; }                                                    
    }


    /// <summary>项目模型更新数据输入</summary>
    public partial class DevProjectModelUpdateInput {
        public long Id { get; set; }
        /// <summary>所属项目</summary>
        [Required(ErrorMessage = "所属项目不能为空")]
        public long ProjectId { get; set; }
        /// <summary>模型名称</summary>
        [Required(ErrorMessage = "模型名称不能为空")]
        public string Name { get; set; }
        /// <summary>模型编码</summary>
        [Required(ErrorMessage = "模型编码不能为空")]
        public string Code { get; set; }
        /// <summary>是否启用</summary>
        [Required(ErrorMessage = "是否启用不能为空")]
        public bool IsEnable { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }


}