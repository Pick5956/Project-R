using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private PlayerController PlayerController;

    [SerializeField] private Transform skillRoot; //เหมือนทำไว้ให้ลากโฟลเดอที่จะเก็บสกิลมาใส่ hierarchy
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
        //foreach (Transform child in skillRoot)
        //{
        //    SkillBase skill = child.GetComponent<SkillBase>();
        //    if (skill != null)
        //    {
        //        skill.Init(PlayerController);
        //        skills.Add(skill);
        //    }
        //}
    }

    public void AddSkill(SkillData skill)
    {
        SkillBase SkillObj = Instantiate(skill.SkillPrefab, skillRoot);
        SkillBase c_skill = SkillObj.GetComponent<SkillBase>();
        c_skill.Init(PlayerController);
        skills.Add(c_skill);
    }

    public void UseSkill(int index)
    {
        if (skills.Count == 0) { return; }
        skills[index].Use();
    }
}
