using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Input;
using Code.Services.Progress;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string SceneNamePrefix = "Level";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IInputService _inputService;
    private readonly IRandomService _randomService;

    private int _previousBlockType;

    public LoadLevelState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, ILoadingCurtain curtain,
      IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticDataService,
      IInputService inputService, IRandomService randomService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticDataService = staticDataService;
      _inputService = inputService;
      _randomService = randomService;
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
      _stateMachine.Enter<LevelLoopState>();
    }
  }
}