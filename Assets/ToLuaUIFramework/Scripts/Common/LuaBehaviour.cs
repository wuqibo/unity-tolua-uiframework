using LuaInterface;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class LuaBehaviour : MonoBehaviour
    {
        public string assetBundleName;
        public string prefabPath;
        public bool keepActive;
        public bool isFloat;
        public bool destroyABAfterAllSpawnDestroy;
        int uiID = -1;
        LuaTable luaClass;

        public Canvas canvas;
        public int sortingOrder;
        public float cameraDepth;

        /// <summary>
        /// Lua调用
        /// </summary>
        public void SetLuaClazz(LuaTable luaClass)
        {
            this.luaClass = luaClass;
        }

        /// <summary>
        /// Lua调用
        /// </summary>
        public void SetUIID(int uiID)
        {
            this.uiID = uiID;
        }

        protected virtual void Awake()
        {
            canvas = GetComponentInChildren<Canvas>();
            if (canvas)
            {
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    sortingOrder = canvas.sortingOrder;
                }
                else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                {
                    cameraDepth = canvas.worldCamera.depth;
                }
            }
        }

        protected virtual void OnEnable()
        {
            if (luaClass != null) luaClass.GetLuaFunction("onEnable").Call(luaClass);
        }

        protected virtual void Start()
        {
            luaClass.GetLuaFunction("onStart").Call(luaClass);
        }

        protected virtual void OnDisable()
        {
            if (luaClass != null) luaClass.GetLuaFunction("onDisable").Call(luaClass);
            if (canvas)
            {
                if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                {
                    canvas.sortingOrder = sortingOrder;
                }
                else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                {
                    canvas.worldCamera.depth = cameraDepth;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (luaClass != null) luaClass.GetLuaFunction("onDestroy").Call(luaClass);
            if (LuaManager.instance)
            {
                if (uiID >= 0)
                {
                    LuaManager.instance.GetFunction("onUIDestroy").Call(uiID);
                }
                LuaManager.instance.GetFunction("clear_class").Call(luaClass);
            }
            if (UIManager.instance)
            {
                UIManager.instance.OnUIDestroy(this);
            }
            if (ResManager.instance)
            {
                if (!string.IsNullOrEmpty(assetBundleName))
                {
                    ResManager.instance.OnSpawnDestroy(assetBundleName, destroyABAfterAllSpawnDestroy);
                }
                ResManager.instance.ClearMemory();
            }
        }
    }
}