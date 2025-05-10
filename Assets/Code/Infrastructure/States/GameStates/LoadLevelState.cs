using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Async;
using Code.Services.StaticData;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
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

    private int _previousBlockType;

    public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IStaticDataService staticData, IAsyncService async)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _staticData = staticData;
      _async = async;
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
      var data = _staticData.GetLevel();
      InitGameField(data).Forget();
      _stateMachine.Enter<LevelLoopState>();
    }

    private async UniTaskVoid InitGameField(LevelStaticData data)
    {
      await FillContainer(data);
    }

    private async UniTask FillContainer(LevelStaticData data)
    {
      var figurinesList = _gameFactory.GenerateRandomFigurineList(data);
      foreach (var figurine in figurinesList)
      {
        _gameFactory.CreateFigurine(figurine.shape, figurine.icon, figurine.color, data.ShapeScale, data.IconScale);
        await _async.WaitForSeconds(data.NextFigurineDelay);
      }
    }
  }
}