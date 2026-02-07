using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DamageAble>(out var target))
        {
            DamageInfo info = new DamageInfo{damage = 20f};
            target.TakeDamage(info);
        }
    }
}
