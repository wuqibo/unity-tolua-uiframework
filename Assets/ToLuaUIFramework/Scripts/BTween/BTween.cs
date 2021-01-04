using System;
using UnityEngine;

namespace ToLuaUIFramework
{
    public delegate void TweenEvent(Transform target);
    public class BTween
    {
        public enum BEaseType
        {
            Linear,
            ExpoIn,
            ExpoOut,
            ExpoInOut,
            SineIn,
            SineOut,
            SineInOut,
            ElasticIn,
            ElasticOut,
            ElasticInOut,
            BackIn,
            BackOut,
            BackInOut,
            BounceIn,
            BounceOut,
            BounceInOut
        }

        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta)
        {
            Parabola(delay, trans, toPos, height, delta, false, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, null, null, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, null, null, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, Action OnEndEvent)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, null, OnEndEvent, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, OnCollisionEvent, null, null);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent, TweenEvent OnEndEvent)
        {
            Parabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, OnCollisionEvent, null, OnEndEvent);
        }
        /// <summary>
        /// 不加Time.deltaTime
        /// </summary>
        public static void Parabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, TweenEvent OnCollisionEventWithParam, Action OnEndEvent, TweenEvent OnEndEventWithParam)
        {
            ParabolaUpdate parabola = trans.GetComponent<ParabolaUpdate>();
            if (!parabola)
            {
                parabola = trans.gameObject.AddComponent<ParabolaUpdate>();
            }
            parabola.Go(delay, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, OnCollisionEventWithParam, OnEndEvent, OnEndEventWithParam);
        }
        public static void StopParabola(Transform trans)
        {
            ParabolaUpdate parabola = trans.GetComponent<ParabolaUpdate>();
            if (parabola)
            {
                GameObject.DestroyImmediate(parabola);
            }
        }

        //开始摇摆物体
        public static void Swing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping)
        {
            Swing(delay, trans, offsetAngle, speed, time, useDamping, null, null);
        }

        public static void Swing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, Action endEvent)
        {
            Swing(delay, trans, offsetAngle, speed, time, useDamping, endEvent, null);
        }

        public static void Swing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, TweenEvent endEvent)
        {
            Swing(delay, trans, offsetAngle, speed, time, useDamping, null, endEvent);
        }

        public static void Swing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, Action endEvent, TweenEvent endEventWithParam)
        {
            SwingUpdate swing = trans.GetComponent<SwingUpdate>();
            if (!swing)
            {
                swing = trans.gameObject.AddComponent<SwingUpdate>();
            }
            swing.Swing(delay, offsetAngle, speed, time, useDamping, endEvent, endEventWithParam);
        }

        public static void StopSwing(Transform trans)
        {
            SwingUpdate swing = trans.GetComponent<SwingUpdate>();
            if (swing)
            {
                GameObject.DestroyImmediate(swing);
            }
        }

        //数值变化Tween
        public static ValueUpdate Value(float delay, float startValue, float toValue, float time, BEaseType method, Action<float> updateEvent, Action finishEvent = null)
        {
            GameObject go = new GameObject("ValueTween");
            ValueUpdate valueUpdate = go.GetComponent<ValueUpdate>();
            if (!valueUpdate)
            {
                valueUpdate = go.AddComponent<ValueUpdate>();
            }
            valueUpdate.Value(delay, startValue, toValue, time, method, updateEvent, finishEvent);
            return valueUpdate;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public double Linear(double t, double b, double c, double d)
        {
            return c * t / d + b;
        }

        class Quad
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return c * (t /= d) * t + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return -c * (t /= d) * (t - 2) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * t * t + b;
                }
                return -c / 2 * ((--t) * (t - 2) - 1) + b;
            }
        }

        public class Cubic
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return c * (t /= d) * t * t + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return c * ((t = t / d - 1) * t * t + 1) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * t * t * t + b;
                }
                return c / 2 * ((t -= 2) * t * t + 2) + b;
            }
        }

        public class Quart
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return c * (t /= d) * t * t * t + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return -c * ((t = t / d - 1) * t * t * t - 1) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * t * t * t * t + b;
                }
                return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
            }
        }

        public class Quint
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return c * (t /= d) * t * t * t * t + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * t * t * t * t * t + b;
                }
                return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
            }
        }

        public class Sine
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return -c * Math.Cos(t / d * (Math.PI / 2)) + c + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return c * Math.Sin(t / d * (Math.PI / 2)) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                return -c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b;
            }
        }

        public class Expo
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if (t == 0)
                {
                    return b;
                }
                if (t == d)
                {
                    return b + c;
                }
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
                }
                return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
            }
        }

        public class Circ
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if ((t /= d / 2) < 1)
                {
                    return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;
                }
                return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
            }
        }

        public class Elastic
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                if (t == 0)
                {
                    return b;
                }
                if ((t /= d) == 1)
                {
                    return b + c;
                }
                double p = d * .3;
                return -(c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - p / 4) * (2 * Math.PI) / p)) + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                if (t == 0)
                {
                    return b;
                }
                if ((t /= d) == 1)
                {
                    return b + c;
                }
                double p = d * .3;
                return (c * Math.Pow(2, -10 * t) * Math.Sin((t * d - p / 4) * (2 * Math.PI) / p) + c + b);
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if (t == 0)
                {
                    return b;
                }
                if ((t /= d / 2) == 2)
                {
                    return b + c;
                }
                double p = d * (.3 * 1.5);
                if (t < 1)
                {
                    return -.5 * (c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - p / 4) * (2 * Math.PI) / p)) + b;
                }
                return c * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - p / 4) * (2 * Math.PI) / p) * .5 + c + b;
            }
        }

        public class Back
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                double s = 1.2;
                return c * (t /= d) * t * ((s + 1) * t - s) + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                double s = 1.2;
                return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                double s = 1.2;
                if ((t /= d / 2) < 1)
                {
                    return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
                }
                return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
            }
        }

        public class Bounce
        {
            public static double EaseIn(double t, double b, double c, double d)
            {
                return c - EaseOut(d - t, 0, c, d) + b;
            }

            public static double EaseOut(double t, double b, double c, double d)
            {
                if ((t /= d) < (1 / 2.75))
                {
                    return c * (7.5625 * t * t) + b;
                }
                else if (t < (2 / 2.75))
                {
                    return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
                }
                else if (t < (2.5 / 2.75))
                {
                    return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
                }
                else
                {
                    return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
                }
            }

            public static double EaseInOut(double t, double b, double c, double d)
            {
                if (t < d / 2)
                {
                    return EaseIn(t * 2, 0, c, d) * .5 + b;
                }
                else
                {
                    return EaseOut(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
                }
            }
        }

        public virtual double Ease(double t, double b, double c, double d)
        {
            return 0;
        }
    }
    //Linear
    public class LinearEase : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Linear(t, b, c, d);
        }
    }
    //Expo
    public class ExpoEaseIn : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseIn(t, b, c, d);
        }
    }

    public class ExpoEaseOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseOut(t, b, c, d);
        }
    }

    public class ExpoEaseInOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseInOut(t, b, c, d);
        }
    }
    //Sine
    public class SineEaseIn : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseIn(t, b, c, d);
        }
    }

    public class SineEaseOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseOut(t, b, c, d);
        }
    }

    public class SineEaseInOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseInOut(t, b, c, d);
        }
    }
    //Elastic
    public class ElasticEaseIn : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseIn(t, b, c, d);
        }
    }

    public class ElasticEaseOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseOut(t, b, c, d);
        }
    }

    public class ElasticEaseInOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseInOut(t, b, c, d);
        }
    }
    //Back
    public class BackEaseIn : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseIn(t, b, c, d);
        }
    }

    public class BackEaseOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseOut(t, b, c, d);
        }
    }

    public class BackEaseInOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseInOut(t, b, c, d);
        }
    }
    //Bounce
    public class BounceEaseIn : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseIn(t, b, c, d);
        }
    }

    public class BounceEaseOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseOut(t, b, c, d);
        }
    }

    public class BounceEaseInOut : BTween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseInOut(t, b, c, d);
        }
    }
}