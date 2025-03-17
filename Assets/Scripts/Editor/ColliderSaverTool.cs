using System.Collections.Generic;
using System.Linq;
using Enums;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ColliderSaverTool : EditorWindow
    {
        private OverlapType _overlapType;
        private ColliderData _colliderData;
        private PolygonCollider2D _collider2D;
        private SpriteRenderer _overlapSpriteRenderer;

        [MenuItem("Tools/Collider Saver")]
        public static void ShowWindow()
        {
            GetWindow<ColliderSaverTool>("Collider Saver");
        }

        private void OnGUI()
        {
            GUILayout.Label("Save PolygonCollider2D Paths", EditorStyles.boldLabel);
        
            _colliderData = (ColliderData)EditorGUILayout
                .ObjectField("Collider Data", _colliderData, typeof(ColliderData), false);
        
            _overlapType = (OverlapType)EditorGUILayout.EnumPopup("Overlap Type", _overlapType);
        
            _collider2D = (PolygonCollider2D)EditorGUILayout
                .ObjectField("Polygon Collider 2D", _collider2D, typeof(PolygonCollider2D), true);
        
            _overlapSpriteRenderer = (SpriteRenderer)EditorGUILayout
                .ObjectField("Sprite Renderer", _overlapSpriteRenderer, typeof(SpriteRenderer), true);

            if (GUILayout.Button("Save Collider Paths"))
            {
                SaveColliderPaths();
            }
        
            if (GUILayout.Button("Update Collider"))
            {
                UpdateCollider();
            }
        }

        private void SaveColliderPaths()
        {
            if (_overlapType == OverlapType.None)
            {
                Debug.LogError("Overlap Type not set");
                return;
            }
        
            if (_colliderData is null)
            {
                Debug.LogError("Collider Data is not set!");
                return;
            }
        
            if (_collider2D is null)
            {
                Debug.LogError("PolygonCollider2D is not set!");
                return;
            }

            var pathPoints = new List<ColliderPathPoints>();
            for (var i = 0; i < _collider2D.pathCount; i++)
            {
                var points = new ColliderPathPoints(_collider2D.GetPath(i).ToArray());
                pathPoints.Add(points);
            }
            
            var path = new ColliderPath(_overlapType, _collider2D.bounds.size.x, pathPoints);

            var index = _colliderData
                .ColliderPaths
                .FindIndex(colliderPath => colliderPath.OverlapType == _overlapType);
        
            if (index != -1)
            {
                _colliderData.ColliderPaths[index] = path;
            }
            else
            {
                _colliderData.ColliderPaths.Add(path);
            }
        
            EditorUtility.SetDirty(_colliderData);
            AssetDatabase.SaveAssets();
        
            Debug.Log($"Collider paths for {_overlapType} saved!");
        }

        private void UpdateCollider()
        {
            // Очищаем текущие пути коллайдера
            _collider2D.pathCount = 0;

            // Получаем форму спрайта
            if (_overlapSpriteRenderer.sprite is not null)
            {
                // Получаем количество физических форм спрайта
                var shapeCount = _overlapSpriteRenderer.sprite.GetPhysicsShapeCount();

                // Проходим по всем формам
                for (var i = 0; i < shapeCount; i++)
                {
                    // Получаем точки формы
                    var points = new List<Vector2>();
                    _overlapSpriteRenderer.sprite.GetPhysicsShape(i, points);

                    // Устанавливаем путь для коллайдера
                    _collider2D.pathCount = i + 1;
                    _collider2D.SetPath(i, points.ToArray());
                }
            }
            else
            {
                Debug.LogWarning("SpriteRenderer не имеет спрайта!");
            }
        }
    }
}
