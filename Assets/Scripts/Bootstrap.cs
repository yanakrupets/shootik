using System;
using System.Collections.Generic;
using Controllers;
using DI;
using Interfaces;
using Managers;
using Models;
using ScriptableObjects;
using Test;
using UI;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Scriptable objects")]
    [SerializeField] private AimWeightData aimWeightData;
    [SerializeField] private ColliderData colliderData;
    [SerializeField] private GraphicData graphicData;
    
    [Header("MonoBehaviours")]
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private LevelGenerator levelGenerator;
    
    [Header("Views")]
    [SerializeField] private EnemyCounterView enemyCounterView;
    [SerializeField] private HeartView heartView;

    [Header("TEST")] 
    [SerializeField] private TestStart test;
    
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
    }

    private void BindMonoBehaviours()
    {
        DiContainer.Bind(poolManager);
        DiContainer.Bind(levelGenerator);
    }

    private void BindViews()
    {
        // bind exactly root canvas
        DiContainer.Bind(enemyCounterView);
        DiContainer.Bind(heartView);
    }

    private void BindModels()
    {
        var enemyCounterModel = DiFactory.Create<EnemyCounterModel>();
        DiContainer.Bind(enemyCounterModel);
        
        var heartModel = DiFactory.Create<HeartModel>();
        DiContainer.Bind(heartModel);
    }

    private void BindManagersAndControllers()
    {
        DiFactory.Create<AnimationController>();
        
        DiContainer.Bind(new EventManager());
        
        var gameManager = DiFactory.Create<GameManager>();
        DiContainer.Bind(gameManager);
        
        _initializables.Add(gameManager);
        _disposables.Add(gameManager);
    }

    private void InjectDependencies()
    {
        DiContainer.InjectDependencies(levelGenerator);
        DiContainer.InjectDependencies(test);
    }
}
