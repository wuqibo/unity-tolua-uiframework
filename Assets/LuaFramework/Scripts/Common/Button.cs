using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LuaFramework
{
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public enum TriggerMethod
        {
            Up, Down, Double
        }
        public object param = 0;
        public TriggerMethod triggerMethod = TriggerMethod.Up;
        public float canTriggerInterval = 0f;
        public Action<GameObject, object> onClick, onTouchDown, onTouchClick, onTouchUp;
        public RectTransform rectTransform { get { return transform as RectTransform; } }
        float doubleTimer;
        float canTouchTimer;
        ButtonChange buttonChange;
        int currPointerId = -1;

        void Awake()
        {
            buttonChange = GetComponent<ButtonChange>();
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
            if (doubleTimer > 0f)
            {
                doubleTimer -= Time.deltaTime;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (enabled)
            {
                if (triggerMethod == TriggerMethod.Down)
                {
                    if (canTouchTimer <= 0f)
                    {
                        if (onClick != null) onClick.Invoke(gameObject, param);
                        canTouchTimer = canTriggerInterval;
                        if (buttonChange && canTouchTimer > 0)
                        {
                            buttonChange.enabled = false;
                        }
                    }
                }
                else if (triggerMethod == TriggerMethod.Double)
                {
                    if (doubleTimer <= 0f)
                    {
                        doubleTimer = 0.5f;
                    }
                    else
                    {
                        if (canTouchTimer <= 0f)
                        {
                            if (onClick != null) onClick.Invoke(gameObject, param);
                            doubleTimer = 0f;
                            canTouchTimer = canTriggerInterval;
                            if (buttonChange && canTouchTimer > 0)
                            {
                                buttonChange.enabled = false;
                            }
                        }
                    }
                }
                if (onTouchDown != null)
                {
                    onTouchDown.Invoke(gameObject, param);
                }
                currPointerId = eventData.pointerId;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            bool canExecUp = true;
            if (!Application.isEditor && Application.platform != RuntimePlatform.WindowsPlayer)
            {
                canExecUp = (currPointerId == eventData.pointerId);
            }
            if (canExecUp)
            {
                if (onTouchUp != null)
                {
                    onTouchUp.Invoke(gameObject, param);
                }
                currPointerId = -1;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enabled)
            {
                if (triggerMethod == TriggerMethod.Up)
                {
                    if (canTouchTimer <= 0f)
                    {
                        if (onClick != null) onClick.Invoke(gameObject, param);
                        canTouchTimer = canTriggerInterval;
                        if (buttonChange && canTouchTimer > 0)
                        {
                            buttonChange.enabled = false;
                        }
                    }
                }
                if (onTouchClick != null)
                {
                    onTouchClick.Invoke(gameObject, param);
                }
            }
        }
    }
}