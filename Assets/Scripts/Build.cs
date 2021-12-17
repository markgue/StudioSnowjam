using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public float buildValue = 1.0f;
    public GameObject baguette;

    public float height = 1.0f;

    float buildTimer = 0.0f;
    GameObject[] baguettes = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(baguette, transform.position + new Vector3(0, Random.value * height, 0), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // add additional checks for proximity
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("START");
            buildTimer = buildValue;
        }

        if (Input.GetKeyUp("e"))
        {
            Debug.Log("END");
            buildTimer = 0.0f;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("e"))
        {
            Debug.Log("HOLD");
            buildTimer -= Time.fixedDeltaTime;
            Debug.Log(buildTimer);
            if (buildTimer < 0.0f)
            {
                buildTimer = buildValue;

                SpawnBaguette();
            }
        }
    }

    void SpawnBaguette()
    {
        for (int i = 0; i < baguettes.Length; i++)
        {
            if (baguettes[i] == null)
            {
                Debug.Log("BUILD");
                baguettes[i] = Instantiate(baguette, transform.position + new Vector3(0, Random.value * height, 0), transform.rotation);
                baguettes[i].transform.parent = gameObject.transform;
                break;
            }
        }
    }
}
