using System;
using FreeSql.DataAnnotations;
using ZhonTai.Admin.Core.Entities;
using ZhonTai.Module.Dev.Domain.DevProjectModel;    

#pragma warning disable CS8632

namespace ZhonTai.Module.Dev.Domain.DevProjectModelField
{
    /// <summary>
    /// 项目模型字段 实体类
    /// </summary>
    /// <remarks></remarks>
    [Table(Name="dev_project_model_field")]
    public partial class DevProjectModelFieldEntity: EntityBase
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        /// <remarks></remarks>
        [Column(StringLength=200)]
        public string Name { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        /// <remarks></remarks>
        
        public string? Description { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        /// <remarks></remarks>
        [Column(StringLength=64)]
        public string? DataType { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        /// <remarks></remarks>
        [Column(StringLength=64)]
        public string? IsRequired { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        /// <remarks></remarks>
        
        public int? MaxLength { get; set; }
        /// <summary>
        /// 最小长度
        /// </summary>
        /// <remarks></remarks>
        
        public int? MinLength { get; set; }
        /// <summary>
        /// 模型Id
        /// </summary>
        /// <remarks></remarks>
        [Column(Precision = 64)]
        public long ModelId { get; set; }
    }

}

#pragma warning restore CS8632

