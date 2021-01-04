using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
    /// <summary>
    /// DOTween扩展类
    /// </summary>
    public static class DOTweenExtend
    {
        /// <summary>
        /// 自动搜索当前和子节点一次性调节透明度
        /// </summary>
        public static TweenerCore<Color, Color, ColorOptions> DOAlpha(this Transform target, float endAlpha, bool includeChildren = true)
        {
            return target.DOAlpha(endAlpha, -1, 0, Ease.Linear, includeChildren);
        }

        /// <summary>
        /// 自动搜索当前和子节点一次性调节透明度
        /// </summary>
        public static TweenerCore<Color, Color, ColorOptions> DOAlpha(this Transform target, float endAlpha, float duration, Ease ease, bool includeChildren = true)
        {
            return target.DOAlpha(endAlpha, -1, duration, ease, includeChildren);
        }

        /// <summary>
        /// 自动搜索当前和子节点一次性调节透明度
        /// </summary>
        public static TweenerCore<Color, Color, ColorOptions> DOAlpha(this Transform target, float beginAlpha, float endAlpha, float duration, Ease ease, bool includeChildren = true)
        {
            Graphic[] graphics = null;
            if (includeChildren)
            {
                graphics = target.GetComponentsInChildren<Graphic>();
            }
            else
            {
                graphics = new Graphic[] { target.GetComponent<Graphic>() };
            }
            int count = graphics.Length;
            for (int i = 0; i < count; i++)
            {
                Graphic graphic = graphics[i];
                Color color = graphic.color;
                if (beginAlpha >= 0f)
                {
                    color.a = beginAlpha;
                    graphic.color = color;
                }
                color.a = endAlpha;
                if (duration > 0f)
                {
                    var t = graphic.DOColor(color, duration).SetEase(ease);
                    if (i == count - 1)
                    {
                        return t;
                    }
                }
                else
                {
                    color.a = endAlpha;
                    graphic.color = color;
                }
            }
            return null;
        }

    }
}