using System;
using System.Collections.Generic;
using DI;
using Enums;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;
using Models;
using static Controllers.AnimationController;

namespace Managers
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly Vector2 _timerRange = new Vector2(3f, 1f);
        private const float SpeedCoefficient = 0.95f;
        
        private readonly AimWeightData _aimWeightData;
        private readonly GraphicData _graphicData;
        
        private readonly EventManager _eventManager;
        private readonly PoolManager _poolManager;
        private readonly LevelGenerator _levelGenerator;
        private readonly HeartModel _heartModel;
        private readonly EnemyCounterModel _enemyCounterModel;

        private readonly Dictionary<ShootableItem, TargetPlace> _targetPlaces = new();
        private float _currentDelay;
        private bool _isPlaying;
        
        [Inject]
        public GameManager(
            AimWeightData aimWeightData, 
            GraphicData graphicData, 
            EventManager eventManager, 
            PoolManager poolManager, 
            LevelGenerator levelGenerator,
            HeartModel heartModel, 
            EnemyCounterModel enemyCounterModel)
        {
            _aimWeightData = aimWeightData;
            _graphicData = graphicData;
            
            _eventManager = eventManager;
            _poolManager = poolManager;
            _levelGenerator = levelGenerator;
            
            _heartModel = heartModel;
            _enemyCounterModel = enemyCounterModel;
        }

        public void Initialize()
        {
            _eventManager.OnStart += StartGame;
            _eventManager.OnFinish += FinishGame;
            _eventManager.OnItemShot += ItemShooted;
        }
        
        public void Dispose()
        {
            if (_eventManager != null)
            {
                _eventManager.OnStart += StartGame;
                _eventManager.OnFinish -= FinishGame;
                _eventManager.OnItemShot -= ItemShooted;
            }
        }
        
        private void StartGame()
        {
            _levelGenerator.Generate();
            
            _enemyCounterModel.ResetCounter();
            _heartModel.ResetHearts();
            
            _currentDelay = _timerRange.x;
            _isPlaying = true;
            _ = AimCycleRoutine();
        }

        private void FinishGame()
        {
            _isPlaying = false;
            StopAllAnimations();
            _levelGenerator.Clean();
        }
        
        private async Task AimCycleRoutine()
        {
            while (_isPlaying)
            {
                await Task.Delay((int)(_currentDelay * 1000));

                if (!_isPlaying)
                    return;

                ShowTarget();

                _currentDelay = Mathf.Max(_currentDelay * SpeedCoefficient, _timerRange.y);
            }
        }

        private void ShowTarget()
        {
            var targetZone = _levelGenerator.GetRandomFreeTargetZone();

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
        
        private void GenerateTarget<T>(TargetZone targetZone) 
            where T : ShootableItem
        {
            var targetPlace = targetZone.GetRandomFreeTargetPlace();
            
            if (targetPlace is null)
                return;
            
            var item = _poolManager.GetPool<T>().Get();
            _targetPlaces.Add(item, targetPlace);
            SetAnimation(item, targetZone, targetPlace);
        }

        private void GenerateTargetWithSprite<T>(TargetType targetType, TargetZone targetZone) 
            where T : ShootableItem, ISpriteRenderer
        {
            var targetPlace = targetZone.GetRandomFreeTargetPlace();

            if (targetPlace is null)
            {
                Debug.LogWarningFormat("There is no free target places in {0}", targetZone.OverlapType);
                return; 
            }
            
            var sprite = _graphicData.GetRandomTargetSprite(targetType);
            var item = _poolManager.GetPool<T>().Get();
            item.SetSprite(sprite);
            item.ChangeFlipX(targetPlace.AnimationData.EndPosition.x > targetPlace.AnimationData.StartPosition.x);
            _targetPlaces.Add(item, targetPlace);
            SetAnimation(item, targetZone, targetPlace);
        }

        private void SetAnimation<T>(T item, TargetZone targetZone, TargetPlace targetPlace) where T : ShootableItem
        {
            item.transform.SetParent(targetZone.transform);
            item.gameObject.SetActive(true);
            targetPlace.IsFree = false;
            Play(targetPlace.AnimationData, item.transform, () =>
            {
                targetPlace.IsFree = true;
                _targetPlaces.Remove(item);
                item.gameObject.SetActive(false);
                item.transform.localRotation = Quaternion.identity;
                _poolManager.GetPool<T>().Return(item);
            });
        }

        private void ItemShooted(ShootableItem item)
        {
            item.IsShot = true;
            
            // play sound
            if (item is IShotAnimated animatedItem)
            {
                Play(animatedItem.AnimationData, item.transform, () =>
                {
                    item.IsShot = false;
                    _targetPlaces[item].IsFree = true;
                    _targetPlaces.Remove(item);
                    item.gameObject.SetActive(false);
                    item.transform.localRotation = Quaternion.identity;
                
                    switch (item)
                    {
                        case EnemyItem enemyItem:
                            _poolManager.GetPool<EnemyItem>().Return(enemyItem);
                            _enemyCounterModel.RaiseCounter();
                            break;
                        case CitizenItem citizenItem:
                            _poolManager.GetPool<CitizenItem>().Return(citizenItem);
                            if (!_heartModel.TryRemoveHeart())
                            {
                                _eventManager.PublishFinishGame();
                            }
                            break;
                        case HeartItem heartItem:
                            _poolManager.GetPool<HeartItem>().Return(heartItem);
                            _heartModel.AddHeart();
                            break;
                    }
                });
            }
        }

        private TargetType GetRandomTargetType()
        {
            var randomValue = Random.Range(0, _aimWeightData.TotalWeight);

            foreach (var entry in _aimWeightData.weights)
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
