using System;
using UnityEngine;

namespace LuaFramework
{
    public delegate void TweenEvent(Transform target);
    public class Tween
    {
        public enum EaseType
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

        //开始移动物体
        public static void SetPosition(Transform trans, Vector3 toPos, bool worldSpace)
        {
            StartMove(0, trans, toPos, 0, EaseType.Linear, null, null, worldSpace, true, true, true);
        }
        public static void StartMove(float delay, Transform trans, Vector3 toPos, float time, EaseType method, bool worldSpace)
        {
            StartMove(delay, trans, toPos, time, method, null, null, worldSpace, true, true, true);
        }

        public static void StartMove(float delay, Transform trans, Vector3 toPos, float time, EaseType method, Action endEvent, bool worldSpace)
        {
            StartMove(delay, trans, toPos, time, method, endEvent, null, worldSpace, true, true, true);
        }

        public static void StartMove(float delay, Transform trans, Vector3 toPos, float time, EaseType method, TweenEvent endEvent, bool worldSpace)
        {
            StartMove(delay, trans, toPos, time, method, null, endEvent, worldSpace, true, true, true);
        }

        public static void StartMove(float delay, Transform trans, Vector3 toPos, float time, EaseType method, Action endEvent, TweenEvent endEventWithParam, bool worldSpace, bool xEnable, bool yEnable, bool zEnable)
        {
            MoveUpdate move = trans.GetComponent<MoveUpdate>();
            if (!move)
            {
                move = trans.gameObject.AddComponent<MoveUpdate>();
            }
            move.Move(delay, toPos, time, method, endEvent, endEventWithParam, worldSpace, xEnable, yEnable, zEnable);
        }

