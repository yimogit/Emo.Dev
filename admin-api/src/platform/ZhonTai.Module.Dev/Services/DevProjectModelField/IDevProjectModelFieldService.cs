using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Domain.DevProjectModel;    
using ZhonTai.Module.Dev.Services.DevProjectModelField.Dto;

namespace ZhonTai.Module.Dev.Services.DevProjectModelField
{
    /// <summary>
    /// 项目模型字段服务
    /// </summary>
    public interface IDevProjectModelFieldService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevProjectModelFieldGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevProjectModelFieldGetPageOutput>> GetPageAsync(PageInput<DevProjectModelFieldGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevProjectModelFieldGetListOutput>> GetListAsync(DevProjectModelFieldGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevProjectModelFieldAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevProjectModelFieldUpdateInput input);
        
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

namespace ZhonTai.Module.Dev.Services.DevProjectModelField.Dto
{
    
    /// <summary>项目模型字段列表查询结果输出</summary>
    public partial class DevProjectModelFieldGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>字段名称</summary>
        public string Name { get; set; }
        /// <summary>字段描述</summary>
        public string? Description { get; set; }
        /// <summary>字段类型</summary>
        public string? DataType { get; set; }
        /// <summary>是否必填</summary>
        public string? IsRequired { get; set; }
        /// <summary>最大长度</summary>
        public int? MaxLength { get; set; }
        /// <summary>最小长度</summary>
        public int? MinLength { get; set; }
        /// <summary>模型Id</summary>
        public long ModelId { get; set; }
        ///<summary>模型Id显示文本</summary>
        public string? ModelId_Text { get; set; }
    }
    /// <summary>项目模型字段列表查询条件输入</summary>
    public partial class DevProjectModelFieldGetListInput : DevProjectModelFieldGetPageInput {
    }

    /// <summary>项目模型字段查询结果输出</summary>
    public partial class DevProjectModelFieldGetOutput {
        public long Id { get; set; }
        /// <summary>字段名称</summary>
        public string Name { get; set; }
        /// <summary>字段描述</summary>
        public string? Description { get; set; }
        /// <summary>字段类型</summary>
        public string? DataType { get; set; }
        /// <summary>是否必填</summary>
        public string? IsRequired { get; set; }
        /// <summary>最大长度</summary>
        public int? MaxLength { get; set; }
        /// <summary>最小长度</summary>
        public int? MinLength { get; set; }
        /// <summary>模型Id</summary>
        public long ModelId { get; set; }
        ///<summary>模型Id显示文本</summary>
        public string? ModelId_Text { get; set; }
    }

    /// <summary>项目模型字段分页查询结果输出</summary>
    public partial class DevProjectModelFieldGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>字段名称</summary>
        public string Name { get; set; }
        /// <summary>字段描述</summary>
        public string? Description { get; set; }
        /// <summary>字段类型</summary>
        public string? DataType { get; set; }
        /// <summary>是否必填</summary>
        public string? IsRequired { get; set; }
        /// <summary>最大长度</summary>
        public int? MaxLength { get; set; }
        /// <summary>最小长度</summary>
        public int? MinLength { get; set; }
        /// <summary>模型Id</summary>
        public long ModelId { get; set; }
        ///<summary>模型Id显示文本</summary>
        public string? ModelId_Text { get; set; }
    }

    /// <summary>项目模型字段分页查询条件输入</summary>
    public partial class DevProjectModelFieldGetPageInput {

        /// <summary>字段名称</summary>       
        public string? Name { get; set; }
        /// <summary>模型Id</summary>       
        public long? ModelId { get; set; }
    }
    
    /// <summary>项目模型字段新增输入</summary>
    public partial class DevProjectModelFieldAddInput {
        /// <summary>字段名称</summary>
        [Required(ErrorMessage = "字段名称不能为空")]
        public string Name { get; set; }                                                    
        /// <summary>字段描述</summary>
        public string? Description { get; set; }                                                    
        /// <summary>字段类型</summary>
        public string? DataType { get; set; }                                                    
        /// <summary>是否必填</summary>
        public string? IsRequired { get; set; }                                                    
        /// <summary>最大长度</summary>
        public int? MaxLength { get; set; }                                                    
        /// <summary>最小长度</summary>
        public int? MinLength { get; set; }                                                    
        /// <summary>模型Id</summary>
        [Required(ErrorMessage = "模型Id不能为空")]
        public long ModelId { get; set; }                                                    
    }


    /// <summary>项目模型字段更新数据输入</summary>
    public partial class DevProjectModelFieldUpdateInput {
        public long Id { get; set; }
        /// <summary>字段名称</summary>
        [Required(ErrorMessage = "字段名称不能为空")]
        public string Name { get; set; }
        /// <summary>字段描述</summary>
        public string? Description { get; set; }
        /// <summary>字段类型</summary>
        public string? DataType { get; set; }
        /// <summary>是否必填</summary>
        public string? IsRequired { get; set; }
        /// <summary>最大长度</summary>
        public int? MaxLength { get; set; }
        /// <summary>最小长度</summary>
        public int? MinLength { get; set; }
        /// <summary>模型Id</summary>
        [Required(ErrorMessage = "模型Id不能为空")]
        public long ModelId { get; set; }
    }


}