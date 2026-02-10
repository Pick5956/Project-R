using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DamageAble>(out var target))
        {
            DamageInfo info = new DamageInfo
            {
                damage = 20f,
                direction = (other.transform.position - transform.position).normalized,
                knockbackforce = 5f
            };
            target.TakeDamage(info);
        }
    }
}
