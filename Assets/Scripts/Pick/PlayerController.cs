using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed; //ทำให้เห็นในinspector แต่ห้ามclass อื่นยุ่ง;
    [SerializeField] GameObject CameraPivot;
    [SerializeField] private FacingByCamera FacingByCamera;

    private PlayerControls PlayerControls; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    private Rigidbody Rigidbody; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    private SkillManager SkillManager;
    [SerializeField]  private SpriteRenderer SpriteRenderer;
    [SerializeField]  private Animator m_Animator;
    public float PlayerSkillSpeed;
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
        SkillManager = GetComponent<SkillManager>();
    }

    public void EnableControl()
    {
        PlayerControls.Enable();
    }

    public void DisableControl()
    {
        PlayerControls.Disable();
    }

    public void Attack()
    {
        SkillManager.UseSkill(0);
    }


    void Update()
    {

        var forward = CameraPivot.transform.forward;
        var right = CameraPivot.transform.right;    

        Vector2 PlayerInput = PlayerControls.Player.Move.ReadValue<Vector2>();
        m_Animator.SetBool("Moving", movement.sqrMagnitude > 0.001f);

        forward.y = 0;
        forward.Normalize();
        right.y = 0;
        right.Normalize();

        movement = (right * PlayerInput.x + forward * PlayerInput.y).normalized;


        FacingByCamera.UpdateFacing(movement);


        if (PlayerControls.Player.Attack.WasPressedThisFrame())
        {
            Attack();
        }
    } 

    private void FixedUpdate()
    {
        Rigidbody.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
    }

}
