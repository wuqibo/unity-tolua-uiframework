using LuaFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainRootUI : MonoBehaviour
{
    public Text text;
    public Slider slider;
    private void Awake()
    {
        EventManager.Add(EventEnum.ABLoadingBegin, (BaseEventData eventData) =>
        {
            text.text = "正在更新资源";
            slider.value = 0;
        });
        EventManager.Add(EventEnum.ABLoadingError, (BaseEventData eventData) =>
        {
            text.text = eventData.args[0].ToString();
        });
        EventManager.Add(EventEnum.ABLoadingProgress, (BaseEventData eventData) =>
        {
            float progress = float.Parse(eventData.args[0].ToString());
            text.text = Mathf.FloorToInt(progress * 100) + "%";
            slider.value = progress;
        });
        EventManager.Add(EventEnum.ABLoadingFinish, (BaseEventData eventData) =>
        {
            text.text = "更新完成";
            slider.gameObject.SetActive(false);
        });

        Main.Instance.StartLuaFramework();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
