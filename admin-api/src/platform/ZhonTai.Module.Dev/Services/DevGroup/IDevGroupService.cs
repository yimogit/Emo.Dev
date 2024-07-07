using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Services.DevGroup.Dto;

namespace ZhonTai.Module.Dev.Services.DevGroup
{
    /// <summary>
    /// 模板组服务
    /// </summary>
    public interface IDevGroupService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevGroupGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevGroupGetPageOutput>> GetPageAsync(PageInput<DevGroupGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevGroupGetListOutput>> GetListAsync(DevGroupGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevGroupAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevGroupUpdateInput input);
        
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

namespace ZhonTai.Module.Dev.Services.DevGroup.Dto
{
    
    /// <summary>模板组列表查询结果输出</summary>
    public partial class DevGroupGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>模板组名称</summary>
        public string Name { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }
    /// <summary>模板组列表查询条件输入</summary>
    public partial class DevGroupGetListInput : DevGroupGetPageInput {
    }

    /// <summary>模板组查询结果输出</summary>
    public partial class DevGroupGetOutput {
        public long Id { get; set; }
        /// <summary>模板组名称</summary>
        public string Name { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>模板组分页查询结果输出</summary>
    public partial class DevGroupGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>模板组名称</summary>
        public string Name { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>模板组分页查询条件输入</summary>
    public partial class DevGroupGetPageInput {

        /// <summary>模板组名称</summary>       
        public string? Name { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public long? Id { get; set; }
    }
    
    /// <summary>模板组新增输入</summary>
    public partial class DevGroupAddInput {
        /// <summary>模板组名称</summary>
        [Required(ErrorMessage = "模板组名称不能为空")]
        public string Name { get; set; }                                                    
        /// <summary>备注</summary>
        public string? Remark { get; set; }                                                    
    }


    /// <summary>模板组更新数据输入</summary>
    public partial class DevGroupUpdateInput {
        public long Id { get; set; }
        /// <summary>模板组名称</summary>
        [Required(ErrorMessage = "模板组名称不能为空")]
        public string Name { get; set; }
        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }


}