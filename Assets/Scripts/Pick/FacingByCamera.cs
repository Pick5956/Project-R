using UnityEngine;

public class FacingByCamera : MonoBehaviour
{
    [SerializeField] private bool SpriteFacingRight;
    [SerializeField] private float deadZone = 0.05f;


    public void UpdateFacing(Vector3 moveDirection)
    {
        moveDirection.y = 0;

        if (moveDirection.sqrMagnitude < 0.0001f)
            return;

        moveDirection.Normalize();

        float side = Vector3.Dot(moveDirection, Camera.main.transform.right);
        //Camera.main.transform.right ขวาของกล้องตอนนี้หันไปทางไหนในทิศแกนโลก

        bool isFacingRight =
            (transform.localScale.x > 0) == SpriteFacingRight;

        if (side > deadZone && !isFacingRight)
        {
            Flip();
        }
        else if (side < -deadZone && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
