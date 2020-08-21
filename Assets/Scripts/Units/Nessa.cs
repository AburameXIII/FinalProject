using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nessa : CharacterUnit
{
    public Character NessaCharacter;

    [Header("Skill Icons")]
    public Sprite BattleLitanyImage;
    public Sprite JumpImage;
    public Sprite HighJumpImage;
    public Sprite DragonDiveImage;

    protected override void Awake()
    {
       
        UnitName = "Nessa";
        int Level = PartyManager.Instance.GetTrainProgression(NessaCharacter).GetLevel();

        MaxHP = NessaCharacter.HP[Level-1];
        CurrentHP = NessaCharacter.HP[Level - 1];

        MaxSecondaryResource = 5;
        CurrentSecondaryResource = 0;
        SecondaryResource = SecondaryResourceType.COMBO;

        Speed = new Stat(NessaCharacter.Speed[Level - 1]);
        Defense = new Stat(NessaCharacter.Defense[Level - 1]);
        Attack = new Stat(NessaCharacter.Attack[Level - 1]);
        Luck = new Stat(NessaCharacter.Luck[Level - 1]);

        TurnSprite = NessaCharacter.CharacterTurnPicture;

        

        base.Awake();

        Skills.Add(new Jump(this, JumpImage));
        Skills.Add(new HighJump(this, HighJumpImage));
        Skills.Add(new BattleLitany(this, BattleLitanyImage));
        Skills.Add(new DragonDive(this, DragonDiveImage));

    }

   

}
