namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        private readonly InputSystem_Actions _controls;

        public InputService()
        {
            _controls = new InputSystem_Actions();
            _controls.Enable();
        }
        
        public InputSystem_Actions Actions() => _controls;
    }
}