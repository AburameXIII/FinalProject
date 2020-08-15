using System;
using UnityEngine;

public class PaceTracker : MonoBehaviour, IWorkoutObjective
{
    public float CooldownAmount;
    private float Cooldown;

    public float Pace;
    public int Amount;
    public int CurrentAmount;

    private Bar Bar;

    private bool Start = false;

    public void Setup(float Pace, int Amount, Bar CountBar)
    {
        this.Pace = Pace;
        this.Amount = Amount;
        this.Bar = CountBar;
        CurrentAmount = 0;
    }

    public void StartMeasuring()
    {
        CurrentAmount = 0;
        Cooldown = Pace;
        Start = true;
    }

    public void Stop()
    {
        Start = false;
        CurrentAmount = 0;
    }


    void Update()
    {
        if(Start && Cooldown <= 0.0f)
        {
            CurrentAmount++;
            if(Bar!=null) Bar.ChangeCurrentValue(CurrentAmount);

            if (IsCompleted())
            {
                Start = false;
            }

            Cooldown = Pace;

        } else
        {
            Cooldown -= Time.deltaTime;
        }
    }

    //Perhaps use an event OnCompleted with subcription events
    //Add to the code refactoring ideas
    public bool IsCompleted()
    {
        return CurrentAmount == Amount;
    }

    public void Setup(float Amount)
    {
        Setup(0, Mathf.RoundToInt(Amount), null);
    }
}
