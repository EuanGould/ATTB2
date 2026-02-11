using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // adjustables
    [SerializeField] private float held_threshold = 0.6f;

    // internal variables

    private bool button_held = false;
    private float button_held_duration = 0f;

    [SerializeField] private InputtableBehaviour inputtable;

    public void ProcessInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            // button released
            button_held = false;
            ProcessHeldDuration(button_held_duration);
            button_held_duration = 0f;
        }
        else
        {
            // button pressed down
            button_held = true;
        }
    }

    private void ProcessHeldDuration(float held_duration)
    {
        // called after the button is released
        if (held_duration > held_threshold)
        {
            inputtable.OnSingleButtonHeld();
        }
        else
        {
            inputtable.OnSingleButtonTapped();
        }
    }

    private void Update()
    {
        if (button_held)
        {
            button_held_duration += Time.deltaTime;
        }
    }
}
