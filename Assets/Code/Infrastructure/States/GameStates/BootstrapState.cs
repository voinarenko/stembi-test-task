using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.StaticData;

namespace Code.Infrastructure.States.GameStates
{
  public class BootstrapState : IState
  {
    private const string BootSceneName = "Boot";
    private const string LevelSceneName = "Main";
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
      LoadStaticData();
      _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
    }

    private void LoadStaticData() =>
      _staticData.LoadLevel();
  }
}