namespace Code.Infrastructure.States.StatesInfrastructure
{
  public interface IPayloadedState<in TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
}