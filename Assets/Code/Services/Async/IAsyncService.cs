using Cysharp.Threading.Tasks;
using System.Threading;

namespace Code.Services.Async
{
  public interface IAsyncService
  {
    UniTask NextFrame(CancellationTokenSource cts = null);
  }
}