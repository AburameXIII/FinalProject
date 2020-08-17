using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkoutUI : MonoBehaviour
{
    public RectTransform WorkoutContainer;
    public GameObject WorkoutPrefab;

    public float StartY = -225;
    public float YDistance = 475;

    // Start is called before the first frame update
    void Start()
    {



        UpdateUI();
    }

    public void UpdateUI()
    {
        int count = 0;
        float Y = StartY;


        foreach(Character c in PartyManager.Instance.Party)
        {
            foreach (WorkoutUnlock w in c.WorkoutUnlocks)
            {
                var newWorkoutUI = Instantiate(WorkoutPrefab, Vector3.zero, Quaternion.identity);
                newWorkoutUI.transform.SetParent(WorkoutContainer.transform, false);
                newWorkoutUI.transform.localPosition = new Vector3(0, Y, 0);
                newWorkoutUI.GetComponent<WorkoutContainer>().UpdateWorkout(c,w);

                Y -= YDistance;
                count++;
            }
        }


        

        WorkoutContainer.sizeDelta = new Vector2(WorkoutContainer.sizeDelta.x, count * YDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
