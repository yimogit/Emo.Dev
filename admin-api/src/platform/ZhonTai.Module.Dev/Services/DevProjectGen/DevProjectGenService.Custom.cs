
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;


using ZhonTai.Admin.Core.Dto;
using ZhonTai.Admin.Services;
using ZhonTai.DynamicApi;
using ZhonTai.DynamicApi.Attributes;
using ZhonTai.Admin.Domain.Dict;

using ZhonTai.Module.Dev.Domain.DevProjectGen;
using ZhonTai.Module.Dev.Services.DevProjectGen.Dto;
using ZhonTai.Module.Dev.Core.Consts;
using ZhonTai.Module.Dev.Services.DevProjectGen.Input;
using ZhonTai.Module.Dev.Domain.DevGroup;
using ZhonTai.Module.Dev.Services.DevProjectGen.Output;
using ZhonTai.Module.Dev.Domain.DevTemplate;
using RazorEngine.Templating;
using System.Diagnostics;
using ZhonTai.Module.Dev.Domain.DevProject;
using ZhonTai.Module.Dev.Domain.DevProjectModel;
using ZhonTai.Module.Dev.Domain.DevProjectModelField;
using AngleSharp.Common;
using ZhonTai.Module.Dev.Services.DevProject.Dto;
using ZhonTai.Module.Dev.Services.DevProjectModel.Dto;
using ZhonTai.Module.Dev.Services.DevProjectModelField.Dto;
using System.IO.Compression;
using ZhonTai.Common.Helpers;


namespace ZhonTai.Module.Dev.Services.DevProjectGen
{
    /// <summary>
    /// 项目生成服务
    /// </summary>
    public partial class DevProjectGenService
    {
        private IDevGroupRepository devGroupRepository => LazyGetRequiredService<IDevGroupRepository>();
        private IDevTemplateRepository devTemplateRepository => LazyGetRequiredService<IDevTemplateRepository>();

