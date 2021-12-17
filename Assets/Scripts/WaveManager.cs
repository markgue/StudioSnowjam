using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] baguettes; // UI Elements
    public GameObject[] doors;
    public GameObject[] chimneySets;

    GameObject[] chimneys;

    public int waveIndex = -1;
    Wave activeWave;
    Wave[] storedWaves = { new Wave(2, 3), new Wave(2, 10), new Wave(2, 15), new Wave(1, 20), new Wave(1, 25) };

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
        if (scan.Length == 0 && activeWave.IsFinished()) // ASSUMES ENEMY IS DELETED, ADD DESTRUCTION BEHAVIOR
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
        else if(waveIndex == 10)
        {
            doors[2].SetActive(false);
            chimneySets[2].SetActive(true);
        }
    }
}
