using System;
using System.Collections;
using Controllers;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private PoolManager poolManager;
        [SerializeField] private GraphicData graphicData;
        [SerializeField] private AimWeightData aimWeightData;

        [SerializeField] private Vector2 timerRange;
        [SerializeField, Range(0.8f, 0.99f)] private float speedCoefficient;

        private AnimationController _animationController;
        private float _currentDelay;

        private void Awake()
        {
            _currentDelay = timerRange.x;
            _animationController = new AnimationController();
        }

        private void Start()
        {
            levelGenerator.Generate();
            StartCoroutine(AimCycleRoutine());
        }

        private IEnumerator AimCycleRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentDelay);

                ShowTarget();

                _currentDelay = Mathf.Max(_currentDelay * speedCoefficient, timerRange.y);
            }
        }

        private void ShowTarget()
        {
            var targetZone = levelGenerator.GetRandomFreeTargetZone();

            switch (GetRandomTargetType())
            {
                case TargetType.Citizen:
                    GenerateTargetWithSprite<CitizenItem>(TargetType.Citizen, targetZone);
                    break;
                case TargetType.Enemy:
                    GenerateTargetWithSprite<EnemyItem>(TargetType.Enemy, targetZone);
                    break;
                case TargetType.Heart:
                    GenerateTarget<HeartItem>(targetZone);
                    break;
                case TargetType.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void GenerateTarget<T>(TargetZone targetZone) where T : ShootableItem
        {
            var targetPlace = targetZone.GetRandomFreeTargetPlace();
            
            if (targetPlace is null)
                return;
            
            var item = poolManager.GetPool<T>().Get();
            SetAnimation(item, targetZone, targetPlace);
        }

        private void GenerateTargetWithSprite<T>(TargetType targetType, TargetZone targetZone) where T : TargetItem
        {
            var targetPlace = targetZone.GetRandomFreeTargetPlace();
            
            if (targetPlace is null)
                return;
            
            var sprite = graphicData.GetRandomTargetSprite(targetType);
            var item = poolManager.GetPool<T>().Get();
            item.SetSprite(sprite);
            item.ChangeFlipX(targetPlace.AnimationData.EndPosition.x > targetPlace.AnimationData.StartPosition.x);
            SetAnimation(item, targetZone, targetPlace);
        }

        private void SetAnimation<T>(T item, TargetZone targetZone, TargetPlace targetPlace) where T : MonoBehaviour
        {
            item.transform.SetParent(targetZone.transform);
            item.gameObject.SetActive(true);
            targetPlace.IsFree = false;
            AnimationController.Play(targetPlace.AnimationData, item.transform, () =>
            {
                targetPlace.IsFree = true;
                item.gameObject.SetActive(false);
                item.transform.localRotation = Quaternion.identity;
                poolManager.GetPool<T>().Return(item);
            });
        }

        private TargetType GetRandomTargetType()
        {
            var randomValue = Random.Range(0, aimWeightData.TotalWeight);

            foreach (var entry in aimWeightData.weights)
            {
                if (randomValue < entry.Weight)
                {
                    return entry.Type;
                }
                randomValue -= entry.Weight;
            }

            return TargetType.None;
        }
    }
}
