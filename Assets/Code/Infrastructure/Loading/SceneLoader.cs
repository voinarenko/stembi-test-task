using Code.Services.Async;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  public class SceneLoader : ISceneLoader
  {
    private readonly IAsyncService _async;

    public SceneLoader(IAsyncService async) =>
      _async = async;

    public async UniTaskVoid Load(string nextSceneName, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextSceneName)
      {
        onLoaded?.Invoke();
        return;
      }
      var wait = SceneManager.LoadSceneAsync(nextSceneName);
      while (!wait!.isDone) await _async.NextFrame();
      onLoaded?.Invoke();
    }
  }
}