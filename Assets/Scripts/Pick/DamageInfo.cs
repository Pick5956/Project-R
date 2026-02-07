using UnityEngine;

public struct DamageInfo
{
    public float damage;

}

public interface DamageAble
{
    void TakeDamage(DamageInfo info);
}