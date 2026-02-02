using UnityEngine;

public class FoodObject : CellData3D
{
    public float HpEffect;

    public override void PlayerEntered()
    {
        base.PlayerEntered();
        GameManager.instance.ChangePlayerHp(HpEffect);
        Destroy(gameObject);
    }
}
