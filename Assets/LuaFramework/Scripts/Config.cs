using UnityEngine;

namespace LuaFramework
{
    /// <summary>
    /// 项目配置
    /// </summary>
    public class Config : MonoBehaviour
    {
        /// <summary>
        /// 是否使用AssetBundle,否则使用本地Resources目录内资源
        /// </summary>
        public const bool UseAssetBundle = true;

        /// <summary>
        /// 导出AB包的路径，导出后资源将从该目录拷贝到远程服务器目录
        /// </summary>
        public readonly static string OutputABPath = Application.streamingAssetsPath;

        /// <summary>
        /// 远程服务器上AB资源网址
        /// </summary>
        public readonly static string RemoteABUrl = Application.streamingAssetsPath;

        /// <summary>
        /// 需要导出Lua代码AssetBundle的目录
        /// </summary>
        public static string[] ExportLuaPaths =
        {
            //Framework
            LuaConst.luaDir,
            LuaConst.toluaDir,
            //Game
            Application.dataPath + "/Resources/Lua"
        };

        /// <summary>
        /// 需要导出预设体AssetBundle的目录
        /// </summary>
        public static string ExportResPath = Application.dataPath + "/Resources/Prefabs";

    }
}