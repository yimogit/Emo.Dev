
using ZhonTai.Admin.Core.Attributes;
using System.ComponentModel;

namespace ZhonTai.Module.Dev.Core.Consts;

public static partial class DevConsts
{
    /// <summary>
    /// 默认域
    /// </summary>
    public const string AreaName = "dev";

}

/// <summary>
/// 数据库键名
/// </summary>
public class DbKeys
{
    /// <summary>
    /// 数据库注册键
    /// </summary>
    [Description("数据库注册键")]
    public static string AppDb { get; set; } = "admindb";

}

/// <summary>
/// 缓存键
/// </summary>
[ScanCacheKeys]
public static partial class CacheKeys
{

}