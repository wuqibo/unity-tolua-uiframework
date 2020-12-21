using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace LuaFramework
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
        Dictionary<string, string> successFiles = new Dictionary<string, string>();
        Dictionary<string, AssetBundleInfo> loadedAssetBundles = new Dictionary<string, AssetBundleInfo>();

        void Awake()
        {
            instance = this;
        }
        public bool ExeCommand(CommandEnum command)
        {
            if (command == CommandEnum.StartLoadAssetBundle)
            {
                if (Config.UseAssetBundle)
                {
                    Debug.Log("开始下载资源");
                    StartCoroutine(CheckAndLoadAB());
                }
                else
                {
                    CommandController.Instance.ExeCommand(CommandEnum.StartLuaMain);
                }
                return true;
            }
            return false;
        }

        #region 加载AssetBundle

        /// <summary>
        /// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
        /// </summary>
        IEnumerator CheckAndLoadAB()
        {
            EventManager.Emit(EventEnum.ABLoadingBegin);
            EventManager.Emit(EventEnum.ABLoadingProgress, 0);
            //读取本地MD5文件
            localFiles = new Dictionary<string, string>();
            string localFilesPath = LuaConst.localABPath + "/" + LuaConst.MD5FileName;
            Debug.Log("localFilesPath: " + localFilesPath);
            if (File.Exists(localFilesPath))
            {
                localFiles = ParseKeyValue(File.ReadAllText(localFilesPath));
            }
            //下载远程MD5文件
            string filesUrl = Config.RemoteABUrl + "/" + LuaConst.MD5FileName;
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
                string url = Config.RemoteABUrl + "/" + item.Key;
                Debug.Log("下载资源：" + url);
                UnityWebRequest resRequest = UnityWebRequest.Get(url);
                yield return resRequest.SendWebRequest();
                if (resRequest.error != null)
                {
                    Debug.LogError(" [ " + url + " ] " + resRequest.error);
                    EventManager.Emit(EventEnum.ABLoadingError, resRequest.error);
                }
                string savePath = LuaConst.localABPath + "/" + item.Key;
                string saveDir = savePath.Substring(0, savePath.LastIndexOf("/"));
                if (!Directory.Exists(saveDir)) Directory.CreateDirectory(saveDir);
                File.WriteAllBytes(savePath, resRequest.downloadHandler.data);
                successFiles.Add(item.Key, item.Value);
                loadedCount++;
                float progress = loadedCount / (float)maxCount;
                Debug.Log("下载进度：" + progress);
                EventManager.Emit(EventEnum.ABLoadingProgress, progress);
            }
            UpdateLocalFiles();
            EventManager.Emit(EventEnum.ABLoadingProgress, 1);
            EventManager.Emit(EventEnum.ABLoadingFinish, "A", "B");
            yield return new WaitForEndOfFrame();

            EventManager.Emit(EventEnum.ResInited);
            CommandController.Instance.ExeCommand(CommandEnum.StartLuaMain);
        }

        #endregion

        #region 对外方法

        /// <summary>
        /// 创建UI(destroyAssetBundle:当所有引用完全销毁后是否同时销毁AssetBundle)
        /// </summary>
        public void CreatePrefab(string prefabPath, Transform parent, Action<LuaBehaviour> callback, bool destroyABAfterSpawn = false, bool destroyABAfterAllSpawnDestroy = false)
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
                    if (callback != null) callback(luaBehaviour);
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
                if (callback != null) callback(luaBehaviour);
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
            foreach (var item in successFiles)
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