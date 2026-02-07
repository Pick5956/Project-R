using System.Collections;
using UnityEngine;

public class Enemy : CellData3D,DamageAble
{

    protected Animator m_Animator;
    [SerializeField] private Collider m_SearchBox;
    [SerializeField] private FacingByCamera FacingByCamera;
    protected Rigidbody Rigidbody;
    private Vector3 SearchArea;
    public int MoveSpeed;
    protected Vector3 TarGetMove;
    protected bool Is_Moving;
    protected bool Is_Attack;
    protected bool Is_Waiting;


    //Hp
    public float m_MaxHp;
    protected float m_CurrentHp;
    private float m_CurrentHpPercent;


    public override void Init(Vector3 coord)
    {
        base.Init(coord);
        m_CurrentHp = m_MaxHp;
    }


    protected void ChangeEnemyHp(float Hp)
    {
        m_CurrentHp += Hp;
        m_CurrentHp = Mathf.Clamp(m_CurrentHp, 0, m_MaxHp);
        m_CurrentHpPercent = (m_CurrentHp/m_MaxHp) * 100;
        if (m_CurrentHp == 0) 
        {
            Debug.Log("AA");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(DamageInfo info)
    {
        ChangeEnemyHp(info.damage);
    }


    Vector3 PlayerPos()
    {
        return GameManager.instance.PlayerController.transform.position;
    }



    public void MoveTo(Vector3 coord)
    {
        TarGetMove = coord;
        Is_Moving = true;
    }


    public void MoveAround()
    {
        float x = Random.Range(-SearchArea.x, SearchArea.x);
        float z = Random.Range(-SearchArea.z, SearchArea.z);
        x += m_BasePos.x;
        z += m_BasePos.z;
        MoveTo(new Vector3(x, 0, z));
    }

    protected void Follow()
    {
        MoveTo(PlayerPos());
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        SearchArea = m_SearchBox.bounds.extents;
        Is_Attack = false;
        MoveAround();
    }

    private void Update()
    {

        m_Animator.SetBool("Moving",Is_Moving);
        m_Pos = transform.position;
        if (!Is_Moving && !Is_Waiting && !Is_Attack)
        {
            MoveAround();
            
        }
    }

    private void FixedUpdate()
    {
        if (!Is_Moving)
            return;

        Vector3 newPos = Vector3.MoveTowards(Rigidbody.position,TarGetMove,MoveSpeed * Time.fixedDeltaTime);

        Rigidbody.MovePosition(newPos);

        if (Vector3.Distance(newPos, TarGetMove) < 0.01f)
        {
            Is_Moving = false;

            if (!Is_Waiting)  
                StartCoroutine(Delay(3f));
        }

        FacingByCamera.UpdateFacing(TarGetMove - m_Pos);
    }

    public virtual void Attack()
    {
        
    }
    public virtual void OnAttackFinished()
    {

    }

    public IEnumerator Delay(float Delay)
    {
       Is_Waiting = true;
       yield return new WaitForSeconds(Delay);
       Is_Waiting = false;
    }



}