        public static void StopMove(Transform trans)
        {
            MoveUpdate move = trans.GetComponent<MoveUpdate>();
            if (move)
            {
                GameObject.DestroyImmediate(move);
            }
        }

        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta)
        {
            StartParabola(delay, trans, toPos, height, delta, false, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, Action OnEndEvent)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, null, OnEndEvent, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, OnCollisionEvent, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent, TweenEvent OnEndEvent)
        {
            StartParabola(delay, trans, toPos, height, delta, worldSpace, elasticity, null, OnCollisionEvent, null, OnEndEvent);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, float height, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, TweenEvent OnCollisionEventWithParam, Action OnEndEvent, TweenEvent OnEndEventWithParam)
        {
            ParabolaUpdate parabola = trans.GetComponent<ParabolaUpdate>();
            if (!parabola)
            {
                parabola = trans.gameObject.AddComponent<ParabolaUpdate>();
            }
            parabola.Go(delay, toPos, height, delta, worldSpace, elasticity, OnCollisionEvent, OnCollisionEventWithParam, OnEndEvent, OnEndEventWithParam);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, false, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, 0.3f, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, elasticity, null, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, elasticity, OnCollisionEvent, null, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, Action OnEndEvent)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, elasticity, OnCollisionEvent, null, OnEndEvent, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, elasticity, null, OnCollisionEvent, null, null);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity, TweenEvent OnCollisionEvent, TweenEvent OnEndEvent)
        {
            StartParabola(delay, trans, toPos, middlePos, delta, worldSpace, elasticity, null, OnCollisionEvent, null, OnEndEvent);
        }
        /// <summary>
        /// delta:0-1的过程中分成几步，不加Time.deltaTime
        /// </summary>
        public static void StartParabola(float delay, Transform trans, Vector3 toPos, Vector3 middlePos, float delta, bool worldSpace, float elasticity, Action OnCollisionEvent, TweenEvent OnCollisionEventWithParam, Action OnEndEvent, TweenEvent OnEndEventWithParam)
        {
            ParabolaUpdate parabola = trans.GetComponent<ParabolaUpdate>();
            if (!parabola)
            {
                parabola = trans.gameObject.AddComponent<ParabolaUpdate>();
            }
            parabola.Go(delay, toPos, middlePos, delta, worldSpace, elasticity, OnCollisionEvent, OnCollisionEventWithParam, OnEndEvent, OnEndEventWithParam);
        }
        public static void StopParabola(Transform trans)
        {
            ParabolaUpdate parabola = trans.GetComponent<ParabolaUpdate>();
            if (parabola)
            {
                GameObject.DestroyImmediate(parabola);
            }
        }

        //开始缩放到指定大小
        public static void SetScale(Transform trans, Vector3 toScale)
        {
            StartScale(0, trans, toScale, 0, EaseType.Linear, null, null);
        }
        public static void StartScale(float delay, Transform trans, Vector3 toScale, float time, EaseType method)
        {
            StartScale(delay, trans, toScale, time, method, null, null);
        }
        public static void StartScale(float delay, Transform trans, Vector3 toScale, float time, EaseType method, Action endEvent)
        {
            StartScale(delay, trans, toScale, time, method, endEvent, null);
        }
        public static void StartScale(float delay, Transform trans, Vector3 toScale, float time, EaseType method, TweenEvent endEvent)
        {
            StartScale(delay, trans, toScale, time, method, null, endEvent);
        }
        public static void StartScale(float delay, Transform trans, Vector3 toScale, float time, EaseType method, Action endEvent, TweenEvent endEventWithParam)
        {
            ScaleUpdate scale = trans.GetComponent<ScaleUpdate>();
            if (!scale)
            {
                scale = trans.gameObject.AddComponent<ScaleUpdate>();
            }
            scale.Scale(delay, toScale, time, method, endEvent, endEventWithParam);
        }

        public static void StopScale(Transform trans)
        {
            ScaleUpdate scale = trans.GetComponent<ScaleUpdate>();
            if (scale)
            {
                GameObject.DestroyImmediate(scale);
            }
        }

        //开始缩放UI到指定尺寸
        public static void SetUISize(Transform trans, Vector2 toSize)
        {
            StartUISize(0, trans, toSize, 0, EaseType.Linear, null, null);
        }
        public static void StartUISize(float delay, Transform trans, Vector2 toSize, float time, EaseType method)
        {
            StartUISize(delay, trans, toSize, time, method, null, null);
        }
        public static void StartUISize(float delay, Transform trans, Vector2 toSize, float time, EaseType method, Action endEvent)
        {
            StartUISize(delay, trans, toSize, time, method, endEvent, null);
        }
        public static void StartUISize(float delay, Transform trans, Vector2 toSize, float time, EaseType method, TweenEvent endEvent)
        {
            StartUISize(delay, trans, toSize, time, method, null, endEvent);
        }
        public static void StartUISize(float delay, Transform trans, Vector2 toSize, float time, EaseType method, Action endEvent, TweenEvent endEventWithParam)
        {
            UISizeUpdate size = trans.GetComponent<UISizeUpdate>();
            if (!size)
            {
                size = trans.gameObject.AddComponent<UISizeUpdate>();
            }
            size.UISize(delay, toSize, time, method, endEvent, endEventWithParam);
        }

        public static void StopUISize(Transform trans)
        {
            UISizeUpdate size = trans.GetComponent<UISizeUpdate>();
            if (size)
            {
                GameObject.DestroyImmediate(size);
            }
        }

        //开始渐变到指定透明度
        public static void SetAlpha(Transform trans, float toAlpha, bool excludeChildren = false)
        {
            StartAlpha(0, trans, toAlpha, 0, EaseType.Linear, null, null, excludeChildren);
        }
        /*
         * 暂时禁用，否则Lua创建映射冲突
        public static void StartAlpha(float delay, Transform trans, float toAlpha, float time, EaseType method, Action endEvent = null, bool excludeChildren = false)
        {
            StartAlpha(delay, trans, toAlpha, time, method, endEvent, null, excludeChildren);
        }
        public static void StartAlpha(float delay, Transform trans, float toAlpha, float time, EaseType method, TweenEvent endEventWithParam = null, bool excludeChildren = false)
        {
            StartAlpha(delay, trans, toAlpha, time, method, null, endEventWithParam, excludeChildren);
        }
        */
        public static void StartAlpha(float delay, Transform trans, float toAlpha, float time, EaseType method, Action endEvent = null, TweenEvent endEventWithParam = null, bool excludeChildren = false)
        {
            AlphaUpdate alpha = trans.GetComponent<AlphaUpdate>();
            if (!alpha)
            {
                alpha = trans.gameObject.AddComponent<AlphaUpdate>();
            }
            alpha.Alpha(delay, toAlpha, time, method, endEvent, endEventWithParam, excludeChildren);
        }
        public static void StopAlpha(Transform trans)
        {
            AlphaUpdate alpha = trans.GetComponent<AlphaUpdate>();
            if (alpha)
            {
                GameObject.DestroyImmediate(alpha);
            }
        }

        //开始抖动物体
        public static void StartShake(float delay, Transform trans, float offset, float time)
        {
            StartShake(delay, trans, offset, time, 1f, null, null);
        }
        public static void StartShake(float delay, Transform trans, float offset, float time, Action endEvent)
        {
            StartShake(delay, trans, offset, time, 1f, endEvent);
        }
        public static void StartShake(float delay, Transform trans, float offset, float time, float scale)
        {
            StartShake(delay, trans, offset, time, scale, null, null);
        }
        public static void StartShake(float delay, Transform trans, float offset, float time, float scale, Action endEvent)
        {
            StartShake(delay, trans, offset, time, scale, endEvent, null);
        }
        public static void StartShake(float delay, Transform trans, float offset, float time, float scale, TweenEvent endEvent)
        {
            StartShake(delay, trans, offset, time, scale, null, endEvent);
        }
        public static void StartShake(float delay, Transform trans, float offset, float time, float scale, Action endEvent, TweenEvent endEventWithParam)
        {
            ShakeUpdate shake = trans.GetComponent<ShakeUpdate>();
            if (!shake)
            {
                shake = trans.gameObject.AddComponent<ShakeUpdate>();
            }
            shake.Shake(delay, offset, time, scale, endEvent, endEventWithParam);
        }

        public static void StopShake(Transform trans)
        {
            ShakeUpdate shake = trans.GetComponent<ShakeUpdate>();
            if (shake)
            {
                GameObject.DestroyImmediate(shake);
            }
        }

        //开始摇摆物体
        public static void StartSwing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping)
        {
            StartSwing(delay, trans, offsetAngle, speed, time, useDamping, null, null);
        }

        public static void StartSwing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, Action endEvent)
        {
            StartSwing(delay, trans, offsetAngle, speed, time, useDamping, endEvent, null);
        }

        public static void StartSwing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, TweenEvent endEvent)
        {
            StartSwing(delay, trans, offsetAngle, speed, time, useDamping, null, endEvent);
        }

        public static void StartSwing(float delay, Transform trans, float offsetAngle, float speed, float time, bool useDamping, Action endEvent, TweenEvent endEventWithParam)
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
        public static ValueUpdate StartValue(float delay, float startValue, float toValue, float time, EaseType method, Action<float> updateEvent, Action finishEvent = null)
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
    public class LinearEase : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Linear(t, b, c, d);
        }
    }
    //Expo
    public class ExpoEaseIn : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseIn(t, b, c, d);
        }
    }

    public class ExpoEaseOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseOut(t, b, c, d);
        }
    }

    public class ExpoEaseInOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Expo.EaseInOut(t, b, c, d);
        }
    }
    //Sine
    public class SineEaseIn : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseIn(t, b, c, d);
        }
    }

    public class SineEaseOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseOut(t, b, c, d);
        }
    }

    public class SineEaseInOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Sine.EaseInOut(t, b, c, d);
        }
    }
    //Elastic
    public class ElasticEaseIn : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseIn(t, b, c, d);
        }
    }

    public class ElasticEaseOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseOut(t, b, c, d);
        }
    }

    public class ElasticEaseInOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Elastic.EaseInOut(t, b, c, d);
        }
    }
    //Back
    public class BackEaseIn : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseIn(t, b, c, d);
        }
    }

    public class BackEaseOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseOut(t, b, c, d);
        }
    }

    public class BackEaseInOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Back.EaseInOut(t, b, c, d);
        }
    }
    //Bounce
    public class BounceEaseIn : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseIn(t, b, c, d);
        }
    }

    public class BounceEaseOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseOut(t, b, c, d);
        }
    }

    public class BounceEaseInOut : Tween
    {
        public override double Ease(double t, double b, double c, double d)
        {
            return Bounce.EaseInOut(t, b, c, d);
        }
    }
}