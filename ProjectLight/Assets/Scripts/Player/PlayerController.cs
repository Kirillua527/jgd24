using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStatus
{
    default_status = 0,
    planting_a_bomb = 1,
    moving = 2,
    idle = 3,
}

public class PlayerController : MonoBehaviour
{
    // component
    private Rigidbody2D m_rigidbody;

    // input
    private PlayerInput m_player_input;
    private InputAction m_plant_a_bomb_action;
    private InputAction m_detonate_all_bombs_action;

    private InputAction m_pause_game_action;
    [SerializeField] private GameObject m_pause_panel = null;
    [SerializeField] private UILogic m_ui_logic;

    // movement
    [SerializeField, ReadOnly] private Vector2 m_input_movement;

    [SerializeField] private float m_speed;
    [SerializeField] private float m_speed_while_planting;

    // planting bomb
    private LineRenderer m_planting_progress_line;
    private const float m_planting_progress_line_length = 2.0f;
    private const float m_planting_progress_line_width = 0.3f;
    private const float m_planting_progress_line_height = 0.8f;

    private const double m_planting_duration = 0.4f;    // todo
    private double m_planting_start_time;

    // status
    [SerializeField, ReadOnly] private PlayerStatus m_status = PlayerStatus.default_status;
    public PlayerStatus getPlayerStatus() => m_status;

    public void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_player_input = new PlayerInput();
        m_plant_a_bomb_action = m_player_input.GP.PlantBomb;
        m_detonate_all_bombs_action = m_player_input.GP.DetonateBomb;
        m_pause_game_action = m_player_input.GP.PauseGame;
    }

    private void OnEnable()
    {
        m_player_input.Enable();

        m_plant_a_bomb_action.performed += PlantABombPerformed;
        m_plant_a_bomb_action.started += PlantABombStarted;
        m_plant_a_bomb_action.canceled += PlantABombCanceled;

        m_detonate_all_bombs_action.performed += DetonateAllBombs;

        m_pause_game_action.performed += PauseGame;
    }

    private void OnDisable()
    {
        m_plant_a_bomb_action.performed -= PlantABombPerformed;
        m_plant_a_bomb_action.started -= PlantABombStarted;
        m_plant_a_bomb_action.canceled -= PlantABombCanceled;

        m_detonate_all_bombs_action.performed -= DetonateAllBombs;

        m_pause_game_action.performed -= PauseGame;

        m_player_input.Disable();
    }

    private void Start()
    {
        m_planting_progress_line = gameObject.AddComponent<LineRenderer>();
        m_planting_progress_line.startWidth = m_planting_progress_line_width;
        m_planting_progress_line.endWidth = m_planting_progress_line_width;
        m_planting_progress_line.positionCount = 2;
        m_planting_progress_line.startColor = Color.yellow;
        m_planting_progress_line.endColor = Color.yellow;
        m_planting_progress_line.material = new Material(Shader.Find("Sprites/Default"));
        m_planting_progress_line.enabled = false;
    }

    public void Update()
    {
        ProcessInput();
        DrawPlantingProgressLine();
    }

    public void FixedUpdate()
    {
        PlayerMovement();
    }

    private void ProcessInput()
    {
        m_input_movement = m_player_input.GP.Move.ReadValue<Vector2>();
        m_input_movement = m_player_input.GP.Move.ReadValue<Vector2>();
    }

    private void PlayerMovement()
    {
        if (m_input_movement.x != 0 || m_input_movement.y != 0)
        if (m_input_movement.x != 0 || m_input_movement.y != 0)
        {
            float2 movement_input = math.normalize(new float2(m_input_movement.x, m_input_movement.y));
            float speed = m_status == PlayerStatus.planting_a_bomb ? m_speed_while_planting : m_speed;
            m_rigidbody.MovePosition(m_rigidbody.position + (new Vector2(movement_input.x, movement_input.y)) * speed * Time.deltaTime);

            if (m_status != PlayerStatus.planting_a_bomb)
            {
                m_status = PlayerStatus.moving;
            }
        }
        else if(m_status != PlayerStatus.planting_a_bomb)
        {
            m_status = PlayerStatus.idle;
        }
    }

    private void PlantABombPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BombManager.GetInstance().PlantABomb(transform);
        m_status = PlayerStatus.default_status;
    }

    private void PlantABombStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_status = PlayerStatus.planting_a_bomb;

        m_planting_start_time = Time.time;
    }

    private void PlantABombCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_status = PlayerStatus.default_status;

        m_planting_start_time = 0f;
    }

    private void DetonateAllBombs(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        BombManager.GetInstance().DetonateAllBombs();
    }

    private void DrawPlantingProgressLine()
    {
        if (m_status == PlayerStatus.planting_a_bomb)
        {
            m_planting_progress_line.enabled = true;

            double planting_time = Time.time - m_planting_start_time;
            float progress_line_length = Mathf.Lerp(0, m_planting_progress_line_length, (float)(planting_time / m_planting_duration));

            Vector3 playerHeadPosition = transform.position + new Vector3(-m_planting_progress_line_length / 2.0f, m_planting_progress_line_height, 0);
            m_planting_progress_line.SetPosition(0, playerHeadPosition);
            m_planting_progress_line.SetPosition(1, playerHeadPosition + Vector3.right * progress_line_length);
        }
        else
        {
            m_planting_progress_line.enabled = false;
        }
    }

    private void PauseGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        m_ui_logic.PauseGame();
        m_ui_logic.ShowPanel(true);
    }
}
