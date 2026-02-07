using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private PlayerController PlayerController;

    [SerializeField] private Transform skillRoot;
    private List<SkillBase> skills = new();
    private SkillBase currentSkill;

    public void SetCurrentSkill(SkillBase skill)
    {
        currentSkill = skill;
    }

    public void EnableHitbox()
    {
        currentSkill.EnableHitBox();
    }
    public void DisableHitbox()
    {
        currentSkill.DisableHitBox();
    }

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        foreach (Transform child in skillRoot)
        {
            SkillBase skill = child.GetComponent<SkillBase>();
            if (skill != null)
            {
                skill.Init(PlayerController);
                skills.Add(skill);
            }
        }
    }
    public void UseSkill(int index)
    {
        if (skills.Count == 0) { return; }
        skills[index].Use();
    }
}
