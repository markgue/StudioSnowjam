using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject imgObject;
    private Image img;
    private CharacterDeath script;
    private RectTransform tranRect;
    //[SerializeField] private float health;
    static private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        script = Player.GetComponent<CharacterDeath>();
        //text = this.gameObject.GetComponentInChildren<TextMeshPro>();
        img = imgObject.GetComponent<Image>();
        tranRect = imgObject.transform.GetComponent<RectTransform>();
        vec = tranRect.localPosition;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float health = (float) (script.getHealth());
        health /= 100f;
        text.SetText(script.getHealth().ToString());
        img.fillAmount = health;
        float width = tranRect.sizeDelta.x * tranRect.localScale.x;
        float shift = (1f-health) * width;
        
        tranRect.localPosition = vec + new Vector3(-shift, 0, 0);

    }
}
