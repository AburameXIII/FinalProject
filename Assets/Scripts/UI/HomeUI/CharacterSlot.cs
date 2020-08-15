using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Image CharacterImage;
    public Image Background;
    public int SlotNumber;

    public void Setup()
    {
        if(SlotNumber < PartyManager.Instance.Party.Count)
        {
            Character c = PartyManager.Instance.Party[SlotNumber];
            CharacterImage.sprite = c.CharacterProfilePicture;
            Background.color = new Color(0.16f, 0.16f, 0.16f, 0.43f);
        } else
        {
            CharacterImage.sprite = null;
            CharacterImage.color = Color.clear;
            Background.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        }
    }

    public void Setup(Character c)
    {
       CharacterImage.sprite = c.CharacterProfilePicture;
       Background.color = new Color(0.16f, 0.16f, 0.16f, 0.43f);
    }

    private void Start()
    {
        Setup();
    }
}
