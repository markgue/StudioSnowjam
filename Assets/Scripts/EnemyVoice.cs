using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    public AudioClip[] voicelines;
    public float voiceValue;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(voicelines[(int)(Random.value * voicelines.Length) % voicelines.Length]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
