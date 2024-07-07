using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhonTai.Module.Dev.Services.DevProjectGen.Input
{
    /// <summary>
    /// 项目生成模板请求
    /// </summary>
    public class DevProjectGenTemplateInput
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 模板组
        /// </summary>
        public long GroupId { get; set; }
    }
}
