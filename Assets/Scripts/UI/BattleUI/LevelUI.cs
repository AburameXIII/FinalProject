using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Level Level;

    public Text BossName;
    public Text BossTitle;
    public Button LevelStart;


    // Start is called before the first frame update
    void Awake()
    {
        BossName.text = Level.BossName;
        BossTitle.text = Level.BossTitle;

        LevelStart.onClick.AddListener(delegate { UIManager.Instance.LoadLevel(Level); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
