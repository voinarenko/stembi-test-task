using Code.Data;
using Code.MonoBehaviours;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.ItemsProcess
{
  public class InputProcessService : IInputProcessService
  {
    public Camera MainCamera { get; set; }
    private readonly IInputService _input;

    public InputProcessService(IInputService input)
    {
      Application.quitting += OnApplicationQuitting;
      _input = input;
    }

    public void Activate()
    {
      _input.Actions().UI.Click.performed += OnPressed;
      _input.Actions().UI.Back.performed += Quit;
    }

    public void Deactivate()
    {
      _input.Actions().UI.Click.performed -= OnPressed;
      _input.Actions().UI.Back.performed -= Quit;
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
      var pointerPosition = MainCamera.ScreenToWorldPoint(_input.Actions().UI.Point.ReadValue<Vector2>());
      var hit = Physics2D.Raycast(pointerPosition, Vector2.zero);
      if (!hit.collider)
        return;

      var figurine = hit.transform.GetComponentInParent<Figurine>();
      if (!figurine)
        return;

      figurine.InvokeClick();
    }

    private static void Quit(InputAction.CallbackContext obj) =>
      Utils.Quit();

    private void OnApplicationQuitting()
    {
      Application.quitting -= OnApplicationQuitting;
      Deactivate();
    }
  }
}