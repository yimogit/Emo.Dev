using System;
using FreeSql.DataAnnotations;
using ZhonTai.Admin.Core.Entities;
using ZhonTai.Module.Dev.Domain.DevGroup;    

#pragma warning disable CS8632

namespace ZhonTai.Module.Dev.Domain.DevTemplate
{
    /// <summary>
    /// 模板 实体类
    /// </summary>
    /// <remarks></remarks>
    [Table(Name="dev_template")]
    public partial class DevTemplateEntity: EntityBase
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=1, StringLength=200)]
        public string Name { get; set; }
        /// <summary>
        /// 模板分组
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=2, Precision = 64)]
        public long GroupId { get; set; }
        /// <summary>
        /// 生成路径
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=3, StringLength=500)]
        public string? Path { get; set; }
        /// <summary>
        /// 模板内容
        /// </summary>
        /// <remarks></remarks>
        [Column(Position=5, DbType="text")]
        public string Content { get; set; }
    }

}

#pragma warning restore CS8632

