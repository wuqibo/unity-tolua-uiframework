using UnityEngine;

namespace LuaFramework
{
    public class Main : MonoBehaviour
    {
        private static Main _instance;
        public static Main Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = GameObject.FindObjectOfType<Main>();
                    if (!_instance)
                    {
                        GameObject go = new GameObject("Main");
                        DontDestroyOnLoad(go);
                        _instance = go.AddComponent<Main>();
                    }
                }
                return _instance;
            }
        }
        bool isStarted;

        public void StartLuaFramework()
        {
            if (isStarted) return;
            isStarted = true;

            CommandController.Instance.AddManager(typeof(ResManager));
            CommandController.Instance.AddManager(typeof(UIManager));
            CommandController.Instance.AddManager(typeof(LuaManager));
            CommandController.Instance.AddManager(typeof(SoundManager));

            CommandController.Instance.ExeCommand(CommandEnum.StartLoadAssetBundle);
        }

    }
}