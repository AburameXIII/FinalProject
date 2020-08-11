using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    private Vector3 DestinationTransform;
    private Vector3 OriginTransform;
    private bool Lerp;

    private Vector3 DestinationScale;
    private Vector3 OriginScale;

    private Color DestinationColor;
    private Color OriginColor;


    public float LerpDuration;
    private float StartLerpTime;
    private bool Delete;

    public Text DamageText;

    bool Disappear;

    public void SetUpDamage(int Damage)
    {
        Disappear = false;
        Lerp = true;

        DamageText.text = Damage.ToString();
        StartLerpTime = Time.time;

        OriginTransform = Vector3.zero;
        //OriginScale = Vector3.zero;
        //OriginColor = new Color(0.9f, 0.75f, 0, 0);
        //DamageText.color = OriginColor;
        //this.transform.localScale = OriginScale;
        this.transform.localPosition = OriginTransform;

        DestinationTransform = new UnityEngine.Vector3(Random.Range(120, 200), Random.Range( 120, 200), 0);
        //DestinationScale = new Vector3(1, 1, 1);
        OriginColor = DestinationColor = new Color(0.9f, 0.75f, 0,1);

        DamageText.color = new Color(0.9f, 0.75f, 0, 1);
    }

    public void SetUpCriticalDamage(int Damage)
    {
        Disappear = false;
        Lerp = true;

        DamageText.text = Damage.ToString() + "!";
        StartLerpTime = Time.time;

        OriginTransform = Vector3.zero;
        //OriginScale = Vector3.zero;
        //OriginColor = new Color(0.9f, 0.5f, 0, 0);
        //DamageText.color = OriginColor;
        //this.transform.localScale = OriginScale;
        this.transform.localPosition = OriginTransform;

        DestinationTransform = new UnityEngine.Vector3(Random.Range(180, 200), Random.Range(180, 200), 0);
        //DestinationScale = new Vector3(1.5f, 1.5f, 1.5f);
        OriginColor = DestinationColor = new Color(0.9f, 0.5f, 0, 1);

        DamageText.color = new Color(0.9f, 0.5f, 0, 1);
    }

    void Update()
    {
        if (Lerp)
        {
            float Progress = (Time.time - StartLerpTime);
            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);
            this.transform.localPosition = Vector3.Lerp(OriginTransform, DestinationTransform, ProgressClamp);
            //this.transform.localScale = Vector3.Lerp(OriginScale, DestinationScale, ProgressClamp);
            DamageText.color = Color.Lerp(OriginColor, DestinationColor, ProgressClamp);
            if (LerpDuration < Progress && !Disappear)
            {
                Disappear = true;
                StartLerpTime = Time.time;
                LerpDuration *= 2;

                OriginTransform = DestinationTransform;
                //OriginScale = DestinationScale;
                OriginColor = DestinationColor;

                //DestinationTransform = OriginTransform * 1.5f;
               // DestinationScale = Vector3.zero;
                Color a = DestinationColor;
                a.a = 0;
                DestinationColor = a;

                
            }
            else if (LerpDuration < Progress && Disappear)
            {
                Lerp = false;
                Destroy(this.gameObject);
            }
        }

    }
}
