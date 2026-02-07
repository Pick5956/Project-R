using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float cooldown;
    protected PlayerController PlayerController;
    protected Animator animator;
    protected SkillManager skillManager;

    public virtual void Init(PlayerController player)
    {
        PlayerController = player;
        animator = PlayerController.GetComponent<Animator>(); 
        skillManager = PlayerController.GetComponent<SkillManager>();
    }

    public abstract void Use();
    public abstract void EnableHitBox();
    public abstract void DisableHitBox();
}
