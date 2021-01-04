using ToLuaUIFramework;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Text text;
    public Slider slider;
    private void Awake()
    {
        MessageManager.Add(MsgEnum.ABLoadingBegin, (BaseMsg msg) =>
        {
            text.text = "正在更新资源";
            slider.value = 0;
        });
        MessageManager.Add(MsgEnum.ABLoadingError, (BaseMsg msg) =>
        {
            text.text = msg.args[0].ToString();
        });
        MessageManager.Add(MsgEnum.ABLoadingProgress, (BaseMsg msg) =>
        {
            float progress = float.Parse(msg.args[0].ToString());
            text.text = Mathf.FloorToInt(progress * 100) + "%";
            slider.value = progress;
        });
        MessageManager.Add(MsgEnum.ABLoadingFinish, (BaseMsg msg) =>
        {
            text.text = "更新完成";
            slider.gameObject.SetActive(false);
        });

        //启动框架
        Main.Instance.StartFramework();
    }
}
