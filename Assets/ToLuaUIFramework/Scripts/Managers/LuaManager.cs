using LuaInterface;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class LuaManager : MonoBehaviour, ICommand
    {
        public static LuaManager instance;

        private LuaState lua;
        private LuaLooper loop = null;

        void Awake()
        {
            instance = this;
            LuaFileUtils.Instance.beZip = Config.UseAssetBundle;
            lua = new LuaState();
            OpenLibs();
            OpenCJson();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            DelegateFactory.Init();
            LuaCoroutine.Register(lua, this);
        }

        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        void OpenLibs()
        {
            lua.OpenLibs(LuaDLL.luaopen_pb);
            lua.OpenLibs(LuaDLL.luaopen_ffi);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            lua.OpenLibs(LuaDLL.luaopen_struct);
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_socket_core);
            lua.OpenLibs(LuaDLL.luaopen_mime_core);
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        void OpenCJson()
        {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        public bool ExeCommand(CommandEnum command)
        {
            if (command == CommandEnum.StartLuaMain)
            {
                if (Config.UseAssetBundle)
                {
                    InitLuaBundle();
                }
                else
                {
                    InitLuaPath();
                }
                lua.Start();
                StartMain();
                StartLooper();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 初始化Lua代码加载路径
        /// </summary>
        void InitLuaPath()
        {
            FindAndAddPaths(Config.GameResourcesPath + "/Lua");
        }

        void FindAndAddPaths(string path)
        {
            lua.AddSearchPath(path);
            string[] paths = Directory.GetDirectories(path);
            for (int i = 0; i < paths.Length; i++)
            {
                FindAndAddPaths(paths[i].Replace("\\", "/"));
            }
        }

        /// <summary>
        /// 初始化LuaBundle
        /// </summary>
        void InitLuaBundle()
        {
            Dictionary<string, string> files = ResManager.instance.localFiles;
            foreach (var item in files)
            {
                string bundleName = item.Key;
                if (bundleName.StartsWith("lua/") && bundleName.EndsWith(LuaConst.ExtName))
                {
                    string url = LuaConst.localABPath + "/" + bundleName.ToLower();
                    if (File.Exists(url))
                    {
                        var bytes = File.ReadAllBytes(url);
                        AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
                        if (bundle != null)
                        {
                            string name = bundleName.Replace("lua/", "").Replace(LuaConst.ExtName, "").ToLower();
                            LuaFileUtils.Instance.AddSearchBundle(name, bundle);
                        }
                    }
                }
            }
        }

        void StartMain()
        {
            lua.DoFile("Main.lua");

            LuaFunction main = lua.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;
        }

        void StartLooper()
        {
            loop = gameObject.AddComponent<LuaLooper>();
            loop.luaState = lua;
        }

        public void DoFile(string filename)
        {
            lua.DoFile(filename);
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        public LuaFunction GetFunction(string funcName)
        {
            return lua.GetFunction(funcName);
        }

        public void LuaGC()
        {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public void Close()
        {
            loop.Destroy();
            loop = null;

            lua.Dispose();
            lua = null;
        }
    }
}