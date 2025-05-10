namespace Code.Infrastructure.States.StatesInfrastructure
{
  public interface IState : IExitableState
  {
    void Enter();
  }
}