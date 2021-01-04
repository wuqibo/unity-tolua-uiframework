using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToLuaUIFramework
{
    public class BButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
    {
        public object param = 0;
        public float canTriggerInterval = 0f;
        public Action<object> onClick, onPointerDown;
        public RectTransform rectTransform { get { return transform as RectTransform; } }
        float canTouchTimer;
        BButtonTrigger buttonChange;

        void Awake()
        {
            buttonChange = GetComponent<BButtonTrigger>();
        }

        void Start() { }

        void Update()
        {
            if (canTouchTimer > 0f)
            {
                canTouchTimer -= Time.deltaTime;
                if (canTouchTimer <= 0f)
                {
                    if (buttonChange)
                    {
                        buttonChange.enabled = true;
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (enabled)
            {
                if (canTouchTimer <= 0f)
                {
                    if (onPointerDown != null) onPointerDown.Invoke(param);
                    canTouchTimer = canTriggerInterval;
                    if (buttonChange && canTouchTimer > 0)
                    {
                        buttonChange.enabled = false;
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enabled)
            {
                if (canTouchTimer <= 0f)
                {
                    if (onClick != null) onClick.Invoke(param);
                    canTouchTimer = canTriggerInterval;
                    if (buttonChange && canTouchTimer > 0)
                    {
                        buttonChange.enabled = false;
                    }
                }
            }
        }
    }
}