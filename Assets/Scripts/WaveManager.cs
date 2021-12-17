using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] baguettes; // UI Elements
    public GameObject[] doors;
    public GameObject[] chimneySets;
    
    public AudioClip[] endSounds;

    GameObject[] chimneys;

    public int waveIndex = -1;
    Wave activeWave;
    Wave[] storedWaves = { new Wave(2, 15), new Wave(2, 20), new Wave(1, 20), 
                           new Wave(2, 30), new Wave(2, 40), new Wave(1, 40),
                           new Wave(1, 40), new Wave(1, 50), new Wave(1, 60),
                           new Wave(2, 0)};

    // Start is called before the first frame update
    void Start()
    {
        chimneys = GameObject.FindGameObjectsWithTag("Chimney");
        ProgressWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activeWave.CheckSpawn(Time.fixedDeltaTime))
        {
            SpawnEnemy();
        }

        GameObject[] scan = GameObject.FindGameObjectsWithTag("Enemy");
        if (scan.Length == 0 && activeWave.IsFinished() && waveIndex < 10) // ASSUMES ENEMY IS DELETED, ADD DESTRUCTION BEHAVIOR
        {
            ProgressWave();
        }
    }

    class Wave
    {
        float frequencyTimer;

        float frequencyValue;
        int numEnemies;

        bool isFinished;

        public Wave(float frequencyValue, int numEnemies)
        {
            this.frequencyValue = frequencyValue;
            this.numEnemies = numEnemies;
            this.frequencyTimer = this.frequencyValue * 5; // give 'breathing room'
            this.isFinished = false;
        }

        public bool CheckSpawn(float fixedDeltaTime)
        {
            frequencyTimer -= fixedDeltaTime;
            if (frequencyTimer < 0.0f && numEnemies > 0)
            {
                frequencyTimer = frequencyValue;
                numEnemies--;
                return true;
            }

            return false;
        }

        public bool IsFinished()
        {
            return (numEnemies == 0);
        }
    }

    void SpawnEnemy()
    {
        int index = (int) (Random.value * chimneys.Length) % chimneys.Length;
        chimneys[index].GetComponent<Spawn>().SpawnEnemy();
    }

    void ProgressWave()
    {
        // add wave progression + UI
        waveIndex++;
        if (waveIndex > 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(endSounds[(int) (Random.value * endSounds.Length) % endSounds.Length]);
        }
        activeWave = storedWaves[waveIndex]; // add win condition
        baguettes[waveIndex].SetActive(true);
        if(waveIndex == 3)
        {
            doors[0].SetActive(false);
            chimneySets[0].SetActive(true);
        }
        else if(waveIndex == 6)
        {
            doors[1].SetActive(false);
            chimneySets[1].SetActive(true);
        }
        else if(waveIndex == 9)
        {
            doors[2].SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                chimneySets[i].SetActive(false);
            }
            
        }
        chimneys = GameObject.FindGameObjectsWithTag("Chimney");
    }
}
