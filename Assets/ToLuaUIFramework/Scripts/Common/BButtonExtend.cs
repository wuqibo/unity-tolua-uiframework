using System;
using UnityEngine;

namespace ToLuaUIFramework
{
    public static class BButtonExtend
    {

        public static void OnClick(this Transform btn, Action<object> clickEvent)
        {
            btn.OnClick(0, clickEvent);
        }

        public static void OnClick(this Transform btn, object param, Action<object> clickEvent)
        {
            BButton bButton = btn.GetComponent<BButton>();
            if (!bButton)
            {
                bButton = btn.gameObject.AddComponent<BButton>();
            }
            bButton.param = param;
            bButton.onClick = clickEvent;
        }

        public static void OnPointerDown(this Transform btn, Action<object> pointerDownEvent)
        {
            btn.OnPointerDown(0, pointerDownEvent);
        }

        public static void OnPointerDown(this Transform btn, object param, Action<object> pointerDownEvent)
        {
            BButton bButton = btn.GetComponent<BButton>();
            if (!bButton)
            {
                bButton = btn.gameObject.AddComponent<BButton>();
            }
            bButton.param = param;
            bButton.onPointerDown = pointerDownEvent;
        }

    }
}