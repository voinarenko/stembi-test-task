using Code.Data;
using Code.MonoBehaviours;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.InputProcessing
{
  public class InputProcessingService : IInputProcessingService
  {
    public Camera MainCamera { get; set; }
    private readonly IInputService _input;

    public InputProcessingService(IInputService input)
    {
      Application.quitting += OnApplicationQuitting;
      _input = input;
      _input.Actions().UI.Back.performed += Quit;
    }

    public void Activate() =>
      _input.Actions().UI.Click.performed += OnPressed;

    public void Deactivate() =>
      _input.Actions().UI.Click.performed -= OnPressed;

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
      _input.Actions().UI.Back.performed -= Quit;
      Deactivate();
    }
  }
}