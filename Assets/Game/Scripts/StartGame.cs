using ToLuaUIFramework;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //启动框架
        Main.Instance.StartFramework();
    }
}
