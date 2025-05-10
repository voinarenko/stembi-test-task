using Cysharp.Threading.Tasks;
using System.Threading;

namespace Code.Services.Async
{
  public class AsyncService : IAsyncService
  {
    public async UniTask NextFrame(CancellationTokenSource cts = null)
    {
      if (cts != null)
        await UniTask.NextFrame(cts.Token, true);
      else
        await UniTask.NextFrame();
    }
  }
}