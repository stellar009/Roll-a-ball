using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    // Reference to the generated Input System actions
    private GameInputs m_GameInputs;

    // Public read-only property to get the current movement input
    public Vector2 movement { get; private set; }

    // Awake is called once before the first frame — sets up the input system
    void Awake()
    {
        m_GameInputs = new GameInputs();
    }

    // Called when the object is enabled — subscribes to input events
    private void OnEnable()
    {
        // Enable the Player and Interactions action maps
        m_GameInputs.Player.Enable();
        m_GameInputs.Interactions.Enable();

        // Subscribe to movement and cursor input events
        m_GameInputs.Player.Movement.performed += OnMovementPerformed;
        m_GameInputs.Player.Movement.canceled += OnMovementCanceled;

        m_GameInputs.Interactions.Cursor.performed += EnableCursor;
    }

    // Called when the object is disabled — unsubscribes and disables input
    private void OnDisable()
    {
        // Unsubscribe from all events to avoid memory leaks
        m_GameInputs.Player.Movement.performed -= OnMovementPerformed;
        m_GameInputs.Player.Movement.canceled -= OnMovementCanceled;

        m_GameInputs.Interactions.Cursor.performed -= EnableCursor;

        // Disable the action maps
        m_GameInputs.Player.Disable();
        m_GameInputs.Interactions.Disable();
    }

    // Called when movement input is detected — reads the Vector2 value
    void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }

    // Called when movement input stops — resets movement to zero
    void OnMovementCanceled(InputAction.CallbackContext ctx)
    {
        movement = Vector2.zero;
    }

    // Toggles the cursor on/off based on the input button state
    void EnableCursor(InputAction.CallbackContext ctx)
    {
        GameManager.instance.ActivateCursor(ctx.ReadValueAsButton());
    }

    // Public method to enable or disable player controls (useful during pauses/game over)
    public void EnablePlayerControls(bool enable)
    {
        if (enable)
        {
            m_GameInputs.Player.Enable();
        }
        else
        {
            m_GameInputs.Player.Disable();
        }
    }
}