        /// <summary>
        /// 生成预览菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<DevProjectGenPreviewMenuOutput>> GetPreviewMenuAsync(DevProjectGenPreviewMenuInput input)
        {
            var list = await devGroupRepository.Select
                .WhereIf(input.GroupIds != null && input.GroupIds.Any(), a => input.GroupIds.Contains(a.Id))
                .ToListAsync<DevProjectGenPreviewMenuOutput>(s => new DevProjectGenPreviewMenuOutput
                {
                    GroupId = s.Id,
                    GroupName = s.Name,
                });
            var groupIds = list.Select(s => s.GroupId).ToList();
            var templates = await devTemplateRepository.Select
                .Where(s => groupIds.Contains(s.GroupId))
                .WhereIf(input.TemplateStatus.HasValue, s => s.IsEnable == input.TemplateStatus)
                .ToListAsync<DevProjectGenPreviewTemplateOutput>(s => new DevProjectGenPreviewTemplateOutput()
                {
                    GroupId = s.GroupId,
                    TemplateId = s.Id,
                    TemplateName = s.Name,
                    TempaltePath = s.OutTo,
                    IsEnable = s.IsEnable
                });
            //查询分组下模板
            list.ForEach(s =>
            {
                //模板组
                s.TemplateList = templates.Where(s2 => s2.GroupId == s.GroupId).ToList();
            });
            return list;
        }
        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<DevProjectGenGenerateOutput>> GenerateAsync(DevProjectGenGenerateInput input)
        {
            var devProjectRepository = LazyGetRequiredService<IDevProjectRepository>();
            var devProjectModelRepository = LazyGetRequiredService<IDevProjectModelRepository>();
            var devProjectModelFieldRepository = LazyGetRequiredService<IDevProjectModelFieldRepository>();
            //项目
            var project = await devProjectRepository
                    .Where(w => w.Id == input.ProjectId)
                    .FirstAsync();
            if (project == null)
                throw ResultOutput.Exception(msg: "项目不存在");
            //模型
            var models = devProjectModelRepository.Where(s => s.ProjectId == project.Id && s.IsEnable)
                .WhereIf(input.ModelIds != null && input.ModelIds.Any(), s => input.ModelIds.Contains(s.Id))
                .ToList();
            var modelIds = models.Select(s => s.Id).ToList();
            //字段
            var fields = devProjectModelFieldRepository.Where(s => s.ModelId > 0 && modelIds.Contains(s.ModelId.Value)).ToList();
            if (!input.GroupIds.Any() && !input.TemplateIds.Any())
            {
                throw ResultOutput.Exception(msg: "分组和模板必须要传递一个");
            }
            //模板
            var templates = devTemplateRepository
                .WhereIf(input.GroupIds != null && input.GroupIds.Any(), s => input.GroupIds.Contains(s.GroupId))
                .WhereIf(input.TemplateIds != null && input.TemplateIds.Any(), s => input.TemplateIds.Contains(s.Id))
                .ToList();
            try
            {
                RazorEngine.Engine.Razor = RazorEngineService.Create(new RazorEngine.Configuration.
                    TemplateServiceConfiguration
                {
                    EncodedStringFactory = new RazorEngine.Text.RawStringFactory()// Raw string encoding.
                });
            }
            catch (Exception ex)
            {
                RazorEngine.Engine.Razor?.Dispose();
                throw ResultOutput.Exception(msg: "初始化编译器失败：" + ex.Message);
            }

            var result = new List<DevProjectGenGenerateOutput>();
            var errors = new List<string>();
            try
            {
                //一个个模型生成
                foreach (var model in models)
                {
                    var modelFields = fields.Where(s => s.ModelId == model.Id).ToList();
                    //模型渲染
                    var gen = new DevProjectRazorRenderModel()
                    {
                        Project = Mapper.Map<DevProjectGetOutput>(project),
                        Model = Mapper.Map<DevProjectModelGetOutput>(model),
                        Fields = Mapper.Map<List<DevProjectModelFieldGetOutput>>(modelFields),
                    };
                    //模型是否禁用
                    if (!model.IsEnable) continue;
                    foreach (var tpl in templates)
                    {
                        //模板是否禁用,预览时跳过禁用判断
                        if (!input.IsPreview && !tpl.IsEnable) continue;
                        if (string.IsNullOrEmpty(tpl.OutTo))
                        {
                            errors.Add($"生成【{tpl.Name}】输出路径为空");
                            break;
                        }
                        //路径需要找个方法替换
                        var outPath = tpl.OutTo;
                        var codeText = tpl.Content;
                        try
                        {
                            var pathCodeText = @"
@{
    var gen = Model as ZhonTai.Module.Dev.DevProjectRazorRenderModel;
}
" + outPath;
                            ////转换路径
                            outPath = RazorCompile(gen, $"{project.Code}_{model.Code}_{tpl.Name}_Path.tpl", pathCodeText).Trim();
                            result.Add(new DevProjectGenGenerateOutput()
                            {
                                TemplateId = tpl.Id,
                                Path = outPath,
                                Content = RazorCompile(gen, $"{project.Code}_{model.Code}_{tpl.Name}.tpl", codeText)
                            });
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"生成【{tpl.Name}】时发生错误：{ex.Message}");
                        }
                    }
                }
                if (errors.Count > 0)
                    throw ResultOutput.Exception(string.Join("\n", errors));
            }
            catch (Exception ex)
            {
                throw ResultOutput.Exception(ex.Message);
            }
            finally
            {
                RazorEngine.Engine.Razor.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DownAsync(DevProjectGenGenerateInput input)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "DownCodes", DateTime.Now.ToString("yyyyMMddHHmmss"));
            var zipFileName = $"源码{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip";
            var zipPath = Path.Combine(AppContext.BaseDirectory, "DownCodes", zipFileName);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var codes = await GenerateAsync(input);
                foreach (var code in codes)
                {
                    var codePath = Path.Combine(path, code.Path);
                    var directory = Path.GetDirectoryName(codePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    if (!File.Exists(codePath))
                    {
                        using (var fs = File.Open(codePath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            await fs.WriteAsync(Encoding.UTF8.GetBytes(code.Content));
                        }
                    }
                }
                ZipFile.CreateFromDirectory(path, zipPath);
                var bytes = await File.ReadAllBytesAsync(zipPath);
                return new FileContentResult(bytes, "application/zip")
                {
                    FileDownloadName = zipFileName
                };
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
            }
        }

        private string RazorCompile(DevProjectRazorRenderModel model, string key, string code)
        {
            try
            {
                var razor = RazorEngine.Engine.Razor;
                return razor.RunCompile(new LoadedTemplateSource(code), key, model.GetType(), model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}