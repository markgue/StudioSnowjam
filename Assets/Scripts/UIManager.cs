using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject scoreUI;
    public GameObject healthUI;
    public GameObject waveUI;
    public GameObject ammoUI;


    int score = -1;
    int health = 100;
    int ammoMag;
    int ammoInv;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        IncrementScore();
        SetHealth(100);

        ammoMag = 2;
        ammoInv = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    public void IncrementScore()
    {
        score++;
        scoreUI.GetComponent<Text>().text = "" + score;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetAmmo(int ammoMag, int ammoInv)
    {
        this.ammoMag = ammoMag;
        this.ammoInv = ammoInv;
    }
}
