﻿using System.ComponentModel;

namespace Emo.Dev.Api.Core.Consts;

/// <summary>
/// 数据库键名
/// </summary>
public class DbKeys
{
    /// <summary>
    /// 数据库注册键
    /// </summary>
    [Description("数据库注册键")]
    public static string AppDb { get; set; } = "appdb";
}