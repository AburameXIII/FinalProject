using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
    public string BossName;
    public string BossTitle;
    public FightType FightType;
    public List<Enemy> Enemies;
}

