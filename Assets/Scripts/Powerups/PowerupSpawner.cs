using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Powerup
    {
        public string name;
        public GameObject prefab;
        public float chance;
        public float delayTime;
    }

    public Transform[] points;
    public Powerup[] powerups;
    public int SpawnTimer;

    Powerup chosenPowerUp;

    float random;
    float cumulative;

    void Start()
    {
        SpawnTimer = 0;
    }

    void Update()
    {
        SpawnTimer = SpawnTimer + 1;
        if (SpawnTimer >= 100)
        {
            SpawnRandom();
            SpawnTimer = 0;
        }
    }

    void SpawnRandom()
    {
        random = Random.value;
        cumulative = 0f;

        for (int i = 0; i < powerups.Length; i++)
        {
            cumulative += powerups[i].chance;
            if (random < cumulative && Time.time >= powerups[i].delayTime)
            {
                chosenPowerUp = powerups[i];
                break;
            }
        }

        Instantiate(chosenPowerUp.prefab, points[Random.Range(0, points.Length)].position, points[Random.Range(0, points.Length)].rotation);
        
    }
}
