using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  public class SceneLoader : ISceneLoader
  {
    public async UniTaskVoid Load(string nextSceneName, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == nextSceneName)
      {
        onLoaded?.Invoke();
        return;
      }
      var wait = SceneManager.LoadSceneAsync(nextSceneName);
      while (!wait!.isDone) await UniTask.NextFrame();
      onLoaded?.Invoke();
    }
  }
}