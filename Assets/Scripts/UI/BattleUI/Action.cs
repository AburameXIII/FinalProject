using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : Button
{
   
    public new List<Graphic> targetGraphic;
    public Text SkillName;
    public Text SkillDescription;
    public Image SkillImage;

    public void SetSkill(Unit c, Skill s)
    {
        if(s != null)
        {
            interactable = s.CanPerform();
            SkillName.text = s.SkillName;
            SkillImage.sprite = s.SkillImage;
            SkillDescription.text = s.SkillDescription;
            onClick.RemoveAllListeners();
            onClick.AddListener(delegate {
                CombatManager.Instance.Actions.Disappear();
                CombatManager.Instance.PerformingAction();
                c.PerformSkill(s);
            });
        } else {
            interactable = false;
        }        
        
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
        var targetColor =
           state == SelectionState.Disabled ? colors.disabledColor :
           state == SelectionState.Highlighted ? colors.highlightedColor :
           state == SelectionState.Normal ? colors.normalColor :
           state == SelectionState.Pressed ? colors.pressedColor :
           state == SelectionState.Selected ? colors.selectedColor : Color.white;

        foreach (var graphic in targetGraphic)
        {
            graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }

    }
}
