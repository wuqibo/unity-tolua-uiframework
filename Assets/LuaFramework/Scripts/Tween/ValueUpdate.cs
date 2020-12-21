using System;
using UnityEngine;

namespace LuaFramework
{
    public class ValueUpdate : MonoBehaviour
    {
        public Action<float> UpdateEvent;
        public Action FinishEvent;
        Tween Tween;
        float delay = 0, t = 0, d = 0, b = 0, c = 0;
        float toValue;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        void Update()
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
                return;
            }
            if (Tween == null)
            {
                return;
            }
            SetValue(t, d);
            if (t < d)
            {
                t += Time.deltaTime;
            }
            else
            {
                SetValue(0, 0, toValue);
                if (FinishEvent != null)
                {
                    FinishEvent();
                }
                Destroy(gameObject);
            }
        }

        void SetValue(float t, float d, float toValue = -1f)
        {
            if (toValue == -1f)
            {
                UpdateEvent((float)Tween.Ease(t, b, c, d));
            }
            else
            {
                UpdateEvent(toValue);
            }
        }

        public void Value(float delay, float startValue, float toValue, float time, Tween.EaseType method, Action<float> updateEvent, Action finishEvent)
        {
            this.delay = delay;
            this.toValue = toValue;
            t = 0f;
            b = startValue;
            c = this.toValue - b;
            d = time;
            this.UpdateEvent = updateEvent;
            this.FinishEvent = finishEvent;
            switch (method)
            {
                case Tween.EaseType.Linear:
                    Tween = new LinearEase();
                    break;
                case Tween.EaseType.ExpoIn:
                    Tween = new ExpoEaseIn();
                    break;
                case Tween.EaseType.ExpoOut:
                    Tween = new ExpoEaseOut();
                    break;
                case Tween.EaseType.ExpoInOut:
                    Tween = new ExpoEaseInOut();
                    break;
                case Tween.EaseType.SineIn:
                    Tween = new SineEaseIn();
                    break;
                case Tween.EaseType.SineOut:
                    Tween = new SineEaseOut();
                    break;
                case Tween.EaseType.SineInOut:
                    Tween = new SineEaseInOut();
                    break;
                case Tween.EaseType.ElasticIn:
                    Tween = new ElasticEaseIn();
                    break;
                case Tween.EaseType.ElasticOut:
                    Tween = new ElasticEaseOut();
                    break;
                case Tween.EaseType.ElasticInOut:
                    Tween = new ElasticEaseInOut();
                    break;
                case Tween.EaseType.BackIn:
                    Tween = new BackEaseIn();
                    break;
                case Tween.EaseType.BackOut:
                    Tween = new BackEaseOut();
                    break;
                case Tween.EaseType.BackInOut:
                    Tween = new BackEaseInOut();
                    break;
                case Tween.EaseType.BounceIn:
                    Tween = new BounceEaseIn();
                    break;
                case Tween.EaseType.BounceOut:
                    Tween = new BounceEaseOut();
                    break;
                case Tween.EaseType.BounceInOut:
                    Tween = new BounceEaseInOut();
                    break;
                default:
                    Tween = new SineEaseOut();
                    break;
            }
        }

    }
}