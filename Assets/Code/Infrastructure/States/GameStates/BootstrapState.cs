using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.StaticData;
using DG.Tweening;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : IState
  {
    private const string BootSceneName = "Boot";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;
    private readonly IStaticDataService _staticData;

    public BootstrapState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _staticData = staticData;
    }

    public void Enter() =>
      _sceneLoader.Load(BootSceneName, OnLoad).Forget();

    public void Exit()
    {
    }

    private void OnLoad()
    {
      // DOTween.SetTweensCapacity(500, 125);
      LoadStaticData();
      _stateMachine.Enter<LoadProgressState>();
    }

    private void LoadStaticData()
    {
    }
  }
}