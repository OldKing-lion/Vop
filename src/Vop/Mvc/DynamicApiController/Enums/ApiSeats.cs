﻿namespace Vop.Api.DynamicApiController
{
    /// <summary>
    /// 接口参数位置
    /// </summary>
    public enum ApiSeats
    {
        /// <summary>
        /// 控制器之前
        /// </summary>
        ControllerStart,

        /// <summary>
        /// 控制器之后
        /// </summary>
        ControllerEnd,

        /// <summary>
        /// 行为之前
        /// </summary>
        ActionStart,

        /// <summary>
        /// 行为之后
        /// </summary>
        ActionEnd
    }
}