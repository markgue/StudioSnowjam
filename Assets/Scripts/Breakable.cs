using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health--;
        if (health <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        Destroy(gameObject);
        // GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>().Scan(AstarPath.active.data.gridGraph);
        
    }
}
