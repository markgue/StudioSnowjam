using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public float halfExtent = 0.5f;
    public float maxDistance = 0.5f;
    public float cooldownValue = 1.0f;

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
    }

    // TO-DO: Swing and Damage

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if enemy should swing
        Vector3 origin = transform.position;
        RaycastHit[] hitInfos = Physics.BoxCastAll(origin,new Vector3(0.1f,halfExtent,halfExtent),transform.forward, transform.rotation, maxDistance);
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
        // score
        // health
    }
}
