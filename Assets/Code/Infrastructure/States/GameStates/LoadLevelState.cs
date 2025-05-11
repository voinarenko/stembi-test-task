using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.MonoBehaviours;
using Code.Services.Async;
using Code.Services.ItemsProcess;
using Code.Services.StaticData;
using Code.StaticData;
using Cysharp.Threading.Tasks;
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

    private int _previousBlockType;

    public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IStaticDataService staticData, IAsyncService async, IInputProcessService inputProcess)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _async = async;
      _inputProcess = inputProcess;
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
      var jar = _gameFactory.CreateJar(data.JarPrefab);
      await FillContainer(data, jar.GetContainer());
      _inputProcess.Activate();
    }

    private async UniTask FillContainer(LevelStaticData data, Transform container)
    {
      var figurinesList = _gameFactory.GenerateRandomFigurineList(data);
      foreach (var figurine in figurinesList)
      {
        _gameFactory.CreateFigurine(figurine.shape, figurine.icon, figurine.color, data.ShapeScale, data.IconScale,
          container);
        await _async.WaitForSeconds(data.NextFigurineDelay);
      }
    }
  }
}