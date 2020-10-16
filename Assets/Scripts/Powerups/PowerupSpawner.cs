using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

    [System.Serializable]
    public class powerUp
    {
        public string name;
        public GameObject prefab;
        public float chance;
        public float delayTime;
    }

    public Transform[] points;
    public powerUp[] powerUps;
    public int SpawnTimer;

    powerUp chosenPowerUp;
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

        for (int i = 0; i < powerUps.Length; i++)
        {
            cumulative +=powerUps[i].chance;
            if (random < cumulative && Time.time >= powerUps[i].delayTime)
            {
                 chosenPowerUp=powerUps[i];
                break;
            }
        }

        Instantiate(chosenPowerUp.prefab, points[Random.Range(0, points.Length)].position, points[Random.Range(0, points.Length)].rotation);
    }
}
