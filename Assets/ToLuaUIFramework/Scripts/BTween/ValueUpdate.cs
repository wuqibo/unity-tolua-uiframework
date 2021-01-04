using System;
using UnityEngine;

namespace ToLuaUIFramework
{
    public class ValueUpdate : MonoBehaviour
    {
        public Action<float> UpdateEvent;
        public Action FinishEvent;
        BTween tween;
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
            if (tween == null)
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
                UpdateEvent((float)tween.Ease(t, b, c, d));
            }
            else
            {
                UpdateEvent(toValue);
            }
        }

        public void Value(float delay, float startValue, float toValue, float time, BTween.BEaseType method, Action<float> updateEvent, Action finishEvent)
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
                case BTween.BEaseType.Linear:
                    tween = new LinearEase();
                    break;
                case BTween.BEaseType.ExpoIn:
                    tween = new ExpoEaseIn();
                    break;
                case BTween.BEaseType.ExpoOut:
                    tween = new ExpoEaseOut();
                    break;
                case BTween.BEaseType.ExpoInOut:
                    tween = new ExpoEaseInOut();
                    break;
                case BTween.BEaseType.SineIn:
                    tween = new SineEaseIn();
                    break;
                case BTween.BEaseType.SineOut:
                    tween = new SineEaseOut();
                    break;
                case BTween.BEaseType.SineInOut:
                    tween = new SineEaseInOut();
                    break;
                case BTween.BEaseType.ElasticIn:
                    tween = new ElasticEaseIn();
                    break;
                case BTween.BEaseType.ElasticOut:
                    tween = new ElasticEaseOut();
                    break;
                case BTween.BEaseType.ElasticInOut:
                    tween = new ElasticEaseInOut();
                    break;
                case BTween.BEaseType.BackIn:
                    tween = new BackEaseIn();
                    break;
                case BTween.BEaseType.BackOut:
                    tween = new BackEaseOut();
                    break;
                case BTween.BEaseType.BackInOut:
                    tween = new BackEaseInOut();
                    break;
                case BTween.BEaseType.BounceIn:
                    tween = new BounceEaseIn();
                    break;
                case BTween.BEaseType.BounceOut:
                    tween = new BounceEaseOut();
                    break;
                case BTween.BEaseType.BounceInOut:
                    tween = new BounceEaseInOut();
                    break;
                default:
                    tween = new SineEaseOut();
                    break;
            }
        }

    }
}