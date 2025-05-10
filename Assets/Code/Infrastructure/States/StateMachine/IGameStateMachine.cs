using Code.Infrastructure.States.StatesInfrastructure;

namespace Code.Infrastructure.States.StateMachine
{
  public interface IGameStateMachine 
  {
    void Enter<TState>() where TState : class, IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
  }
}