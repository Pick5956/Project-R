using UnityEngine;

public class SpriteBillboarding : MonoBehaviour
{

    void LateUpdate()
    {
        Vector3 euler = transform.eulerAngles;
        euler.y = Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = euler;

    }
}
