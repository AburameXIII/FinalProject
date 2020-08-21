using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]  private float BaseValue;
    
    public float Value {
        get {
            if (isDirty) {
                FinalValue = CalculateFinalValue();
                isDirty = false;
            }
            return FinalValue;
        } 
    }

    private bool isDirty = true;
    private float FinalValue;

    [SerializeField] private List<StatModifier> statModifiers;

    public Stat(float BaseValue) : this()
    {
        this.BaseValue = BaseValue;
    }

    public Stat(){
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier StatModifer)
    {
        isDirty = true;
        statModifiers.Add(StatModifer);
        statModifiers.OrderBy(c => (int)c.Type);
    }

    public void RemoveModifier(StatModifier StatModifer)
    {
        isDirty = statModifiers.Remove(StatModifer);
    }

    public void RemoveAllModifiers()
    {
        isDirty = true;
        statModifiers.Clear();
    }

    private float CalculateFinalValue()
    {
        float FinalValue = BaseValue;
        float SumPercentAdd = 0;


        //TODO CHANGE MODIFIER ORDER IF NECESSARY
        for(int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier Modifier = statModifiers[i];
            switch (Modifier.Type)
            {
                case StatModType.Flat:
                    FinalValue += Modifier.Value;
                    break;
                case StatModType.Percentage:
                    SumPercentAdd += Modifier.Value;

                    if (i + 1 >= statModifiers.Count)
                    {
                        FinalValue *= 1 + SumPercentAdd;
                    }
                    break;
            }
        }

        FinalValue = Mathf.Clamp(FinalValue, 0, 999);
        return FinalValue;
    }
}
