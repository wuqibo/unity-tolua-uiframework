using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace ToLuaUIFramework
{
    public class AssetBundleInfo
    {
        public AssetBundle assetBundle;
        public int referencedCount;
        public AssetBundleInfo(AssetBundle assetBundle)
        {
            this.assetBundle = assetBundle;
            referencedCount = 0;
        }
    }
    public class ResManager : MonoBehaviour, ICommand
    {
        public static ResManager instance;

        public Dictionary<string, string> localFiles = new Dictionary<string, string>();
        Dictionary<string, string> loadSuccessFiles = new Dictionary<string, string>();
        Dictionary<string, AssetBundleInfo> loadedAssetBundles = new Dictionary<string, AssetBundleInfo>();
        List<string> preloadAssetBundleNames = new List<string>();
        int totalPreloadAssetBundles;

        void Awake()
        {
            instance = this;
        }
        public bool ExeCommand(CommandEnum command)
        {
            if (command == CommandEnum.UpdateRemoteAssetBundle)
            {
                if (Config.UseAssetBundle)
                {
                    Debug.Log("开始下载资源");
                    StartCoroutine(CheckAndDownloadAssetBundle());
                }
                else
                {
                    CommandController.Instance.Execute(CommandEnum.StartLuaMain);
                }
                return true;
            }
            return false;
        }

        #region 加载远程AssetBundle

        /// <summary>
        /// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
        /// </summary>
        IEnumerator CheckAndDownloadAssetBundle()
        {
            MessageManager.Dispatch(MsgEnum.ABLoadingBegin);
            MessageManager.Dispatch(MsgEnum.ABLoadingProgress, 0);
            //读取本地MD5文件
            localFiles = new Dictionary<string, string>();
            string localFilesPath = LuaConst.localABPath + "/" + LuaConst.MD5FileName;
            Debug.Log("localFilesPath: " + localFilesPath);
            if (File.Exists(localFilesPath))
            {
                localFiles = ParseKeyValue(File.ReadAllText(localFilesPath));
            }
            //下载远程MD5文件
            string filesUrl = Config.RemoteUrl + "/" + LuaConst.MD5FileName;
            UnityWebRequest request = UnityWebRequest.Get(filesUrl);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError("[ " + filesUrl + " ]" + request.error);
                yield break;
            }
            if (!Directory.Exists(LuaConst.localABPath)) Directory.CreateDirectory(LuaConst.localABPath);
            Dictionary<string, string> newestFiles = ParseKeyValue(request.downloadHandler.text);
            Dictionary<string, string> reloadFiles = new Dictionary<string, string>();
            foreach (var item in newestFiles)
            {
                bool localHaveKey = localFiles.ContainsKey(item.Key);
                if (!localHaveKey)
                {
                    //是新的文件，加入加载
                    reloadFiles.Add(item.Key, item.Value);
                }
                else
                {
                    bool fileExists = File.Exists(LuaConst.localABPath + "/" + item.Key);
                    if (!fileExists)
                    {
                        //本地找不到，加入下载
                        reloadFiles.Add(item.Key, item.Value);
                    }
                    else
                    {
                        string localHash = localFiles[item.Key];
                        string remoteHash = newestFiles[item.Key];
                        bool md5Match = localHash.Equals(remoteHash);
                        if (!md5Match)
                        {
                            //文件有改动，加入下载
                            reloadFiles.Add(item.Key, item.Value);
                        }
                    }
                }
            }
            int maxCount = reloadFiles.Count;
            Debug.Log("下载资源数量：" + maxCount);
            int loadedCount = 0;
            foreach (var item in reloadFiles)
            {
                string url = Config.RemoteUrl + "/" + item.Key;
                Debug.Log("下载资源：" + url);
                UnityWebRequest resRequest = UnityWebRequest.Get(url);
                yield return resRequest.SendWebRequest();
                if (resRequest.error != null)
                {
                    Debug.LogError(" [ " + url + " ] " + resRequest.error);
                    MessageManager.Dispatch(MsgEnum.ABLoadingError, resRequest.error);
                }
                string savePath = LuaConst.localABPath + "/" + item.Key;
                string saveDir = savePath.Substring(0, savePath.LastIndexOf("/"));
                if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
                File.WriteAllBytes(savePath, resRequest.downloadHandler.data);
                loadSuccessFiles.Add(item.Key, item.Value);
                loadedCount++;
                float progress = loadedCount / (float)maxCount;
                Debug.Log("下载进度：" + progress);
                MessageManager.Dispatch(MsgEnum.ABLoadingProgress, progress);
            }
            UpdateLocalFiles();
            MessageManager.Dispatch(MsgEnum.ABLoadingProgress, 1);
            MessageManager.Dispatch(MsgEnum.ABLoadingFinish, "A", "B");
            yield return new WaitForEndOfFrame();

            MessageManager.Dispatch(MsgEnum.ResInited);
            CommandController.Instance.Execute(CommandEnum.StartLuaMain);
        }

        #endregion

        #region 预加载本地AssetBundle

        /// <summary>
        /// Lua调用,预加载AssetBundle列表，传入目录路径
        /// </summary>
        public void PreloadLocalAssetBundles(string[] assetBundlePaths, Action<float> onProgress)
        {
            if (!Config.UseAssetBundle)
            {
                if (onProgress != null) onProgress(1);
                return;
            }
            preloadAssetBundleNames.Clear();
            for (int i = 0; i < assetBundlePaths.Length; i++)
            {
                string path = assetBundlePaths[i].Replace("\\", "/");
                if (path.Contains("Resources/Prefabs"))
                {
                    path = path.Substring(path.IndexOf("/Prefabs") + 1);
                }
                string namePrefix = null;
                if (path.Contains("/"))
                {
                    namePrefix = "res/" + path.Replace("/", "_").ToLower();
                }
                else
                {
                    namePrefix = "res/" + path.ToLower();
                }
                foreach (var item in localFiles.Keys)
                {
                    if (item.EndsWith(LuaConst.ExtName) && item.StartsWith(namePrefix))
                    {
                        string name = item.Substring(0, item.Length - LuaConst.ExtName.Length);
                        if (!preloadAssetBundleNames.Contains(name))
                        {
                            preloadAssetBundleNames.Add(name);
                        }
                    }
                }
            }
            totalPreloadAssetBundles = preloadAssetBundleNames.Count;
            StartCoroutine(PreloadAssetBundle(onProgress));
        }

        /// <summary>
        /// Lua调用,将预加载好的AssetBundle全部卸载，是否包括它的所有Spawn,由参数传入
        /// </summary>
        public void UnloadAllAssetBundles(bool unloadAllLoadedObjects)
        {
            foreach (var item in loadedAssetBundles.Values)
            {
                AssetBundle assetBundle = item.assetBundle;
                assetBundle.Unload(unloadAllLoadedObjects);
            }
            loadedAssetBundles.Clear();
            ClearMemory();
        }

        #endregion

        #region 对外方法

        /// <summary>
        /// 创建UI(destroyAssetBundle:当所有引用完全销毁后是否同时销毁AssetBundle)
        /// </summary>
        public void SpawnPrefab(string prefabPath, Transform parent, Action<GameObject, bool> callback, bool destroyABAfterSpawn = false, bool destroyABAfterAllSpawnDestroy = false)
        {
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("prefabPath为空");
                return;
            }
            if (Config.UseAssetBundle)
            {
                string assetBundleName = "res/" + prefabPath.ToLower();
                string prefabName = prefabPath;
                if (prefabPath.Contains("/"))
                {
                    assetBundleName = prefabPath.Substring(0, prefabPath.LastIndexOf("/"));
                    assetBundleName = "res/" + assetBundleName.Replace("/", "_").ToLower();
                    prefabName = prefabPath.Substring(prefabPath.LastIndexOf("/") + 1);
                }
                StartCoroutine(LoadAssetFromAssetBundle(assetBundleName, prefabName, (GameObject prefab) =>
                {
                    GameObject go = Instantiate(prefab);
                    if (parent) go.transform.SetParent(parent, false);
                    LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
                    luaBehaviour.assetBundleName = assetBundleName;
                    luaBehaviour.prefabPath = prefabPath;
                    luaBehaviour.destroyABAfterAllSpawnDestroy = destroyABAfterAllSpawnDestroy;
                    if (callback != null) callback(luaBehaviour.gameObject, false);
                }, destroyABAfterSpawn));
            }
            else
            {
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                GameObject go = Instantiate(prefab);
                if (parent) go.transform.SetParent(parent, false);
                LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
                luaBehaviour.assetBundleName = "";
                luaBehaviour.prefabPath = prefabPath;
                luaBehaviour.destroyABAfterAllSpawnDestroy = destroyABAfterAllSpawnDestroy;
                if (callback != null) callback(luaBehaviour.gameObject, false);
            }
        }

        /// <summary>
        /// 清除AssetBundle(自动判断引用数量为0时回收AssetBundle)
        /// </summary>
        public void OnSpawnDestroy(string bundleName, bool destroyABAfterAllSpawnDestroy)
        {
            if (loadedAssetBundles.ContainsKey(bundleName))
            {
                AssetBundleInfo assetBundleInfo = loadedAssetBundles[bundleName];
                assetBundleInfo.referencedCount--;
                if (assetBundleInfo.referencedCount <= 0 && destroyABAfterAllSpawnDestroy)
                {
                    assetBundleInfo.assetBundle.Unload(true);
                    loadedAssetBundles.Remove(bundleName);
                }
            }
        }

        /// <summary>
        /// 清理内存
        /// </summary>
        public void ClearMemory()
        {
            //Debug.LogError("主动GC");
            Resources.UnloadUnusedAssets();
            GC.Collect();
            LuaManager.instance.LuaGC();
        }

        #endregion

        #region 内部方法

        IEnumerator LoadAssetFromAssetBundle(string assetBundleName, string prefabName, Action<GameObject> callback, bool destroyABAfterSpawn = false)
        {
            AssetBundleInfo assetBundleInfo = null;
            loadedAssetBundles.TryGetValue(assetBundleName, out assetBundleInfo);
            if (assetBundleInfo == null)
            {
                Debug.Log("重新加载AssetBundle: " + assetBundleName);
                string localUrl = LuaConst.localABPath + "/" + assetBundleName + LuaConst.ExtName;
                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(localUrl);
                yield return request.SendWebRequest();
                if (request.error != null)
                {
                    Debug.LogError(" [ " + localUrl + " ] " + request.error);
                }
                AssetBundle assetBundle = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                if (destroyABAfterSpawn)
                {
                    GameObject prefab = assetBundle.LoadAsset<GameObject>(prefabName);
                    if (callback != null) callback(prefab);
                    assetBundle.Unload(false);
                }
                else
                {
                    assetBundleInfo = new AssetBundleInfo(assetBundle);
                    GameObject prefab = assetBundleInfo.assetBundle.LoadAsset<GameObject>(prefabName);
                    if (callback != null)
                    {
                        callback(prefab);
                        assetBundleInfo.referencedCount++;
                    }
                    loadedAssetBundles.Add(assetBundleName, assetBundleInfo);
                }
            }
            else
            {
                GameObject prefab = assetBundleInfo.assetBundle.LoadAsset<GameObject>(prefabName);
                if (callback != null)
                {
                    callback(prefab);
                    assetBundleInfo.referencedCount++;
                }
                if (destroyABAfterSpawn)
                {
                    assetBundleInfo.assetBundle.Unload(false);
                    loadedAssetBundles.Remove(assetBundleName);
                }
            }
        }

        IEnumerator PreloadAssetBundle(Action<float> onProgress)
        {
            while (preloadAssetBundleNames.Count > 0)
            {
                string assetBundleName = preloadAssetBundleNames[0];
                preloadAssetBundleNames.RemoveAt(0);
                AssetBundleInfo assetBundleInfo = null;
                loadedAssetBundles.TryGetValue(assetBundleName, out assetBundleInfo);
                if (assetBundleInfo == null)
                {
                    string localUrl = LuaConst.localABPath + "/" + assetBundleName + LuaConst.ExtName;
                    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(localUrl);
                    yield return request.SendWebRequest();
                    if (request.error != null)
                    {
                        Debug.LogError(" [ " + localUrl + " ] " + request.error);
                    }
                    AssetBundle assetBundle = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                    assetBundleInfo = new AssetBundleInfo(assetBundle);
                    loadedAssetBundles.Add(assetBundleName, assetBundleInfo);
                }
                if (onProgress != null) onProgress((totalPreloadAssetBundles - preloadAssetBundleNames.Count) / (float)totalPreloadAssetBundles);
            }
        }

        Dictionary<string, string> ParseKeyValue(string filesContent)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            try
            {
                string[] files = filesContent.Split('\n');
                for (int i = 0; i < files.Length; i++)
                {
                    if (string.IsNullOrEmpty(files[i])) continue;
                    string[] keyValue = files[i].Split('|');
                    string key = keyValue[0];
                    string value = keyValue[1].Replace("\n", "").Trim();
                    list.Add(key, value);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return list;
        }
        void UpdateLocalFiles()
        {
            //仅更新下载成功的文件
            foreach (var item in loadSuccessFiles)
            {
                localFiles[item.Key] = item.Value;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in localFiles)
            {
                stringBuilder.Append(item.Key + "|" + item.Value + "\n");
            }
            File.WriteAllText(LuaConst.localABPath + "/" + LuaConst.MD5FileName, stringBuilder.ToString());
        }
        #endregion
    }

}