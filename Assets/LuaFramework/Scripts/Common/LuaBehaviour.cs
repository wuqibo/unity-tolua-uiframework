using System;
using UnityEngine;

namespace LuaFramework
{
    public class LuaBehaviour : MonoBehaviour
    {
        public string assetBundleName;
        public string prefabPath;
        public Canvas canvas { get; set; }
        public bool isFloat, destroyABAfterAllSpawnDestroy;
        Action onEnable, onStart, onDisable, onDestroy;

        /// <summary>
        /// 去掉尾部(Clone)
        /// </summary>
        string _name;

        public void SetEnableAction(Action onEnable)
        {
            this.onEnable = onEnable;
        }

        public void SetStartAction(Action onStart)
        {
            this.onStart = onStart;
        }

        public void SetDisableAction(Action onDisable)
        {
            this.onDisable = onDisable;
        }

        public void SetDestroyAction(Action onDestroy)
        {
            this.onDestroy = onDestroy;
        }

        protected virtual void Awake()
        {
            if (!canvas) canvas = GetComponent<Canvas>();
        }

        protected virtual void OnEnable()
        {
            if (onEnable != null) onEnable.Invoke();
        }

        protected virtual void Start()
        {
            if (onStart != null) onStart.Invoke();
        }

        protected virtual void OnDisable()
        {
            if (onDisable != null) onDisable.Invoke();
        }

        protected virtual void OnDestroy()
        {
            if (onDestroy != null) onDestroy.Invoke();
            UIManager.instance.OnUIDestroy(this);
            if (!string.IsNullOrEmpty(assetBundleName))
            {
                ResManager.instance.OnSpawnDestroy(assetBundleName, destroyABAfterAllSpawnDestroy);
            }
            ResManager.instance.ClearMemory();
        }
    }
}