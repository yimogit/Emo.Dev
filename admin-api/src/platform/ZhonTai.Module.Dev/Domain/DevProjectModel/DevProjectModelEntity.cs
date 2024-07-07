using System;
using FreeSql.DataAnnotations;
using ZhonTai.Admin.Core.Entities;
using ZhonTai.Module.Dev.Domain.DevProject;    

#pragma warning disable CS8632

namespace ZhonTai.Module.Dev.Domain.DevProjectModel
{
    /// <summary>
    /// 项目模型 实体类
    /// </summary>
    /// <remarks></remarks>
    [Table(Name="dev_project_model")]
    public partial class DevProjectModelEntity: EntityBase
    {
        /// <summary>
        /// 所属项目
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=1, Precision = 64)]
        public long ProjectId { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=2, StringLength=200)]
        public string Name { get; set; }
        /// <summary>
        /// 模型编码
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=3, StringLength=200)]
        public string Code { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=4)]
        public bool IsEnable { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=5)]
        public string? Remark { get; set; }
    }

}

#pragma warning restore CS8632

