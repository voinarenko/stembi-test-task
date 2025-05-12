using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.MonoBehaviours;
using Code.Services.InputProcessing;
using Code.Services.ItemsAccounting;
using Code.Services.ItemsGeneration;
using Code.Services.StaticData;
using Code.Services.UIAnimation;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoadState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _staticData;
    private readonly IInputProcessingService _inputProcessing;
    private readonly IItemsAccountingService _itemsAccounting;
    private readonly IUIAnimationService _uiAnimation;
    private readonly IItemGenerationService _itemGeneration;

    private int _previousBlockType;

    public LevelLoadState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IStaticDataService staticData, IInputProcessingService inputProcessing,
      IItemsAccountingService itemsAccounting, IUIAnimationService uiAnimation, IItemGenerationService itemGeneration)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _inputProcessing = inputProcessing;
      _itemsAccounting = itemsAccounting;
      _uiAnimation = uiAnimation;
      _itemGeneration = itemGeneration;
    }

    public void Enter(string payload)
    {
      _curtain.Show();
      _sceneLoader.Load(payload, OnLoaded).Forget();
    }

    public void Exit() =>
      _curtain.Hide();

    private void OnLoaded()
    {
      SetCamera();
      var data = _staticData.GetLevel();
      InitGameField(data).Forget();
      _stateMachine.Enter<LevelLoopState>();
    }

    private void SetCamera()
    {
      _gameFactory.MainCamera.TryGetComponent<CameraCorrector>(out var corrector);
      corrector?.SetCameraSize();
    }

    private async UniTaskVoid InitGameField(LevelStaticData data)
    {
      _gameFactory.Init(data);
      _uiAnimation.Init();
      _itemsAccounting.Init();
      var container = _gameFactory.CreateJar(data.JarPrefab).GetContainer();
      
      _itemsAccounting.Container = container;
      await FillContainerAsync(data, container);
      _uiAnimation.ShowUIElements();
      _inputProcessing.Activate();
    }

    private async UniTask FillContainerAsync(LevelStaticData data, Transform container)
    {
      var figurinesKeys = _itemGeneration.GenerateRandomFigurineKeys(data);
      await _itemsAccounting.DropFigurinesAsync(data, container, figurinesKeys);
    }
  }
}