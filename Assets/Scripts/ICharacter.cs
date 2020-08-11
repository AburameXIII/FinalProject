using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    List<Skill> GetSkills();
    void PerformSkill(int SkillNumber, List<Unit> Targets);
}
