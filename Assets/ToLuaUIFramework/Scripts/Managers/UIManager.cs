using System;
using System.Collections.Generic;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class UIManager : MonoBehaviour, ICommand
    {
        public static UIManager instance;

        public List<LuaBehaviour> uiStack = new List<LuaBehaviour>();
        List<LuaBehaviour> currVisibleUIList = new List<LuaBehaviour>();
        void Awake()
        {
            instance = this;
        }
        public bool ExeCommand(CommandEnum command)
        {
            return false;
        }

        /// <summary>
        /// 创建UI
        /// </summary>
        public void SpawnUI(string prefabPath, Transform parent, Action<GameObject, bool> callback, bool keepActive = false, bool isFloat = false, bool destroyABAfterSpawn = false, bool destroyABAfterAllSpawnDestroy = false)
        {
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("prefabPath为空");
                return;
            }
            //UI当单例使用，先从站内查找，有则直接显示,从栈顶往下找，更快找到
            for (int i = uiStack.Count - 1; i >= 0; i--)
            {
                LuaBehaviour luaBehaviour = uiStack[i];
                if (luaBehaviour.prefabPath.Equals(prefabPath))
                {
                    if (i < uiStack.Count - 1)
                    {
                        uiStack.RemoveAt(i);
                        uiStack.Add(luaBehaviour);
                        RefreshStack();
                    }
                    luaBehaviour.keepActive = keepActive;
                    luaBehaviour.isFloat = isFloat;
                    luaBehaviour.destroyABAfterAllSpawnDestroy = destroyABAfterAllSpawnDestroy;
                    if (callback != null) callback(luaBehaviour.gameObject, true);
                    return;
                }
            }
            //开始创建
            ResManager.instance.SpawnPrefab(prefabPath, parent, (GameObject go, bool isSingletonActiveCallback) =>
            {
                LuaBehaviour luaBehaviour = go.GetComponent<LuaBehaviour>();
                luaBehaviour.keepActive = keepActive;
                luaBehaviour.isFloat = isFloat;
                //处理入栈
                uiStack.Add(luaBehaviour);
                RefreshStack();
                if (callback != null) callback(go, isSingletonActiveCallback);
            }, destroyABAfterSpawn, destroyABAfterAllSpawnDestroy);
        }

        /// <summary>
        /// 不在栈顶的UI重新到栈顶显示
        /// </summary>
        public void ResumeUI(GameObject go)
        {
            for (int i = 0; i < uiStack.Count; i++)
            {
                LuaBehaviour behaviour = uiStack[i];
                if (behaviour.gameObject == go)
                {
                    uiStack.RemoveAt(i);
                    uiStack.Add(behaviour);
                    RefreshStack();
                    break;
                }
            }
        }

        /// <summary>
        /// 清空删除栈内所有UI
        /// </summary>
        public void ClearAllUI()
        {
            for (int i = 0; i < uiStack.Count; i++)
            {
                LuaBehaviour behaviour = uiStack[i];
                if (behaviour)
                {
                    Destroy(behaviour.gameObject);
                }
            }
            uiStack.Clear();
        }

        /// <summary>
        /// 刷新UI栈
        /// </summary>
        public void OnUIDestroy(LuaBehaviour behaviour)
        {
            if (uiStack.Contains(behaviour))
            {
                uiStack.Remove(behaviour);
            }
            RefreshStack();
        }

        #region 内部方法
        void RefreshStack()
        {
            currVisibleUIList.Clear();
            for (int i = 0; i < uiStack.Count; i++)
            {
                LuaBehaviour behaviour = uiStack[i];
                if (i == uiStack.Count - 1)
                {
                    if (!behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(true);
                }
                else
                {
                    if (AllOverLayerIsFloat(i))
                    {
                        if (!behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(behaviour.keepActive);
                    }
                }
                if (behaviour.gameObject.activeInHierarchy)
                {
                    currVisibleUIList.Add(behaviour);
                }
            }
            //临时改变层级
            for (int i = 0; i < currVisibleUIList.Count; i++)
            {
                if (i > 0)
                {
                    LuaBehaviour currBehaviour = currVisibleUIList[i];
                    LuaBehaviour underBehaviour = currVisibleUIList[i - 1];
                    if (currBehaviour.canvas && underBehaviour.canvas && currBehaviour.canvas.renderMode == underBehaviour.canvas.renderMode)
                    {
                        if (currBehaviour.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        {
                            currBehaviour.canvas.sortingOrder = underBehaviour.canvas.sortingOrder + 1;
                        }
                        else if (currBehaviour.canvas.renderMode == RenderMode.ScreenSpaceCamera)
                        {
                            currBehaviour.canvas.worldCamera.depth = underBehaviour.canvas.worldCamera.depth + 1f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当前层上面的层全是浮动层
        /// </summary>
        /// <returns></returns>
        bool AllOverLayerIsFloat(int currIndex)
        {
            for (int i = 0; i < uiStack.Count; i++)
            {
                LuaBehaviour behaviour = uiStack[i];
                if (i > currIndex)
                {
                    if (!behaviour.isFloat)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}