using UnityEditor;
using UnityEngine;

namespace LuaFramework
{
    [CustomEditor(typeof(TweenOnEnable))]
    public class TweenOnEnableEditor : Editor
    {
        TweenOnEnable tweenComponent;

        void OnEnable()
        {
            tweenComponent = (TweenOnEnable)target;
        }

        public override void OnInspectorGUI()
        {
            tweenComponent.delay = EditorGUILayout.FloatField("Delay", tweenComponent.delay);
            tweenComponent.target = (Transform)EditorGUILayout.ObjectField("Tarrget", tweenComponent.target, typeof(Transform), true);
            tweenComponent.duration = EditorGUILayout.FloatField("Duration", tweenComponent.duration);
            
            EditorGUILayout.Space();
            
            tweenComponent.positionEnable = EditorGUILayout.Toggle("Position", tweenComponent.positionEnable);
            if (tweenComponent.positionEnable)
            {
                tweenComponent.fromPos = EditorGUILayout.Vector3Field("     From", tweenComponent.fromPos);
                tweenComponent.toPos = EditorGUILayout.Vector3Field("     To", tweenComponent.toPos);
                tweenComponent.worldSpace = EditorGUILayout.Toggle("     WorldSpace", tweenComponent.worldSpace);
                tweenComponent.posEaseType = (Tween.EaseType)EditorGUILayout.EnumPopup("     EaseType", tweenComponent.posEaseType);
            }

            EditorGUILayout.Space();

            tweenComponent.scaleEnable = EditorGUILayout.Toggle("Scale", tweenComponent.scaleEnable);
            if (tweenComponent.scaleEnable)
            {
                tweenComponent.fromScale = EditorGUILayout.FloatField("     From", tweenComponent.fromScale);
                tweenComponent.toScale = EditorGUILayout.FloatField("     To", tweenComponent.toScale);
                tweenComponent.scaleEaseType = (Tween.EaseType)EditorGUILayout.EnumPopup("     EaseType", tweenComponent.scaleEaseType);
            }

            EditorGUILayout.Space();

            tweenComponent.alphaEnable = EditorGUILayout.Toggle("Alpha", tweenComponent.alphaEnable);
            if (tweenComponent.alphaEnable)
            {
                tweenComponent.fromAlpha = EditorGUILayout.FloatField("     From", tweenComponent.fromAlpha);
                tweenComponent.toAlpha = EditorGUILayout.FloatField("     To", tweenComponent.toAlpha);
                tweenComponent.excludeChildren = EditorGUILayout.Toggle("     ExcludeChildren", tweenComponent.excludeChildren);
                tweenComponent.alphaEaseType = (Tween.EaseType)EditorGUILayout.EnumPopup("     EaseType", tweenComponent.alphaEaseType);
            }

            EditorGUILayout.Space();

            

            //保存数据
            serializedObject.ApplyModifiedProperties();
        }
    }
}