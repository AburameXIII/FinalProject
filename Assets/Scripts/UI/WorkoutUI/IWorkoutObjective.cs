using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorkoutObjective
{
    void Setup(float Amount);
    void StartMeasuring();
    bool IsCompleted();
    void Stop();
}
