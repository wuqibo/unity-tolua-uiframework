using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using ToLuaUIFramework;

public class Packager
{
    public static string platform = string.Empty;
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();
    static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

    [MenuItem("ToLuaUIFramework/Build iPhone AssetBundle", false, 100)]
    public static void BuildiPhoneResource()
    {
        BuildAssetResource(BuildTarget.iOS, 0);
    }

    [MenuItem("ToLuaUIFramework/Build Android AssetBundle/All", false, 101)]
    public static void BuildAndroidResourceAll()
    {
        BuildAssetResource(BuildTarget.Android, 0);
    }

    [MenuItem("ToLuaUIFramework/Build Android AssetBundle/Lua", false, 101)]
    public static void BuildAndroidResourceLua()
    {
        BuildAssetResource(BuildTarget.Android, 1);
    }

    [MenuItem("ToLuaUIFramework/Build Android AssetBundle/Res", false, 101)]
    public static void BuildAndroidResourceRes()
    {
        BuildAssetResource(BuildTarget.Android, 2);
    }

    [MenuItem("ToLuaUIFramework/Build Windows AssetBundle", false, 102)]
    public static void BuildWindowsResource()
    {
        BuildAssetResource(BuildTarget.StandaloneWindows, 0);
    }

    /// <summary>
    /// 生成绑定素材(mode:0全部导出 1导出lua脚本 2导出资源)
    /// </summary>
    public static void BuildAssetResource(BuildTarget target, int mode)
    {
        string outputPath = Config.OutputPath;
        string delPath = outputPath;
        if (mode == 1)
        {
            delPath += "/lua";
        }
        else if (mode == 2)
        {
            delPath += "/res";
        }
        if (Directory.Exists(delPath))
        {
            Directory.Delete(delPath, true);
        }
        Directory.CreateDirectory(delPath);
        AssetDatabase.Refresh();

        maps.Clear();

        string tempDir = Application.dataPath + "/temp";
        if (mode == 0)
        {
            HandleLuaBundle(tempDir);
            HandleResBundle();
        }
        else if (mode == 1)
        {
            HandleLuaBundle(tempDir);
        }
        else if (mode == 2)
        {
            HandleResBundle();
        }
        AssetDatabase.Refresh();

        string relativePath = outputPath.Substring(outputPath.IndexOf("Assets/"));
        BuildPipeline.BuildAssetBundles(relativePath, maps.ToArray(), BuildAssetBundleOptions.None, target);
        BuildFileIndex();

        if (mode == 0 || mode == 1)
        {
            Directory.Delete(tempDir, true);
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 处理Lua的AssetBundle
    /// </summary>
    static void HandleLuaBundle(string tempDir)
    {

        if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

        string[] srcDirs = Config.ExportLuaPaths;
        for (int i = 0; i < srcDirs.Length; i++)
        {
            string sourceDir = srcDirs[i];
            string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
            int len = sourceDir.Length;

            if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
            {
                --len;
            }
            for (int j = 0; j < files.Length; j++)
            {
                string str = files[j].Remove(0, len);
                string dest = tempDir + str + ".bytes";
                string dir = Path.GetDirectoryName(dest);
                Directory.CreateDirectory(dir);
                EncodeLuaFile(files[j], dest);
            }
        }
        string[] dirs = Directory.GetDirectories(tempDir, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++)
        {
            string name = dirs[i].Replace(tempDir, string.Empty);
            name = name.Replace('\\', '_').Replace('/', '_');
            name = "lua/lua" + name.ToLower() + LuaConst.ExtName;
            string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
            AddBuildMap(name, "*.bytes", path);
        }
        AddBuildMap("lua/lua" + LuaConst.ExtName, "*.bytes", tempDir.Substring(tempDir.IndexOf("Assets/")));

        //-------------------------------处理非Lua文件(如Build.bat文件等)----------------------------------
        string luaPath = Application.streamingAssetsPath + "/lua/";
        for (int i = 0; i < srcDirs.Length; i++)
        {
            paths.Clear(); files.Clear();
            string luaDataPath = srcDirs[i].ToLower();
            Recursive(luaDataPath);
            foreach (string f in files)
            {
                if (f.EndsWith(".meta") || f.EndsWith(".lua")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string path = Path.GetDirectoryName(luaPath + newfile);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string destfile = path + "/" + Path.GetFileName(f);
                File.Copy(f, destfile, true);
            }
        }
    }

    /// <summary>
    /// 处理Res的AssetBundle
    /// </summary>
    static void HandleResBundle()
    {
        string name = "res/" + Config.ExportResPath.Substring(Config.ExportResPath.LastIndexOf("/") + 1);
        string path = "Assets" + Config.ExportResPath.Replace(Application.dataPath, "");
        AddBuildMap(name + LuaConst.ExtName, "*.prefab", path);

        string[] typePaths = Directory.GetDirectories(Config.ExportResPath);
        for (int i = 0; i < typePaths.Length; i++)
        {
            string typePath = typePaths[i];
            name = typePath.Substring(typePath.LastIndexOf("/") + 1).Replace("\\", "_").Replace("/", "_");
            name = "res/" + name;
            path = "Assets" + typePath.Replace(Application.dataPath, "");
            AddBuildMap(name + LuaConst.ExtName, "*.prefab", path);

            string[] modulePaths = Directory.GetDirectories(typePath);
            for (int j = 0; j < modulePaths.Length; j++)
            {
                string modulePath = modulePaths[j];
                name = modulePath.Substring(modulePath.LastIndexOf("/") + 1).Replace("\\", "_").Replace("/", "_");
                name = "res/" + name;
                path = "Assets" + modulePath.Replace(Application.dataPath, "");
                AddBuildMap(name + LuaConst.ExtName, "*.prefab", path);
            }
        }
    }
    static void AddBuildMap(string bundleName, string pattern, string path)
    {
        string[] files = Directory.GetFiles(path, pattern);
        if (files.Length == 0) return;

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');
        }
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = bundleName;
        build.assetNames = files;
        maps.Add(build);
    }

    /// <summary>
    /// 创建资源MD5列表，以便检查更新
    /// </summary>
    static void BuildFileIndex()
    {
        string resPath = Application.streamingAssetsPath;
        string newFilePath = resPath + "/" + LuaConst.MD5FileName;
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

            string md5 = Utils.md5file(file);
            string value = file.Replace(resPath + "/", string.Empty);
            sw.WriteLine(value + "|" + md5);
        }
        sw.Close(); fs.Close();
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    static void UpdateProgress(int progress, int progressMax, string desc)
    {
        string title = "Processing...[" + progress + " - " + progressMax + "]";
        float value = (float)progress / (float)progressMax;
        EditorUtility.DisplayProgressBar(title, desc, value);
    }

    public static void EncodeLuaFile(string srcFile, string outFile)
    {
        if (!srcFile.ToLower().EndsWith(".lua"))
        {
            File.Copy(srcFile, outFile, true);
            return;
        }
        bool isWin = true;
        string luaexe = string.Empty;
        string args = string.Empty;
        string exedir = string.Empty;
        string currDir = Directory.GetCurrentDirectory();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            isWin = true;
            luaexe = "luajit.exe";
            args = "-b -g " + srcFile + " " + outFile;
            exedir = LuaConst.frameworkRoot + "/LuaEncoder/luajit/";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            isWin = false;
            luaexe = "./luajit";
            args = "-b -g " + srcFile + " " + outFile;
            exedir = LuaConst.frameworkRoot + "/LuaEncoder/luajit_mac/";
        }
        Directory.SetCurrentDirectory(exedir);
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = luaexe;
        info.Arguments = args;
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.UseShellExecute = isWin;
        info.ErrorDialog = true;

        Process pro = Process.Start(info);
        pro.WaitForExit();
        Directory.SetCurrentDirectory(currDir);
    }

}