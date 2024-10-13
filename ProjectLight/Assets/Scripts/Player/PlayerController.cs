using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private PlayerInput m_player_input;

    [SerializeField, ReadOnly] private Vector2 m_input_movement;

    [SerializeField] private float m_speed;

    public void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_player_input = new PlayerInput();
    }

    private void OnEnable()
    {
        m_player_input.Enable();

        m_player_input.GP.DropBomb.performed -= DropABomb;
        m_player_input.GP.DropBomb.performed += DropABomb;
        m_player_input.GP.DetonateBomb.performed -= DetonateAllBombs;
        m_player_input.GP.DetonateBomb.performed += DetonateAllBombs;
    }

    private void OnDisable()
    {
        m_player_input.Disable();

        m_player_input.GP.DropBomb.performed -= DropABomb;
        m_player_input.GP.DetonateBomb.performed -= DetonateAllBombs;
    }

    public void Update()
    {
        ProcessInput();
    }

    public void FixedUpdate()
    {
        PlayerMovement();
    }

    private void ProcessInput()
    {
        m_input_movement = m_player_input.GP.Move.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        if (m_input_movement.x != 0 || m_input_movement.y != 0)
        {
            float2 movement_input = math.normalize(new float2(m_input_movement.x, m_input_movement.y));
            m_rigidbody.MovePosition(m_rigidbody.position + (new Vector2(movement_input.x, movement_input.y)) * m_speed * Time.deltaTime);
        }
    }

    private void DropABomb(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BombManager.GetInstance().DropABomb(transform);
    }

    private void DetonateAllBombs(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BombManager.GetInstance().DetonateAllBombs();
    }
}
