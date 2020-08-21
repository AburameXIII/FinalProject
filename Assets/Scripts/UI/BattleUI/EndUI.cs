using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    private List<Graphic> GraphicsToAnimate = new List<Graphic>();
    private List<Color> OriginColor = new List<Color>();
    private List<Color> DestinationColor = new List<Color>();

    private bool Lerp;

    public float LerpDuration;
    private float StartLerpTime;

    private bool Disable = false;

    private void Start()
    {
        GraphicsToAnimate = new List<Graphic>();
        foreach (Graphic g in GetComponentsInChildren<Graphic>())
        {
            GraphicsToAnimate.Add(g);
        }
    }

    public void FadeIn()
    {
        if(GraphicsToAnimate.Count == 0) Start();

        StartLerpTime = Time.time;

        Lerp = true;

        OriginColor = new List<Color>();
        DestinationColor = new List<Color>();
        foreach(Graphic g in GraphicsToAnimate)
        {
            Color d = g.color;
            d.a = 1;
            DestinationColor.Add(d);
            Color o = g.color;
            o.a = 0;
            g.color = o;
            OriginColor.Add(o);
        }

        

        Disable = false;

    }

    public void FadeOut()
    {
        StartLerpTime = Time.time;

        Lerp = true;

        OriginColor = new List<Color>();
        DestinationColor = new List<Color>();
        foreach (Graphic g in GraphicsToAnimate)
        {
            Color d = g.color;
            d.a = 1;
            OriginColor.Add(d);
            Color o = g.color;
            o.a = 0;
            DestinationColor.Add(o);
        }

        Disable = true;

    }


    void Update()
    {
        if (Lerp)
        {

            float Progress = Time.time - StartLerpTime;
            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);

            for(int i = 0; i < GraphicsToAnimate.Count; i++)
            {
                GraphicsToAnimate[i].color = Color.Lerp(OriginColor[i], DestinationColor[i], ProgressClamp);
            }
            if (LerpDuration < Progress)
            {
                Lerp = false;
                if (Disable) this.gameObject.SetActive(false);
            }

        }
    }
}
