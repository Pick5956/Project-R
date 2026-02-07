using UnityEngine;

public interface TriggerInterface
{
    void OnTriggerEnter(Collider other,Trigger trigger);
    void OnTriggerStay(Collider other,Trigger trigger);
    void OnTriggerExit(Collider other,Trigger trigger);
}

public interface EnterTriggerInterface
{
    void OnTriggerEnter(Collider other, Trigger trigger);
}

public interface StayTriggerInterface
{
    void OnTriggerStay(Collider other, Trigger trigger);
}
public interface ExitTriggerInterface
{
    void OnTriggerExit(Collider other, Trigger trigger);
}
