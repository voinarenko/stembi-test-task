using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.MonoBehaviours;
using Code.Services.Async;
using Code.Services.InputProcessing;
using Code.Services.ItemsAccounting;
using Code.Services.ItemsGeneration;
using Code.Services.StaticData;
using Code.Services.UIAnimation;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _staticData;
    private readonly IAsyncService _async;
    private readonly IInputProcessingService _inputProcessing;
    private readonly IItemsAccountingService _itemsAccounting;
    private readonly IUIAnimationService _uiAnimation;
    private readonly IItemGenerationService _itemGeneration;

    private int _previousBlockType;

    public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IStaticDataService staticData, IAsyncService async, IInputProcessingService inputProcessing,
      IItemsAccountingService itemsAccounting, IUIAnimationService uiAnimation, IItemGenerationService itemGeneration)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _async = async;
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
      _uiAnimation.Init();
      var jar = _gameFactory.CreateJar(data.JarPrefab);
      await FillContainer(data, jar.GetContainer());
      _uiAnimation.ShowUIElements();
      _inputProcessing.Activate();
    }

    private async UniTask FillContainer(LevelStaticData data, Transform container)
    {
      var figurinesList = _itemGeneration.GenerateRandomFigurineList(data);
      foreach (var figurine in figurinesList.Select(entry => _gameFactory.GetFigurine(entry.Shape, entry.Icon,
                 entry.Color, data.ShapeScale, data.IconScale, container)))
      {
        _itemsAccounting.AddFigurine(figurine);
        await _async.WaitForSeconds(data.NextFigurineDelay);
      }
    }
  }
}