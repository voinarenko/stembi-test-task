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

    public InputProcessService(IInputService input) =>
      _input = input;

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
  }
}