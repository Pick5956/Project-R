using UnityEngine;

public struct DamageInfo
{
    public float damage;
    public Vector3 direction;
    public float knockbackforce;
}

public interface DamageAble
{
    void TakeDamage(DamageInfo info);
}