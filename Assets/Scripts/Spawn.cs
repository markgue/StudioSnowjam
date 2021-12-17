using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;

    public float radius = 1.0f;
    public float height = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("SPAWN");
            SpawnEnemy(); // can add game object argument
        }
    }

    public void SpawnEnemy()
    {
        GameObject temp = Instantiate(enemy, transform.position + new Vector3(Random.value * radius, Random.value * height, Random.value * radius), transform.rotation);
        temp.transform.parent = gameObject.transform;
    }
}
