using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator,AnimatorStateInfo stateInfo,int layerIndex)
    {
        animator.transform.GetComponentInParent<Enemy>().OnAttackFinished();
    }
}
