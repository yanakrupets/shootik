using System;
using System.Collections.Generic;
using Controllers;
using DI;
using Interfaces;
using Managers;
using Models;
using ScriptableObjects;
using UI;
using UI.Canvases;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [Header("Scriptable objects")]
    [SerializeField] private AimWeightData aimWeightData;
    [SerializeField] private ColliderData colliderData;
    [SerializeField] private GraphicData graphicData;
    
    [Space]
    [SerializeField] private InputActionAsset inputActionsAsset;
    
    [Header("MonoBehaviours")]
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private LevelGenerator levelGenerator;
    
    [Header("Views")]
    [SerializeField] private EnemyCounterView enemyCounterView;
    [SerializeField] private HeartView heartView;
    [SerializeField] private BestScoreView bestScoreView;
    [SerializeField] private ScoreResultView resultScoreView;
    
    [Header("Canvases")]
    [SerializeField] private Menu menuCanvas;
    [SerializeField] private Gameplay gameplayCanvas;
    [SerializeField] private PauseMenu pauseMenuCanvas;
    [SerializeField] private Education educationCanvas;
    [SerializeField] private Result resultCanvas;
    
    private readonly List<IInitializable> _initializables = new();
    private readonly List<IDisposable> _disposables = new();
    
    private void Awake()
    {
        BindScriptableObjects();
        BindMonoBehaviours();
        BindViews();
        BindModels();
        BindManagersAndControllers();
        
        InjectDependencies();
    }

    private void Start()
    {
        if (_initializables.Count <= 0)
            return;
        
        foreach (var initializable in _initializables)
        {
            initializable.Initialize();
        }
    }

    private void OnDestroy()
    {
        if (_disposables.Count <= 0)
            return;
        
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }

    private void BindScriptableObjects()
    {
        DiContainer.Bind(aimWeightData);
        DiContainer.Bind(colliderData);
        DiContainer.Bind(graphicData);
        
        DiContainer.Bind(inputActionsAsset);
    }

    private void BindMonoBehaviours()
    {
        DiContainer.Bind(poolManager);
        DiContainer.Bind(levelGenerator);
    }

    private void BindViews()
    {
        DiContainer.Bind(menuCanvas);
        DiContainer.Bind(gameplayCanvas);
        DiContainer.Bind(pauseMenuCanvas);
        DiContainer.Bind(educationCanvas);
        DiContainer.Bind(resultCanvas);
        
        DiContainer.Bind(enemyCounterView);
        DiContainer.Bind(heartView);
        DiContainer.Bind(bestScoreView);
        DiContainer.Bind(resultScoreView);
    }

    private void BindModels()
    {
        var enemyCounterModel = DiFactory.Create<EnemyCounterModel>();
        DiContainer.Bind(enemyCounterModel);
        
        var heartModel = DiFactory.Create<HeartModel>();
        DiContainer.Bind(heartModel);
        
        var scoreModel = DiFactory.Create<ScoreModel>();
        DiContainer.Bind(scoreModel);
        
        var menuCanvasModel = DiFactory.Create<MenuCanvasModel>();
        DiContainer.Bind(menuCanvasModel);
        
        var resultCanvasModel = DiFactory.Create<ResultCanvasModel>();
        DiContainer.Bind(resultCanvasModel);
        
        var pauseMenuCanvasModel = DiFactory.Create<PauseMenuCanvasModel>();
        DiContainer.Bind(pauseMenuCanvasModel);
        
        var educationCanvasModel = DiFactory.Create<EducationCanvasModel>();
        DiContainer.Bind(educationCanvasModel);
        
        _initializables.Add(scoreModel);
    }

    private void BindManagersAndControllers()
    {
        DiFactory.Create<AnimationController>();
        
        DiContainer.Bind(new EventManager());
        
        var canvasController = DiFactory.Create<CanvasController>();
        DiContainer.Bind(canvasController);
        
        var gameStateMachine = DiFactory.Create<GameStateMachine>();
        DiContainer.Bind(gameStateMachine);
        
        var gameManager = DiFactory.Create<GameManager>();
        DiContainer.Bind(gameManager);
        
        var cursorController = DiFactory.Create<CursorController>();
        
        _initializables.Add(cursorController);
        _initializables.Add(gameManager);
        
        _disposables.Add(gameManager);
    }

    private void InjectDependencies()
    {
        DiContainer.InjectDependencies(levelGenerator);
    }
}
