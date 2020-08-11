using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightScreenUI : MonoBehaviour
{
    public Button ExitButton;


    private void Awake()
    {
        ExitButton.onClick.AddListener(delegate { UIManager.Instance.FinishLevel(); });
    }
}
