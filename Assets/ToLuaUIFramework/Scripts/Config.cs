using UnityEngine;

namespace ToLuaUIFramework
{
    /// <summary>
    /// 项目配置
    /// </summary>
    public class Config : MonoBehaviour
    {
        /// <summary>
        /// 是否使用AssetBundle,开发时使用本地Resources目录，写完代码直接启动测试
        /// </summary>
        public const bool UseAssetBundle = false;

        /// <summary>
        /// 开发专用目录，放在Resources里方便写完代码直接启动测试，正式发布时因为使用外部加载的AB，所以该目录需要临时排除不打入包内
        /// </summary>
        public readonly static string GameResourcesPath = Application.dataPath + "/Game/Resources";

        /// <summary>
        /// 导出AB包的路径，导出后资源从该目录上传到远程服务器，并本地清除，切勿留着导入包内，建议定义在工程目录外，如"E:/ExportAssetBundles"
        /// </summary>
        public readonly static string OutputPath = Application.streamingAssetsPath;

        /// <summary>
        /// 远程服务器上AB资源网址(如：http://xxx.xxx.xxx.xxx:8081/res)
        /// </summary>
        public readonly static string RemoteUrl = "file://" + Application.streamingAssetsPath;

        /// <summary>
        /// 需要导出AssetBundle的Lua代码目录
        /// </summary>
        public static string[] ExportLuaPaths =
        {
            //Framework
            LuaConst.luaDir,
            LuaConst.toluaDir,
            //Game
            GameResourcesPath + "/Lua"
        };

        /// <summary>
        /// 需要导出AssetBundle的所有Prefabs目录
        /// </summary>
        public static string ExportResPath = GameResourcesPath + "/Prefabs";

    }
}