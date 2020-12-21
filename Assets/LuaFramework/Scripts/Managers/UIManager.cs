using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaFramework
{
    public class UIManager : MonoBehaviour, ICommand
    {
        public static UIManager instance;

        public List<LuaBehaviour> uiStack = new List<LuaBehaviour>();
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
        public void CreateUI(string prefabPath, Action<GameObject> callback, bool isFloat = false, bool destroyABAfterSpawn = false, bool destroyABAfterAllSpawnDestroy = false)
        {
            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("prefabPath为空");
                return;
            }
            ResManager.instance.CreatePrefab(prefabPath, null, (LuaBehaviour luaBehaviour) =>
            {
                luaBehaviour.isFloat = isFloat;
                if (!luaBehaviour.canvas) luaBehaviour.canvas = luaBehaviour.GetComponent<Canvas>();
                if (!luaBehaviour.canvas)
                {
                    Debug.LogError(prefabPath + "上找不到Canvas组件，无法放入UI栈进行统一管理");
                }
                else
                {
                    uiStack.Add(luaBehaviour);
                    RefreshStack();
                }
                if (!luaBehaviour.gameObject.activeInHierarchy) luaBehaviour.gameObject.SetActive(true);
                if (callback != null) callback(luaBehaviour.gameObject);
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
            for (int i = 0; i < uiStack.Count; i++)
            {
                LuaBehaviour behaviour = uiStack[i];
                behaviour.canvas.sortingOrder = i;
                if (i == uiStack.Count - 1)
                {
                    if (!behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(true);
                }
                else
                {
                    if (uiStack[i + 1].isFloat)
                    {
                        if (!behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (behaviour.gameObject.activeInHierarchy) behaviour.gameObject.SetActive(false);
                    }
                }
            }
        }
        #endregion
    }
}