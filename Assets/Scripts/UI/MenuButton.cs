using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{

    public InputAction invokeAction;
    [field: SerializeField] public UnityEvent Event { get; private set; } = new UnityEvent();

    private void Start()
    {
        invokeAction.performed += InvokeAction_performed;
        invokeAction.Enable();
    }

    public void InvokeButtonEvent()
    {
        Event?.Invoke();
    }

    private void InvokeAction_performed(InputAction.CallbackContext context)
    {
        InvokeButtonEvent();
    }

}
