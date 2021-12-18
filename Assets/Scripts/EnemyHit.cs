using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float halfExtent = 0.5f;
    public float maxDistance = 0.5f;
    public float cooldownValue = 1.0f;

    public float health = 2;

    public AudioClip hitSound;

    private AudioSource aud;

    // stuff for hop animation
    public GameObject coalModel;
    public float hopHeight = 0.02f;
    public float hopSpeed = 5f;
    private Vector3 localCoalBaseTransform;
    private float spawnTime;

    GameObject player;
    AIDestinationSetter destinationScript;
    float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        // find player object
        player = GameObject.FindGameObjectsWithTag("Player")[0]; // find top-level player game object
        destinationScript = gameObject.GetComponent<AIDestinationSetter>();
        destinationScript.target = player.transform;
        cooldownTimer = 0.0f;
        coalModel = transform.GetChild(0).gameObject;
        localCoalBaseTransform = coalModel.transform.localPosition;
        spawnTime = Time.time;
        aud = gameObject.GetComponent<AudioSource>();
    }

    // TO-DO: Swing and Damage

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if enemy should swing
        Vector3 origin = transform.position;
        RaycastHit[] hitInfos = Physics.BoxCastAll(origin,new Vector3(halfExtent, halfExtent,0.1f),transform.forward, transform.rotation, maxDistance);
        Debug.DrawRay(origin, transform.forward * maxDistance);

        for (int i = 0; cooldownTimer <= 0.0f && i < hitInfos.Length; i++)
        {
            if (hitInfos[i].collider.CompareTag("Player") || hitInfos[i].collider.CompareTag("Breakable"))
            {
                Hit(hitInfos);
            }
        }

        if (cooldownTimer > 0.0f)
        {
            cooldownTimer -= Time.fixedDeltaTime;
        }


        Hop();
    }

    void Hit(RaycastHit[] hitInfos)
    {
        // handle animation stuff
        
        cooldownTimer = cooldownValue;
        // handle damage
        for (int i = 0; i < hitInfos.Length; i++)
        {
            if (hitInfos[i].collider.CompareTag("Player"))
            {
                hitInfos[i].collider.gameObject.GetComponent<CharacterDeath>().GiveDamage(10); // hard coded, 10 hits = dead
                // Player.damage;
            }
        }

        // handle damage
        for (int i = 0; i < hitInfos.Length; i++)
        {
            if (hitInfos[i].collider.CompareTag("Breakable"))
            {
                hitInfos[i].collider.gameObject.GetComponent<Breakable>().Damage();
                break;
            }
        }
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            UIManager.GetInstance().IncrementScore();
        }
        // score
        // health
    }

    public void Hop()
    {
        coalModel.transform.localPosition = new Vector3(localCoalBaseTransform.x, localCoalBaseTransform.y + hopHeight * Mathf.Max(Mathf.Sin(Time.time * hopSpeed - spawnTime), 0), localCoalBaseTransform.z);
    }
}