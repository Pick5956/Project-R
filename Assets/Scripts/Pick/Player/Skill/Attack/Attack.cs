using UnityEngine;

public class Attack : SkillBase
{
    [SerializeField] private Collider Hitbox;
    private float BaseSpeed;

    public override void Use()
    {
        BaseSpeed = animator.speed;
        animator.speed = PlayerController.PlayerSkillSpeed;
        animator.SetTrigger("Attack");
        skillManager.SetCurrentSkill(this);

        PlayerController.DisableControl();
    }

    public override void EnableHitBox()
    {
        Hitbox.enabled = true;
    }

    public override void DisableHitBox()
    {
        Hitbox.enabled = false;
        PlayerController.EnableControl();
        animator.speed = BaseSpeed;
    }
}
