using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public Bar HealthBar;
    public Bar SecondaryBar;
    public Text CharacterName;
    public Text SecondaryText;
    public static Dictionary<SecondaryResourceType, Color> SecondaryColors;


    public Dictionary<SkillEffect, EffectUI> EffectCount;
    public GameObject EffectPrefab;
    public List<Transform> EffectLocations;
    public Transform NextEffectLocation;
    private List<EffectUI> Effects;

    public void Awake()
    {
        EffectCount = new Dictionary<SkillEffect, EffectUI>();
        Effects = new List<EffectUI>();
        if(SecondaryColors == null)
        {
            SecondaryColors = new Dictionary<SecondaryResourceType, Color>();
            SecondaryColors.Add(SecondaryResourceType.MP, new Color(0.1176f, 0.6069f, 0.7450f));
            SecondaryColors.Add(SecondaryResourceType.RG, new Color(0.7450f, 0.5725f, 0.1176f));
            SecondaryColors.Add(SecondaryResourceType.PS, new Color(0.4070f, 0.2306f, 0.5094f));
            SecondaryColors.Add(SecondaryResourceType.COMBO, new Color(0.2f, 0.67f, 0.72f));
        }
        
    }


    public void Setup(Unit u)
    {
        CharacterName.text = u.UnitName;
        HealthBar.ChangeCurrentMax(u.CurrentHP, u.MaxHP);
        if (SecondaryBar)
        {
            SecondaryBar.ChangeCurrentMax(u.CurrentSecondaryResource, u.MaxSecondaryResource);
            SecondaryText.text = u.SecondaryResource.ToString();
            Color c = new Color();
            SecondaryColors.TryGetValue(u.SecondaryResource, out c);
            SecondaryBar.SetColor(c);
        }
    }

    public void ChangeHealth(int CurrentValue)
    {
        if (HealthBar) HealthBar.ChangeCurrentValue(CurrentValue);
    }

    public virtual void ChangeSecondary(int CurrentValue)
    {
        if (SecondaryBar) SecondaryBar.ChangeCurrentValue(CurrentValue);
    }

    public bool HasEffect(SkillEffect e)
    {
        return EffectCount.ContainsKey(e);
    }

    public void AddEffect(SkillEffect e)
    {
        //Effect already in play
        if (EffectCount.ContainsKey(e))
        {
            EffectUI eUI;
            EffectCount.TryGetValue(e, out eUI);
            eUI.amount++;
            eUI.UpdateStack();
        }
        //Effect not in play yet
        else
        {
            int EffectNumber = Effects.Count;
            EffectUI eUI;
            if (EffectNumber <= EffectLocations.Count)
            {

                eUI = Instantiate(EffectPrefab, EffectLocations[EffectNumber]).GetComponent<EffectUI>();

            } else
            {
                eUI = Instantiate(EffectPrefab, NextEffectLocation).GetComponent<EffectUI>();
                eUI.gameObject.SetActive(false);
                
            }
            eUI.SetSprite(e);
            Effects.Add(eUI);
            EffectCount.Add(e, eUI);

        }
    }

    public void RemoveEffect(SkillEffect e)
    {
        if (EffectCount.ContainsKey(e))
        {
            EffectUI eUI;
            EffectCount.TryGetValue(e, out eUI);
            if (eUI.amount > 1)
            {
                eUI.amount--;
                eUI.UpdateStack();
            }
            else
            {
                EffectCount.Remove(e);


                eUI.GoDown();
                int EffectIndex = Effects.IndexOf(eUI);

                if (EffectIndex < Effects.Count - 1)
                {
                    for (int i = Effects.IndexOf(eUI) + 1; i < Effects.Count; i++)
                    {
                        if (i == EffectLocations.Count - 1)
                        {
                            Effects[i].gameObject.SetActive(true);
                            Effects[i].GoIn(EffectLocations[i - 1]);
                            break;
                        }
                        Effects[i].GoNext(EffectLocations[i - 1]);
                    }
                }

                Effects.Remove(eUI);
            }
        }
        
    }
}


