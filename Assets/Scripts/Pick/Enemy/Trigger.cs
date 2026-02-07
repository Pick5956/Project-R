using UnityEngine;

public class Trigger : MonoBehaviour 
{
    private TriggerInterface TriggerOwner;
    private EnterTriggerInterface EnterTriggerOwner;
    private StayTriggerInterface StayTriggerOwner;
    private ExitTriggerInterface ExitTriggerOwner;

    private void Awake()
    {
        TriggerOwner = GetComponentInParent<TriggerInterface>() ?? GetComponent<TriggerInterface>();
        EnterTriggerOwner = GetComponentInParent<EnterTriggerInterface>() ?? GetComponent<EnterTriggerInterface>();
        StayTriggerOwner = GetComponentInParent<StayTriggerInterface>() ?? GetComponent<StayTriggerInterface>();
        ExitTriggerOwner = GetComponentInParent<ExitTriggerInterface>() ?? GetComponent<ExitTriggerInterface>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerOwner?.OnTriggerEnter(other, this);
            EnterTriggerOwner?.OnTriggerEnter(other, this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerOwner?.OnTriggerStay(other,this);
            StayTriggerOwner?.OnTriggerStay(other,this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerOwner?.OnTriggerExit(other, this);
            ExitTriggerOwner?.OnTriggerExit(other, this);
        }
    }

}
