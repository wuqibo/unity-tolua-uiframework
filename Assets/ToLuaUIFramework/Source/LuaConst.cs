using ToLuaUIFramework;
using UnityEngine;

/// <summary>
/// 框架配置
/// </summary>
public static class LuaConst
{
    public readonly static string frameworkRoot = Application.dataPath + "/ToLuaUIFramework";
    public readonly static string toluaRoot = frameworkRoot + "/ToLua";
    public readonly static string luaDir = frameworkRoot + "/Lua";
    public readonly static string toluaDir = frameworkRoot + "/ToLua/Lua";
    public readonly static string luaEncoderRoot = frameworkRoot + "/LuaEncoder";
    public readonly static string localABPath = Application.persistentDataPath + "/AssetBundle";
    public const string MD5FileName = "files.txt";
    public const string ExtName = ".u3d";
    public static string streamPathUrl
    {
        get
        {
            string path = "file:///" + Application.dataPath + "/StreamingAssets";
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = "file:///" + Application.dataPath + "/Raw/";
                    break;
            }
            return path;
        }
    }

#if UNITY_STANDALONE
    public static string osDir = "Win";
#elif UNITY_ANDROID
    public static string osDir = "Android";
#elif UNITY_IPHONE
    public static string osDir = "iOS";        
#else
    public static string osDir = "";        
#endif

    public static string luaResDir = string.Format("{0}/{1}/Lua", Application.persistentDataPath, osDir);      //手机运行时lua文件下载目录    

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    public static string zbsDir = "D:/ZeroBraneStudio/lualibs/mobdebug";        //ZeroBraneStudio目录       
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
	public static string zbsDir = "/Applications/ZeroBraneStudio.app/Contents/ZeroBraneStudio/lualibs/mobdebug";
#else
    public static string zbsDir = luaResDir + "/mobdebug/";
#endif

    public static bool openLuaSocket = true;            //是否打开Lua Socket库
    public static bool openLuaDebugger = false;         //是否连接lua调试器
}
