// ReSharper disable once RedundantUsingDirective
using UnityEngine;

namespace Code.Data
{
  public static class Utils
  {
    public static void Quit()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      Application.Quit();
#elif UNITY_ANDROID
      var activity =
        new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
      activity.Call<bool>("moveTaskToBack", true);
#endif
    }
  }
}