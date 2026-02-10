using UnityEngine;

public class SkillPickup : MonoBehaviour
{
    public SkillData SkillData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            other.GetComponent<SkillManager>().AddSkill(SkillData);
            Destroy(gameObject);
        }
    }
}
