using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhonTai.Module.Dev.Services.DevProject.Dto;
using ZhonTai.Module.Dev.Services.DevProjectModel.Dto;
using ZhonTai.Module.Dev.Services.DevProjectModelField.Dto;

namespace ZhonTai.Module.Dev
{
    public class DevProjectRazorRenderModel
    {
        /// <summary>
        /// 项目信息
        /// </summary>
        public DevProjectGetOutput Project { get; set; }
        /// <summary>
        /// 模型信息
        /// </summary>
        public DevProjectModelGetOutput Model { get; set; }
        /// <summary>
        /// 字段信息
        /// </summary>
        public List<DevProjectModelFieldGetOutput> Fields { get; set; }

    }
}
