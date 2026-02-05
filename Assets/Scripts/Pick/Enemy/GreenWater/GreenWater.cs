using UnityEngine;

public class GreenWater : Enemy, TriggerInterface
{
    [SerializeField] private Collider BiteHitbox;
    public float AttackSpeed;
    public Trigger FollowArea; //เพื่อเช็คว่า Trigger ที่เข้ามาเป็นของอันไหน
    public Trigger AttackArea;
    public Trigger Bite;



    void TriggerInterface.OnTriggerEnter(Collider other, Trigger trigger)
    {

        if (trigger == AttackArea) 
        {
            Attack();
        }
        if (trigger == Bite)
        {
            GameManager.instance.ChangePlayerHp(-20);
        }
    }
    void TriggerInterface.OnTriggerStay(Collider other, Trigger trigger)
    {
        if (trigger == AttackArea)
        {
            Attack();
        }
        if (trigger == FollowArea)
        {
            Follow();
        }
    }
    void TriggerInterface.OnTriggerExit(Collider other, Trigger trigger)
    {
        
    }




    public override void Attack()
    {
        if (!Is_Waiting) 
        { 
            base.Attack();
            Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            m_Animator.speed = AttackSpeed;
            m_Animator.SetBool("Bite", true);
            Is_Attack = true;
        }
    }

    void Bite_Attack()
    {
        BiteHitbox.enabled = true;
    }

    void Stop_Attack()
    {
        BiteHitbox.enabled = false;
    }

    public override void OnAttackFinished()
    {
        base.OnAttackFinished();
        Is_Attack = false;
        Rigidbody.constraints =  RigidbodyConstraints.FreezeRotation;
        m_Animator.SetBool("Bite", false);
        StartCoroutine(Delay(1f));
    }
    public void CoolDown()
    {

    }
   
}
