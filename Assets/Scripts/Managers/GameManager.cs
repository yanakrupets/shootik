using System;
using System.Collections.Generic;
using DI;
using Enums;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;
using Controllers;
using Models;
using UnityEngine.InputSystem;
using static Controllers.AnimationController;

namespace Managers
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly Vector2 _timerRange = new Vector2(3f, 1f);
        private const float SpeedCoefficient = 0.95f;
        
        private readonly AimWeightData _aimWeightData;
        
        private readonly EventManager _eventManager;
        private readonly PoolManager _poolManager;
        private readonly GraphicController _graphicController;
        private readonly CanvasController _canvasController;
        private readonly SaveController _saveController;
        private readonly SoundController _soundController;
        private readonly LevelGenerator _levelGenerator;
        private readonly InputActionAsset _inputActionsAsset;
        private readonly GameStateMachine _gameStateMachine;
        
        private readonly HeartModel _heartModel;
        private readonly EnemyCounterModel _enemyCounterModel;
        private readonly ScoreModel _scoreModel;
        private readonly MenuCanvasModel _menuCanvasModel;
        private readonly ResultCanvasModel _resultCanvasModel;
        private readonly PauseMenuCanvasModel _pauseMenuCanvasModel;
        private readonly EducationCanvasModel _educationCanvasModel;

        private readonly Dictionary<ShootableItem, TargetPlace> _targetPlaces = new();
        private float _currentDelay;
        private bool _isPlaying;
        
        private InputAction _escapeAction;
        
        [Inject]
        public GameManager(
            AimWeightData aimWeightData, 
            EventManager eventManager, 
            PoolManager poolManager, 
            GraphicController graphicController,
            CanvasController canvasController, 
            SaveController saveController,
            SoundController soundController,
            LevelGenerator levelGenerator,
            InputActionAsset inputActionsAsset,
            GameStateMachine gameStateMachine,
            HeartModel heartModel, 
            EnemyCounterModel enemyCounterModel,
            ScoreModel scoreModel, 
            MenuCanvasModel menuCanvasModel,
            ResultCanvasModel resultCanvasModel,
            PauseMenuCanvasModel pauseMenuCanvasModel,
            EducationCanvasModel educationCanvasModel)
        {
            _aimWeightData = aimWeightData;
            
            _eventManager = eventManager;
            _poolManager = poolManager;
            _graphicController = graphicController;
            _canvasController = canvasController;
            _saveController = saveController;
            _soundController = soundController;
            _levelGenerator = levelGenerator;
            _inputActionsAsset = inputActionsAsset;
            _gameStateMachine = gameStateMachine;
            
            _heartModel = heartModel;
            _enemyCounterModel = enemyCounterModel;
            _scoreModel = scoreModel;
            _menuCanvasModel = menuCanvasModel;
            _resultCanvasModel = resultCanvasModel;
            _pauseMenuCanvasModel = pauseMenuCanvasModel;
            _educationCanvasModel = educationCanvasModel;
        }

        public void Initialize()
        {
            _eventManager.OnStart += StartGame;
            _eventManager.OnFinish += FinishGame;
            _eventManager.OnItemShot += ItemShooted;

            _scoreModel.OnSave += _saveController.Save;
            
            _escapeAction = _inputActionsAsset.FindAction("UI/Escape");
            if (_escapeAction == null)
            {
                Debug.LogError("Escape action not found in Input Actions Asset!");
                return;
            }
            
            _escapeAction.performed += HandleEscape;
            
            _menuCanvasModel.View.PlayButton.onClick.AddListener(PublishStartGame);
            _menuCanvasModel.View.EducationButton.onClick.AddListener(OpenEducation);
            _resultCanvasModel.View.MenuButton.onClick.AddListener(OpenMenu);
            _pauseMenuCanvasModel.View.ContinueButton.onClick.AddListener(ContinueGame);
            _pauseMenuCanvasModel.View.MenuButton.onClick.AddListener(FinishGame);
            _educationCanvasModel.View.MenuButton.onClick.AddListener(OpenMenu);
            
            _scoreModel.UpdateScore(_saveController.LoadInt(SaveConstants.BestScore));
            _canvasController.Open(CanvasType.Menu);
        }
        
        public void Dispose()
        {
            if (_eventManager != null)
            {
                _eventManager.OnStart -= StartGame;
                _eventManager.OnFinish -= FinishGame;
                _eventManager.OnItemShot -= ItemShooted;
            }

            if (_scoreModel != null)
            {
                _scoreModel.OnSave -= _saveController.Save;
            }
            
            if (_escapeAction != null)
            {
                _escapeAction.performed -= HandleEscape;
        
                if (_escapeAction.enabled)
                {
                    _escapeAction.Disable();
                }
            }
            
            _menuCanvasModel.View.PlayButton.onClick.RemoveListener(PublishStartGame);
            _menuCanvasModel.View.EducationButton.onClick.RemoveListener(OpenEducation);
            _resultCanvasModel.View.MenuButton.onClick.RemoveListener(OpenMenu);
            _pauseMenuCanvasModel.View.ContinueButton.onClick.RemoveListener(ContinueGame);
            _pauseMenuCanvasModel.View.MenuButton.onClick.RemoveListener(FinishGame);
            _educationCanvasModel.View.MenuButton.onClick.AddListener(OpenMenu);
        }
        
        private void StartGame()
        {
            _levelGenerator.Generate();
            _graphicController.SelectRandomEnemySet();
            
            _enemyCounterModel.ResetCounter();
            _heartModel.ResetHearts();
            
            _canvasController.Open(CanvasType.Gameplay);
            
            _currentDelay = _timerRange.x;
            _isPlaying = true;
            _ = AimCycleRoutine();
            
            _gameStateMachine.ChangeState(GameState.Playing);
            _escapeAction.Enable();
        }

        private void ContinueGame()
        {
            _soundController.Play(SoundName.UIButton);
            
            _isPlaying = true;
            _ = AimCycleRoutine();
            PauseGame(false);
            _canvasController.Open(CanvasType.Gameplay);
            
            _gameStateMachine.ChangeState(GameState.Playing);
            _escapeAction.Enable();
        }

        private void PauseGame(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }

        private void FinishGame()
        {
            _soundController.Play(SoundName.GameOver);
            
            PauseGame(false);
            _isPlaying = false;
            StopAllAnimations();
            _levelGenerator.Clean();
            
            _enemyCounterModel.SetResultScore();
            _canvasController.Open(CanvasType.Result);
            
            _gameStateMachine.ChangeState(GameState.None);
            _escapeAction.Disable();
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
            
            var sprite = _graphicController.GetRandomTargetSprite(targetType);
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
            PlayShotSound(item);
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
                            _scoreModel.UpdateScore(_enemyCounterModel.CurrentCount);
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

        private void PlayShotSound(ShootableItem item)
        {
            switch (item)
            {
                case EnemyItem enemyItem:
                    _soundController.Play(SoundName.Enemy);
                    break;
                case CitizenItem citizenItem:
                    _soundController.Play(SoundName.Citizen);
                    break;
                case HeartItem heartItem:
                    _soundController.Play(SoundName.Heart);
                    break;
                case LandscapeItem landscapeItem:
                    _soundController.Play(SoundName.Landscape);
                    break;
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
        
        private void OpenEducation()
        {
            _soundController.Play(SoundName.UIButton);
            _canvasController.Open(CanvasType.Education);
        }

        private void OpenMenu()
        {
            _soundController.Play(SoundName.UIButton);
            _canvasController.Open(CanvasType.Menu);
        }

        private void PublishStartGame()
        {
            _soundController.Play(SoundName.UIButton);
            _eventManager.PublishStartGame();
        }
        
        private void HandleEscape(InputAction.CallbackContext context)
        {
            _canvasController.Open(CanvasType.PauseMenu);
            _isPlaying = false;
            PauseGame(true);
                
            _gameStateMachine.ChangeState(GameState.Paused);
            _escapeAction.Disable();
        }
    }
}
