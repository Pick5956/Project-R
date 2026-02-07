using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform target;   // Player
    public float rotateSpeed;
    [SerializeField] PlayerControls PlayerControls;



    private void Awake()
    {
        PlayerControls = new PlayerControls(); //อารมณ์แบบกดรันโค้ด ได้ของแล้วเพราะสั่งสร้างมาเก็บไว้
    }
    private void OnEnable()
    {
        PlayerControls.Player.Enable();
    }

    public float stepAngle = 90f;
    private float targetY;
    private float currentY;
    

    void LateUpdate()
    {
        if (PlayerControls.Player.RotateQ.WasPressedThisFrame())
        {
            targetY += stepAngle;
        }
        if (PlayerControls.Player.RotateE.WasPressedThisFrame())
        {
            targetY -= stepAngle;
        }

        currentY = Mathf.LerpAngle(
            currentY,
            targetY,
            rotateSpeed * Time.deltaTime
        );


        transform.position = target.position;
        transform.rotation = Quaternion.Euler(0, currentY, 0);
    }
}
