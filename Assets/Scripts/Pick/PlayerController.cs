using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int MoveSpeed; //ทำให้เห็นในinspector แต่ห้ามclass อื่นยุ่ง;
    [SerializeField] private float JumpHeight;
    [SerializeField] GameObject CameraPivot;
    [SerializeField] private FacingByCamera FacingByCamera;

    private PlayerControls PlayerControls; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    private Rigidbody Rigidbody; //ยังไม่ได้ของเพราะแค่ประกาศตัวแปร
    private SkillManager SkillManager;
    private Collider Collider;
    [SerializeField]  private SpriteRenderer SpriteRenderer;
    [SerializeField]  private Animator m_Animator;
    public float PlayerSkillSpeed;
    public LayerMask groundLayer;
    private Vector3 movement;
    private bool m_DisableControl;
    private RaycastHit Hit;
    private bool MidAirJump;


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
        Collider = GetComponent<Collider>();
    }

    public void EnableControl()
    {
        m_DisableControl = false;
    }

    public void DisableControl()
    {
        m_DisableControl = true;
    }

    public void Attack()
    {
        SkillManager.UseSkill(0);
    }


    void Update()
    {
        if (m_DisableControl) { return; }
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
        if (PlayerControls.Player.Jump.WasCompletedThisFrame()){
            Jump();
        }
    } 


    void Jump()
    {
        if (!isGround())
        {
            if (!MidAirJump)
            {
                return;
            }
            Rigidbody.linearVelocity = new Vector3(Rigidbody.linearVelocity.x, 0f, Rigidbody.linearVelocity.z);
            MidAirJump = false;
        }
        Rigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
    }

        
    private void FixedUpdate()
    {
        if (m_DisableControl) { return; }

        Vector3 moveDir = movement;
        if (Physics.Raycast(Collider.bounds.center,Vector3.down,out Hit, Collider.bounds.extents.y + 0.2f , groundLayer))
        {
            moveDir = Vector3.ProjectOnPlane(movement, Hit.normal);
        }
        
        Rigidbody.MovePosition(Rigidbody.position + moveDir * MoveSpeed * Time.fixedDeltaTime);
        
        if (Rigidbody.linearVelocity.y < 0)
        {
            Rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * (5f) * Time.fixedDeltaTime;
        }
        if (Rigidbody.linearVelocity.y > 0)
        {
            Rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * (5f) * Time.fixedDeltaTime;
        }


        //เซ็ตให้ตัวติดพื้นมั้ง
        if (isGround())
        {
            //Rigidbody.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
            MidAirJump = true;
        }
    }

    bool isGround()
    {
        Vector3 origin = Collider.bounds.center;
        float distance = Collider.bounds.extents.y + 0.1f;
        return Physics.Raycast(origin,Vector3.down,distance,groundLayer);
    }

}
