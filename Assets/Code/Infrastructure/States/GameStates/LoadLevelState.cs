using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.MonoBehaviours;
using Code.Services.Async;
using Code.Services.InputProcess;
using Code.Services.ItemsAccount;
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
    private readonly IInputProcessService _inputProcess;
    private readonly IItemsAccountService _itemsAccount;
    private readonly IUIAnimationService _uiAnimation;

    private int _previousBlockType;

    public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IStaticDataService staticData, IAsyncService async, IInputProcessService inputProcess,
      IItemsAccountService itemsAccount, IUIAnimationService uiAnimation)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _async = async;
      _inputProcess = inputProcess;
      _itemsAccount = itemsAccount;
      _uiAnimation = uiAnimation;
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
      _inputProcess.Activate();
    }

    private async UniTask FillContainer(LevelStaticData data, Transform container)
    {
      var figurinesList = _gameFactory.GenerateRandomFigurineList(data);
      foreach (var figurine in figurinesList.Select(entry => _gameFactory.CreateFigurine(entry.Shape, entry.Icon,
                 entry.Color, data.ShapeScale, data.IconScale, container)))
      {
        _itemsAccount.AddFigurine(figurine);
        await _async.WaitForSeconds(data.NextFigurineDelay);
      }
    }
  }
}