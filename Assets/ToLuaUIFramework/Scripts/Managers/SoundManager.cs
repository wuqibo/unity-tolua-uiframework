using UnityEngine;

namespace ToLuaUIFramework
{
    public class SoundManager : MonoBehaviour, ICommand
    {
        public static SoundManager instance;

        void Awake()
        {
            instance = this;
        }
        public bool ExeCommand(CommandEnum command)
        {
            if (command == CommandEnum.BGM_Enable)
            {
                Debug.Log("背景音乐开启");
                return true;
            }
            else if (command == CommandEnum.BGM_Disable)
            {
                Debug.Log("背景音乐关闭");
                return true;
            }
            return false;
        }
    }
}