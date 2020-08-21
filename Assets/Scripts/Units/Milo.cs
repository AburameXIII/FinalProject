using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo : CharacterUnit
{
    public Character MiloCharacter;

    
    public Sprite TapPunchImage;
    public Sprite MeditateImage;
    public Sprite SuperPunchImage;
    public Sprite WarcryImage;

    protected override void Awake()
    {
       
        UnitName = "Milo";
        int Level = PartyManager.Instance.GetTrainProgression(MiloCharacter).GetLevel();

        MaxHP = MiloCharacter.HP[Level-1];
        CurrentHP = MiloCharacter.HP[Level - 1];

        MaxSecondaryResource = 100;
        CurrentSecondaryResource = 0;
        SecondaryResource = SecondaryResourceType.RG;

        Speed = new Stat(MiloCharacter.Speed[Level - 1]);
        Defense = new Stat(MiloCharacter.Defense[Level - 1]);
        Attack = new Stat(MiloCharacter.Attack[Level - 1]);
        Luck = new Stat(MiloCharacter.Luck[Level - 1]);

        TurnSprite = MiloCharacter.CharacterProfilePicture;

        

        base.Awake();

        Skills.Add(new TapPunch(this, TapPunchImage));
        Skills.Add(new SuperPunch(this, SuperPunchImage));
        Skills.Add(new Warcry(this, WarcryImage));
        Skills.Add(new Meditate(this, MeditateImage));

    }

   

}
