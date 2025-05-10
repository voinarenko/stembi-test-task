using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Code.Services.Async
{
  public class AsyncService : IAsyncService
  {
    private readonly CancellationTokenSource _cts = new();

    public AsyncService() =>
      Application.quitting += OnApplicationQuitting;

    public async UniTask NextFrame(CancellationTokenSource cts = null) =>
      await UniTask.NextFrame(_cts.Token);

    public async UniTask WaitForSeconds(float duration, CancellationTokenSource cts = null) =>
      await UniTask.WaitForSeconds(duration, cancellationToken: _cts.Token);

    private void OnApplicationQuitting()
    {
      Application.quitting -= OnApplicationQuitting;
      _cts?.Cancel();
      _cts?.Dispose();
    }
  }
}