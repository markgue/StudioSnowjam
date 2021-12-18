using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    //Lifetime
    public int maxCollisions;
    public float maxLifeTime;
    public bool explodeOnTouch = true;
    bool isActive = true;

    int collisions;
    PhysicMaterial physics_mat;

    //Sound
    private AudioSource aud;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode:
        if(collisions> maxCollisions)
        {
            Explode();
        }

        //Count down lifetime
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0)
            Explode();
    }

    private void Explode()
    {
        //Instantiate explosion
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //Get component of enemy and call Take Damage

            //Just an example!
            //enemies[i].GetComponent<ShootingAI>().TakeDamage(explosionDamage);

            //if (enemies[i].GetComponent<Rigidbody>())
            //{
            //enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            //Destroy(enemies[i]);
            //}
            enemies[i].transform.parent.GetComponent<EnemyHit>().Damage();
            Debug.Log("ENEMY FOUND");
        }

        //Add a little delay, just to make sure everything works fine
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Don't count collisions with other bullets
        if (collision.collider.CompareTag("Bullet"))
        {
            return;
        }

        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if(collision.collider.CompareTag("Enemy") && isActive)
        {
            // Explode();
            collision.collider.transform.parent.GetComponent<EnemyHit>().Damage();
            aud.Play();
            isActive = false;
        }
        if (collision.collider.CompareTag("Environment"))
        {
            isActive = false;
        }
    }

    private void Setup()
    {
        //Create a new Physics material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        //GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
        
        //Get audio source
        aud = gameObject.GetComponent<AudioSource>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
