using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenPosition
{
    Right, Left, Center
}

public class UIScreen : MonoBehaviour
{
    private float Destination;
    private float Origin;
    private bool Lerp;
    public float DefaultLerpDuration;
    private float LerpDuration;
    private float StartLerpTime;
    public RectTransform RectTransform;
    private float ScreenOffset;
    public ScreenPosition InitialPosition;

    void Awake()
    {
        RectTransform.sizeDelta = new Vector2(1600, 1 / Camera.main.aspect * 1600);
        float newScale = Camera.main.orthographicSize * 2 / RectTransform.sizeDelta.y;
        RectTransform.localScale = Vector3.one * newScale;
        ScreenOffset = 1600 * newScale;

        switch (InitialPosition)
        {
            case ScreenPosition.Center:
                this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
                break;
            case ScreenPosition.Left:
                this.transform.position = new Vector3(-ScreenOffset + Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
                break;
            case ScreenPosition.Right:
                this.transform.position = new Vector3(ScreenOffset + Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Lerp)
        {
            float Progress = Time.time - StartLerpTime;
            this.transform.position = new Vector3(Mathf.Lerp(Origin, Destination, Progress / LerpDuration), this.transform.position.y, this.transform.position.z);
            if (LerpDuration < Progress)
            {
                Lerp = false;
            }
        }
    }

    public void GoCenter()
    {
        GoCenter(DefaultLerpDuration);
    }

    public void GoLeft()
    {
        GoLeft(DefaultLerpDuration);
    }

    public void GoRight()
    {
        GoRight(DefaultLerpDuration);
    }

    public void GoCenter(float Duration)
    {
        Origin = this.transform.position.x;
        Destination = 0;
        StartLerpTime = Time.time;
        LerpDuration = Duration;
        Lerp = true;
    }

    public void GoLeft(float Duration)
    {
        Origin = this.transform.position.x;
        Destination = -ScreenOffset;
        StartLerpTime = Time.time;
        LerpDuration = Duration;
        Lerp = true;
    }

    public void GoRight(float Duration)
    {
        Origin = this.transform.position.x;
        Destination = ScreenOffset;
        StartLerpTime = Time.time;
        LerpDuration = Duration;
        Lerp = true;
    }
}
