using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public Image Background;
    public Image Sprite;
    public Outline Border;

    private Vector3 DestinationTransform;
    private Vector3 OriginTransform;
    private bool LerpTransform;

    private Vector3 DestinationScale;
    private Vector3 OriginScale;
    private bool LerpScale;

    private float DestinationColor;
    private float OriginColor;
    private bool LerpColor;

    private float DestinationColorBorder;
    private float OriginColorBorder;
    private bool LerpColorBorder;

    public float LerpDuration;
    private float StartLerpTime;
    private bool Delete;

    public Color FriendlyColor;
    public Color EnemyColor;

    private void Start()
    {
    }

    public void SetSprite(Unit u)
    {

        Sprite.sprite = u.TurnSprite;
        switch (u.UnitType)
        {
            case UnitType.Friendly:
                Background.color = FriendlyColor;
                break;
            case UnitType.Enemy:
                Background.color = EnemyColor;
                break;
        }
    }

    public void GoUp()
    {
        StartLerpTime = Time.time;

        LerpTransform = true;
        OriginTransform = this.transform.position;
        DestinationTransform = this.transform.position + new Vector3(0,500,0);

        LerpColor = true;
        OriginColor = 1f;
        DestinationColor = 0f;

        Delete = true;
    }

    public void GoFirst(Transform t)
    {
        StartLerpTime = Time.time;

        LerpTransform = true;
        OriginTransform = this.transform.position;
        DestinationTransform = t.position;

        LerpScale = true;
        OriginScale = this.transform.localScale;
        DestinationScale = t.localScale;

        LerpColorBorder = true;
        OriginColorBorder = 0f;
        DestinationColorBorder = 1f;
    }

    public void GoNext(Transform t)
    {
        StartLerpTime = Time.time;

        LerpTransform = true;
        OriginTransform = this.transform.position;
        DestinationTransform = t.position;
    }


    void Update()
    {
        if (LerpTransform)
        {
            float Progress = Time.time - StartLerpTime;
            float ProgressClamp = Mathf.Clamp(Progress/ LerpDuration, 0, 1);
            this.transform.position = Vector3.Lerp(OriginTransform, DestinationTransform, ProgressClamp);
            if (LerpDuration < Progress)
            {
                //this.transform.position = DestinationTransform;
                LerpTransform = false;
                if (Delete)
                {
                    Destroy(this.gameObject);
                } 
            }
        }


        if (LerpScale)
        {
            
            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress/ LerpDuration, 0, 1);
            this.transform.localScale = Vector3.Lerp(OriginScale, DestinationScale, ProgressClamp);
            if (LerpDuration < Progress)
            {
                //this.transform.localScale = DestinationScale;

                LerpScale = false;
            }
            
        }

        if (LerpColor)
        {
            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress/ LerpDuration, 0, 1);

            float a = Mathf.Lerp(OriginColor, DestinationColor, ProgressClamp);
            Color c1 = new Color(Sprite.color.r, Sprite.color.g, Sprite.color.b, a);
            Color c2 = new Color(Background.color.r, Background.color.g, Background.color.b, a);
            Color c3 = new Color(Border.effectColor.r, Border.effectColor.g, Border.effectColor.b, a);

            Sprite.color = c1;
            Background.color = c2;
            Border.effectColor = c3;

            if (LerpDuration < Progress)
            {
                LerpColor = false;
            }
        }

        if (LerpColorBorder)
        {
            float Progress = Time.time - StartLerpTime;

            float ProgressClamp = Mathf.Clamp(Progress / LerpDuration, 0, 1);

            float a = Mathf.Lerp(OriginColorBorder, DestinationColorBorder, ProgressClamp);
            Color c3 = new Color(Border.effectColor.r, Border.effectColor.g, Border.effectColor.b, a);

            Border.effectColor = c3;

            if (LerpDuration < Progress)
            {
                LerpColor = false;
            }
        }
    }
}
