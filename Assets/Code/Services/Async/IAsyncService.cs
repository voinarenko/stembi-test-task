using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Code.Services.Async
{
  public interface IAsyncService
  {
    UniTask NextFrame(CancellationTokenSource cts = null);
    UniTask WaitForSeconds(float duration, CancellationTokenSource cts = null);
  }
}