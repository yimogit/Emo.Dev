using System.ComponentModel.DataAnnotations;
using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Core.Entities;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ZhonTai.Module.Dev.Domain.DevProject;    
using ZhonTai.Module.Dev.Services.DevProjectGen.Dto;

namespace ZhonTai.Module.Dev.Services.DevProjectGen
{
    /// <summary>
    /// 项目生成服务
    /// </summary>
    public interface IDevProjectGenService
    {
        /// <summary>
        /// 查询
        /// </summary>
        Task<DevProjectGenGetOutput> GetAsync(long id);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        Task<PageOutput<DevProjectGenGetPageOutput>> GetPageAsync(PageInput<DevProjectGenGetPageInput> input);
        
        
        /// <summary>
        /// 列表查询
        /// </summary>
        Task<IEnumerable<DevProjectGenGetListOutput>> GetListAsync(DevProjectGenGetListInput input);
    
        /// <summary>
        /// 新增
        /// </summary>
        Task<long> AddAsync(DevProjectGenAddInput input);
        
        /// <summary>
        /// 编辑
        /// </summary>
        Task UpdateAsync(DevProjectGenUpdateInput input);
        
        /// <summary>
        /// 删除
        /// </summary>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除
        /// </summary>
        Task<bool> BatchDeleteAsync(long[] ids);
        /// <summary>
        /// 批量软删除
        /// </summary>
        Task<bool> BatchSoftDeleteAsync(long[] ids);
    }
}

namespace ZhonTai.Module.Dev.Services.DevProjectGen.Dto
{
    
    /// <summary>项目生成列表查询结果输出</summary>
    public partial class DevProjectGenGetListOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模板组</summary>
        public string GroupIds { get; set; }
        ///<summary>模板组显示文本</summary>
        public List<string>? GroupIds_Texts { get; set; }
        ///<summary>页面使用的模板组数组</summary>
        public List<string>? GroupIds_Values { get { return GroupIds?.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>(); } }
    }
    /// <summary>项目生成列表查询条件输入</summary>
    public partial class DevProjectGenGetListInput : DevProjectGenGetPageInput {
    }

    /// <summary>项目生成查询结果输出</summary>
    public partial class DevProjectGenGetOutput {
        public long Id { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模板组</summary>
        public string GroupIds { get; set; }
        ///<summary>模板组显示文本</summary>
        public List<string>? GroupIds_Texts { get; set; }
        ///<summary>页面使用的模板组数组</summary>
        public List<string>? GroupIds_Values { get { return GroupIds?.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>(); } }
    }

    /// <summary>项目生成分页查询结果输出</summary>
    public partial class DevProjectGenGetPageOutput {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTime { get; set; }
        /// <summary>所属项目</summary>
        public long ProjectId { get; set; }
        ///<summary>所属项目显示文本</summary>
        public string? ProjectId_Text { get; set; }
        /// <summary>模板组</summary>
        public string GroupIds { get; set; }
        ///<summary>模板组显示文本</summary>
        public List<string>? GroupIds_Texts { get; set; }
        ///<summary>页面使用的模板组数组</summary>
        public List<string>? GroupIds_Values { get { return GroupIds?.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>(); } }
    }

    /// <summary>项目生成分页查询条件输入</summary>
    public partial class DevProjectGenGetPageInput {

        /// <summary>所属项目</summary>       
        public long? ProjectId { get; set; }
    }
    
    /// <summary>项目生成新增输入</summary>
    public partial class DevProjectGenAddInput {
        /// <summary>所属项目</summary>
        [Required(ErrorMessage = "所属项目不能为空")]
        public long ProjectId { get; set; }                                                    
        /// <summary>模板组</summary>
        [Required(ErrorMessage = "模板组不能为空")]
        public string GroupIds { get { return string.Join(',', GroupIds_Values ?? new List<string>()); } }
        ///<summary>页面提交的模板组数组</summary>
        public List<string>? GroupIds_Values { get; set; }                                                    
    }


    /// <summary>项目生成更新数据输入</summary>
    public partial class DevProjectGenUpdateInput {
        public long Id { get; set; }
        /// <summary>所属项目</summary>
        [Required(ErrorMessage = "所属项目不能为空")]
        public long ProjectId { get; set; }
        /// <summary>模板组</summary>
        [Required(ErrorMessage = "模板组不能为空")]
        public string GroupIds { get { return string.Join(',', GroupIds_Values ?? new List<string>()); } }
        ///<summary>页面提交的模板组数组</summary>
        public List<string>? GroupIds_Values { get; set; }
    }


}