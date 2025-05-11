using UnityEngine;

namespace Code.MonoBehaviours
{
  public class CameraCorrector : MonoBehaviour
  {
    private const float MinCameraSize = 5;
    private const float MaxCameraSize = 6.15f;
    private const int BaseScreenWidth = 1080;
    private const int MinBaseScreenHeight = 1698;
    private const int MaxBaseScreenHeight = 3000;

    private const float MinAspectRatio = 1.572222f;
    
    [SerializeField] private Camera _camera;

    public void SetCameraSize()
    {
      var ratio = (float)Screen.height / Screen.width;
      if (ratio > MinAspectRatio)
        _camera.orthographicSize = MinCameraSize + (MaxCameraSize - MinCameraSize) /
          (MaxBaseScreenHeight - MinBaseScreenHeight) * (ratio * BaseScreenWidth - MinBaseScreenHeight);
      else
        _camera.orthographicSize = MinCameraSize;
    }
  }
}