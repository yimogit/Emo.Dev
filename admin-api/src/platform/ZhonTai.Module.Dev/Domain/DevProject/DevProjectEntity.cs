using System;
using FreeSql.DataAnnotations;
using ZhonTai.Admin.Core.Entities;

#pragma warning disable CS8632

namespace ZhonTai.Module.Dev.Domain.DevProject
{
    /// <summary>
    /// 项目 实体类
    /// </summary>
    /// <remarks></remarks>
    [Table(Name="dev_project")]
    public partial class DevProjectEntity: EntityBase
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <remarks></remarks>
        [Column(StringLength=200)]
        public string Name { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        /// <remarks></remarks>
        [Column(StringLength=200)]
        public string Code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <remarks></remarks>
        
        public string? Remark { get; set; }
    }

}

#pragma warning restore CS8632

