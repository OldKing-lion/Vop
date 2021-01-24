﻿using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Vop.Api.DynamicApiController
{
    /// <summary>
    /// 动态接口控制器配置
    /// </summary>
    public class DynamicApiControllerSettingsOptions
    {
        /// <summary>
        /// 默认路由前缀
        /// </summary>
        public string DefaultRoutePrefix { get; set; }

        /// <summary>
        /// 默认请求谓词
        /// </summary>
        public string DefaultHttpMethod { get; set; }

        /// <summary>
        /// 默认模块（区域）名称
        /// </summary>
        public string DefaultModule { get; set; }

        /// <summary>
        /// 小写路由
        /// </summary>
        public bool? LowerCaseRoute { get; set; }

        /// <summary>
        /// 驼峰路由
        /// </summary>
        public bool? CamelCaseRoute { get; set; }

        /// <summary>
        /// 保留行为名称谓词
        /// </summary>
        public bool? KeepVerb { get; set; }

        /// <summary>
        /// 保留名称
        /// </summary>
        public bool? KeepName { get; set; }

        /// <summary>
        /// 骆驼命名分隔符
        /// </summary>
        public string CamelCaseSeparator { get; set; }

        /// <summary>
        /// 版本号分隔符
        /// </summary>
        public string VersionSeparator { get; set; }

        /// <summary>
        /// 模型转查询参数（只有GET、HEAD请求有效）
        /// </summary>
        public bool? ModelToQuery { get; set; }

        /// <summary>
        /// 支持Mvc控制器处理
        /// </summary>
        public bool? SupportedMvcController { get; set; }

        /// <summary>
        /// 被舍弃的控制器名称前后缀
        /// </summary>
        public string[] AbandonControllerAffixes { get; set; }

        /// <summary>
        /// 被舍弃的行为名称前后缀
        /// </summary>
        public string[] AbandonActionAffixes { get; set; }
    }
}