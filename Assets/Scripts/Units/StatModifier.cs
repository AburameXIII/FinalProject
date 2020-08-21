using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat,
    Percentage
}

[System.Serializable]
public class StatModifier 
{
    public float Value;
    public StatModType Type;

    public StatModifier(float Value, StatModType StatModType)
    {
        this.Value = Value;
        this.Type = StatModType;
    }
}
