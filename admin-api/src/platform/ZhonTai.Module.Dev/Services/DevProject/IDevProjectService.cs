using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Services.DevProject.Dto;

namespace ZhonTai.Module.Dev.Services.DevProject
{
    /// <summary>
    /// 项目服务
    /// </summary>
    public interface IDevProjectService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevProjectGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevProjectGetPageOutput>> GetPageAsync(PageInput<DevProjectGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevProjectGetListOutput>> GetListAsync(DevProjectGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevProjectAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevProjectUpdateInput input);
        
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

namespace ZhonTai.Module.Dev.Services.DevProject.Dto
{
    
    /// <summary>项目列表查询结果输出</summary>
    public partial class DevProjectGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>项目名称</summary>
        public string Name { get; set; }
        /// <summary>项目编码</summary>
        public string Code { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }
    /// <summary>项目列表查询条件输入</summary>
    public partial class DevProjectGetListInput : DevProjectGetPageInput {
    }

    /// <summary>项目查询结果输出</summary>
    public partial class DevProjectGetOutput {
        public long Id { get; set; }
        /// <summary>项目名称</summary>
        public string Name { get; set; }
        /// <summary>项目编码</summary>
        public string Code { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>项目分页查询结果输出</summary>
    public partial class DevProjectGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>项目名称</summary>
        public string Name { get; set; }
        /// <summary>项目编码</summary>
        public string Code { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>项目分页查询条件输入</summary>
    public partial class DevProjectGetPageInput {

        /// <summary>项目名称</summary>       
        public string? Name { get; set; }
        /// <summary>项目编码</summary>       
        public string? Code { get; set; }
    }
    
    /// <summary>项目新增输入</summary>
    public partial class DevProjectAddInput {
        /// <summary>项目名称</summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string Name { get; set; }                                                    
        /// <summary>项目编码</summary>
        [Required(ErrorMessage = "项目编码不能为空")]
        public string Code { get; set; }                                                    
        /// <summary>备注</summary>
        public string? Remark { get; set; }                                                    
    }


    /// <summary>项目更新数据输入</summary>
    public partial class DevProjectUpdateInput {
        public long Id { get; set; }
        /// <summary>项目名称</summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string Name { get; set; }
        /// <summary>项目编码</summary>
        [Required(ErrorMessage = "项目编码不能为空")]
        public string Code { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }


}