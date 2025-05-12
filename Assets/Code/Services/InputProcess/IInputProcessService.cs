using UnityEngine;

namespace Code.Services.InputProcess
{
  public interface IInputProcessService
  {
    void Activate();
    void Deactivate();
    Camera MainCamera { get; set; }
  }
}