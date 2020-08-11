using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    public List<Graphic> GraphicsToAnimate;
    private List<Color> OriginColor = new List<Color>();
    private List<Color> DestinationColor = new List<Color>();

    private bool Lerp;

    public float LerpDuration;
    private float StartLerpTime;

    private bool Disable = false;

    public void FadeIn()
    {
        StartLerpTime = Time.time;

        Lerp = true;

        OriginColor = new List<Color>();
        DestinationColor = new List<Color>();
        foreach(Graphic g in GraphicsToAnimate)
        {
            OriginColor.Add(g.color);
            Color c = g.color;
            c.a = 1;
            DestinationColor.Add(c);
        }

        Color background = DestinationColor[0];
        background.a = 0.67f;
        DestinationColor[0] = background;

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
            OriginColor.Add(g.color);
            Color c = g.color;
            c.a = 0;
            DestinationColor.Add(c);
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
