using Code.Data;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.States.StatesInfrastructure;
using Code.Services.Progress;

namespace Code.Infrastructure.States.GameStates
{
  public class LoadProgressState : IState
  {
    private const string LevelSceneName = "Main";
    private readonly IGameStateMachine _stateMachine;
    private readonly IProgressService _progressService;

    public LoadProgressState(IGameStateMachine stateMachine, IProgressService progressService)
    {
      _stateMachine = stateMachine;
      _progressService = progressService;
    }

    public void Enter()
    {
      _progressService.Progress = new PlayerProgress();
      _stateMachine.Enter<LoadLevelState, string>(LevelSceneName);
    }

    public void Exit()
    {

    }
  }
}