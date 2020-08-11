using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class DistanceTracker : MonoBehaviour, IWorkoutObjective
{
    public float CooldownAmount;
    private float Cooldown;

    private double PreviousLatitude;
    private double PreviousLongitude;

    public double CurrentDistance;
    public double ObjectiveDistance;

    const double EarthRadius = 6371e3;
    public bool Enabled = false;
    private bool Start = false;

    public void Setup(float Objective)
    {
        if (Input.location.isEnabledByUser) {
            StartCoroutine(GetLocation());
        }
        Cooldown = CooldownAmount;
        CurrentDistance = 0.0f;
        ObjectiveDistance = Objective;
    }

    public void StartMeasuring()
    {
        if (Enabled)
        {
            PreviousLatitude = Input.location.lastData.latitude;
            PreviousLongitude = Input.location.lastData.longitude;
        }
        Start = true;
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while(Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        //Resolve time out
        //Resolve LocationServiceStatusFailed

        Enabled = true;
        PreviousLatitude = Input.location.lastData.latitude;
        PreviousLongitude = Input.location.lastData.longitude;

        yield break;
    }

    void Update()
    {
        if(Cooldown <= 0.0f && Enabled && Start)
        {
            double CurrentLatitude = Input.location.lastData.latitude;
            double CurrentLongitude = Input.location.lastData.longitude;

            double PreviousLatAngle = PreviousLatitude * Math.PI / 180;
            double CurrentLatAngle = CurrentLatitude * Math.PI / 180;

            double LatAngleDifference = (CurrentLatitude - PreviousLatitude) * Math.PI / 180;
            double LonAngleDifference = (CurrentLongitude - PreviousLongitude) * Math.PI / 180;

            double a = Math.Sin(LatAngleDifference / 2) * Math.Sin(LatAngleDifference / 2) + Math.Cos(PreviousLatAngle) * Math.Cos(CurrentLatAngle) * Math.Sin(LonAngleDifference / 2) * Math.Sin(LonAngleDifference / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            CurrentDistance += EarthRadius * c;

            Cooldown = CooldownAmount;

            PreviousLatitude = CurrentLatitude;
            PreviousLongitude = CurrentLongitude;

        } else
        {
            Cooldown -= Time.deltaTime;
        }
    }

    public bool IsCompleted()
    {
        return CurrentDistance >= ObjectiveDistance;
    }
}
