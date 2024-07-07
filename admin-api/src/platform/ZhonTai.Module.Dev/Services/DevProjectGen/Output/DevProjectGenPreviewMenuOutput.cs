using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhonTai.Module.Dev.Services.DevProjectGen.Output
{
    public class DevProjectGenPreviewMenuOutput
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 模板列表
        /// </summary>
        public List<DevProjectGenPreviewTemplateOutput> TemplateList { get; set; }
    }
    public class DevProjectGenPreviewTemplateOutput
    {
        /// <summary>
        /// 模板组Id
        /// </summary>
        public long GroupId { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public long TemplateId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 模板生成路径
        /// </summary>
        public string TempaltePath { get; set; }
    }
}
