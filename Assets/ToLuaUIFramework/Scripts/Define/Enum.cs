namespace ToLuaUIFramework
{
    /// <summary>
    /// 全局命令枚举
    /// </summary>
    public enum CommandEnum
    {
        /// <summary>
        /// 开始增量更新远程AssetBundle
        /// </summary>
        UpdateRemoteAssetBundle,

        /// <summary>
        /// 开始执行Main.lua
        /// </summary>
        StartLuaMain,

        /// <summary>
        /// 背景音乐开启
        /// </summary>
        BGM_Enable,

        /// <summary>
        /// 背景音乐关闭
        /// </summary>
        BGM_Disable

    }

    /// <summary>
    /// 全局事件枚举
    /// </summary>
    public enum MsgEnum
    {
        /// <summary>
        /// 更新AssetBundle开始
        /// </summary>
        ABLoadingBegin,

        /// <summary>
        /// 更新AssetBundle进度
        /// </summary>
        ABLoadingProgress,

        /// <summary>
        /// 更新AssetBundle出错
        /// </summary>
        ABLoadingError,

        /// <summary>
        /// 更新AssetBundle结束
        /// </summary>
        ABLoadingFinish,

        /// <summary>
        /// 本地资源初始化完成
        /// </summary>
        ResInited


    }
}