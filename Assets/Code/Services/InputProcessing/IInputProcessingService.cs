using UnityEngine;

namespace Code.Services.InputProcessing
{
  public interface IInputProcessingService
  {
    void Activate();
    void Deactivate();
    Camera MainCamera { get; set; }
  }
}