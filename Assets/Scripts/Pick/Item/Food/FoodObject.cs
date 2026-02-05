using UnityEngine;

public class FoodObject : CellData3D,EnterTriggerInterface
{
    public float HpEffect;
    public Trigger Collect;

    void EnterTriggerInterface.OnTriggerEnter(Collider other, Trigger trigger)
    {
        if (trigger == Collect)
        {
            GameManager.instance.ChangePlayerHp(HpEffect);
            Destroy(gameObject);
        }
    }

}
