using UnityEngine;

public class Player : MonoBehaviour
{
    // Reference to the Rigidbody component for physics-based movement
    private Rigidbody m_PlayerRB;

    // Reference to the input manager to read player movement input
    private GameInputManager m_GameInput;

    [Header("Player Settings")]
    // How fast the player moves
    public float speed = 5f;

    // Calculated velocity applied to the Rigidbody
    private Vector3 m_MovementVelocity;

    // Raw input direction from the input manager
    Vector3 input;

    // Start is called once before the first frame
    void Start()
    {
        // Cache the Rigidbody component attached to the player
        m_PlayerRB = GetComponent<Rigidbody>();

        // Find the GameInputManager in the scene
        m_GameInput = FindObjectOfType<GameInputManager>();
    }

    // FixedUpdate is called at a fixed interval — best for physics
    void FixedUpdate()
    {
        Movement();
        KillPlayer();
    }

    // Handles player movement based on input
    void Movement()
    {
        // Convert 2D input (x, y) into a 3D direction (x, 0, z)
        input = new Vector3(m_GameInput.movement.x, 0f, m_GameInput.movement.y);

        // Prevent faster diagonal movement by normalizing if needed
        if (input.sqrMagnitude > 1) input.Normalize();

        // Calculate the movement velocity
        m_MovementVelocity = input * speed;

        // Apply velocity while keeping the existing Y velocity (gravity)
        m_PlayerRB.linearVelocity = new Vector3(m_MovementVelocity.x, m_PlayerRB.linearVelocity.y, m_MovementVelocity.z);
    }

    // Ends the game if the player falls below a certain height
    void KillPlayer()
    {
        if(m_PlayerRB.transform.position.y < -5)
        {
            GameManager.instance.GameOver();
        }
    }
}