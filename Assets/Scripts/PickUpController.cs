using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    //public ProjectileGun gunScript;
    public GameObject gun;
    public int ammoAmt;
    //public Rigidbody rb;
    //public BoxCollider coll;
    //public Transform player;
    //public Transform gunContainer;
    //public Transform fpsCam;

    //public float pickUpRange;
    //public float dropForwardForce;
    //public float dropUpwardForce;

    //public bool equipped;
    //public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision Detected");
        if (collider.GetComponent<Collider>().CompareTag("Player"))
        {
            Debug.Log("Player detected");
            //gun.GetComponent<ProjectileGun>().EquipAmmo(ammoAmt);
            collider.gameObject.GetComponentInChildren<ProjectileGun>().EquipAmmo(ammoAmt);
            Destroy(gameObject);
        }
    }
}
