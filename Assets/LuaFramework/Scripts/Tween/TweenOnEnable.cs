using UnityEngine;

namespace LuaFramework
{
    /// <summary>
    /// 本工具包唯一使用拖动到GameObject上实现缓动的组建：TweenOnEnable。
    /// 其他使用方法：(代码直接执行)
    /// Tween.Move(...)
    /// Tween.Scale(...)
    /// Tween.Rotate(...)
    /// Tween.Alpha(...)
    /// 等等
    /// </summary>
    public class TweenOnEnable : MonoBehaviour
    {
        public float delay = 0.2f;
        public Transform target;

        public bool positionEnable;
        public Vector3 fromPos, toPos;
        public bool worldSpace;
        public Tween.EaseType posEaseType = Tween.EaseType.BackOut;

        public bool scaleEnable;
        public float fromScale, toScale = 1f;
        public Tween.EaseType scaleEaseType = Tween.EaseType.BackOut;

        public bool alphaEnable;
        public float fromAlpha, toAlpha = 1;
        public bool excludeChildren;
        public Tween.EaseType alphaEaseType = Tween.EaseType.ExpoOut;

        public float duration = 0.3f;

        Transform _target;
        //延迟在Update里执行，以确保获取到所有子物体一起操作
        bool isExecuted;

        void OnEnable()
        {
            isExecuted = false;
        }

        void Update()
        {
            if (!isExecuted)
            {
                _target = target ? target : transform;
                if (positionEnable)
                {
                    Tween.SetPosition(_target, fromPos, worldSpace);
                    Tween.StartMove(delay, _target, toPos, duration, posEaseType, worldSpace);
                }
                if (scaleEnable)
                {
                    Tween.SetScale(_target, Vector3.one * fromScale);
                    Tween.StartScale(delay, _target, Vector3.one * toScale, duration, scaleEaseType);
                }
                if (alphaEnable)
                {
                    Tween.SetAlpha(_target, fromAlpha, excludeChildren);
                    Tween.StartAlpha(delay, _target, toAlpha, duration, alphaEaseType, null, null, excludeChildren);
                }
                isExecuted = true;
            }
        }

    }
}