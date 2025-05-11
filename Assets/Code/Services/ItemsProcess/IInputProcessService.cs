using UnityEngine;

namespace Code.Services.ItemsProcess
{
  public interface IInputProcessService
  {
    void Activate();
    void Deactivate();
    Camera MainCamera { get; set; }
  }
}