using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed; //ทำให้เห็นในinspector แต่ห้ามclass อื่นยุ่ง;


    private PlayerControls PlayerControls; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    private Rigidbody Rigidbody; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    [SerializeField]  private SpriteRenderer SpriteRenderer;
    [SerializeField]  private Animator m_Animator;
    private Vector3 movement;
    private bool IsMoving;

    private void Awake()
    {
        PlayerControls = new PlayerControls(); //อารมณ์แบบกดรันโค้ด ได้ของแล้วเพราะสั่งสร้างมาเก็บไว้
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>(); //ได้ของแล้วเพราะสั่งสร้างมาเก็บไว้
    }

    void Update()
    {
        Vector2 PlayerPos = PlayerControls.Player.Move.ReadValue<Vector2>();
        m_Animator.SetBool("Moving", movement.sqrMagnitude > 0.001f);

        movement = new Vector3(PlayerPos.x, 0, PlayerPos.y).normalized;
        if (movement.x > 0.1f)
        {
            SpriteRenderer.flipX = true;
        }
        else if (movement.x < -0.1f)
        {
            SpriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }
}
