using Enums;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editors
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GraphicData))]
    public class GraphicDataEditor : Editor
    {
        private SerializedProperty _overlapGraphicsProp;
        private SerializedProperty _backgroundSpritesProp;
        private SerializedProperty _landscapeBackgroundSpritesProp;
        private SerializedProperty _citizenSpritesProp;
        private SerializedProperty _enemySpriteSetsProp;

        private void OnEnable()
        {
            _overlapGraphicsProp = serializedObject.FindProperty("overlapGraphics");
            _backgroundSpritesProp = serializedObject.FindProperty("backgroundSprites");
            _landscapeBackgroundSpritesProp = serializedObject.FindProperty("landscapeBackgroundSprites");
            _citizenSpritesProp = serializedObject.FindProperty("citizenSprites");
            _enemySpriteSetsProp = serializedObject.FindProperty("enemySpriteSets");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Стандартные поля
            EditorGUILayout.PropertyField(_backgroundSpritesProp, true);
            EditorGUILayout.PropertyField(_landscapeBackgroundSpritesProp, true);
            EditorGUILayout.PropertyField(_citizenSpritesProp, true);
            EditorGUILayout.PropertyField(_enemySpriteSetsProp, true);
        
            // Overlap Graphics
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Overlap Graphics", EditorStyles.boldLabel);
        
            // Отрисовка элементов
            for (var i = 0; i < _overlapGraphicsProp.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.BeginHorizontal();
        
                // Основное содержимое элемента
                EditorGUILayout.BeginVertical();
                var element = _overlapGraphicsProp.GetArrayElementAtIndex(i);
                
                EditorGUILayout.PropertyField(element.FindPropertyRelative("overlapType"));
                EditorGUILayout.PropertyField(element.FindPropertyRelative("sprite"));
                
                var landscapeLayerProp = element.FindPropertyRelative("landscapeLayer");
                EditorGUILayout.PropertyField(landscapeLayerProp);
                
                var landscapeLayerValue = (LandscapeLayerFlags)landscapeLayerProp.intValue;
                bool hasOverlapFlag = (landscapeLayerValue & LandscapeLayerFlags.Overlap) != 0;
        
                if (hasOverlapFlag)
                {
                    var animationDataProp = element.FindPropertyRelative("animationData");
                    EditorGUILayout.PropertyField(animationDataProp, true);
                }
                EditorGUILayout.EndVertical();
        
                // Кнопка удаления
                if (GUILayout.Button("Remove", GUILayout.Width(70)))
                {
                    _overlapGraphicsProp.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                    EditorGUILayout.EndVertical();
                    break; // Прерываем цикл после изменения массива
                }
        
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
            
            // Кнопки управления массивом
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Element"))
            {
                _overlapGraphicsProp.arraySize++;
                serializedObject.ApplyModifiedProperties();
            }
            if (GUILayout.Button("Clear All"))
            {
                _overlapGraphicsProp.arraySize = 0;
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
        
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
