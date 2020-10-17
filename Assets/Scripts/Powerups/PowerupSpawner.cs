using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

    public enum SpawnState{ SPAWNING, IDLE, COUNTING};
    [System.Serializable]
    public class Spawner
    {
        public string spawnName;
        public Transform prefab;
        public int spawnAmount;
        public float spawnRate;
    }

    public Spawner[] spawners;
    private int nextSpawn = 0;
    public float timeBetweenSpawn = 5f;
    public float spawnCountDown;
    private float searchCountdown = 1f;

    public Transform[] spawnLocation;

    public SpawnState state = SpawnState.COUNTING;

      void Start()
    {
        if (spawnLocation.Length == 0)
        {
            Debug.Log("Spawn points are not given.");
        }

        //setting countdown to 5seconds
        spawnCountDown = timeBetweenSpawn;
    }

      void Update()
    {
        //checks if all the powerup has been collected or destroyed in game
        if (state == SpawnState.IDLE)
        {
            //check if powerups are still ingame
            if (PowersupIsStillAround() == false)
            {
                SpawnCompleted();
            }
            else
            {
                return;
            }
        }
        void SpawnCompleted()
        {
            //begin new round of spawning powerups
            Debug.Log("All powerups spawned!");

            state = SpawnState.COUNTING;
            spawnCountDown = timeBetweenSpawn;

            if (nextSpawn + 1 > spawners.Length - 1)
            {
                nextSpawn = 0;

                //possible additions of stack multiplyer here,new scene, etc
                Debug.Log("All spawns haven spawned. Looping now!");
            }
            else
            {
                nextSpawn++;

            }
           

        }

        //check if powerups are still in game 
        bool PowersupIsStillAround()
        {
            searchCountdown -= Time.deltaTime;

            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f; //resets searchcoutdown to 1second; in the event powerups are still around
                if (GameObject.FindGameObjectWithTag("PowerUp") == null)
                {
                    return false;
                }
            }
            return true;
        }

        if (spawnCountDown <= 0)
        {   
            //check if spawning has strted
            if (state != SpawnState.SPAWNING)
            {
                //strt spawning wave
                StartCoroutine( SpawnPowerups ( spawners [nextSpawn]));
            }
        }
        else
        {   
            //subtract the number time the last from was drawn 
            spawnCountDown -= Time.deltaTime; // setting coutdown to be  relevant to time and not frames per second
        }

        //buffer time before the method strts
        IEnumerator SpawnPowerups(Spawner spawn)
        {
            Debug.Log("Powerups spawning:"+ spawn.spawnName);
            state = SpawnState.SPAWNING;
            //loops how many times we want 
            for (int i = 0; i < spawn.spawnAmount; i++)
            {
                SpawnPowerUp(spawn.prefab, (Powerup.PowerupType)Random.Range(1, 9)); 
                yield return new WaitForSeconds(1f/ spawn.spawnRate) ; //time before the next spawn
            }
            // spawn state is at idle to allow the powerup to be either collected by player or despawn
            state = SpawnState.IDLE;
            yield break;
        }
        
        //instantiate powerup
        void SpawnPowerUp(Transform powerup, Powerup.PowerupType powerupType)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //spawn powerup
                Debug.Log("Spawning Powerup:" + powerup.name);

                //spawning the powerup in a random location

                Transform spawnPoint = spawnLocation[Random.Range(0, spawnLocation.Length)];
                PhotonNetwork.Instantiate(Path.Combine("Powerups", powerupType.ToString()), spawnPoint.position, spawnPoint.rotation);
                // PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            }
        }
    }













    //[System.Serializable]
    //public class Powerup
    //{
    //    public string name;
    //    public GameObject prefab;
    //    public float chance;
    //    public float delayTime;
    //    public int   spawnAmount;

    //    //coordinate boundaries to spawn the items
    //    public float positionX;
    //    public float positionY;
    //    public float positionZ;
    //}

    //public Transform[] points;
    //public Powerup[] powerups;
    //public int SpawnTimer;

    //Powerup chosenPowerUp;

    //float random;
    //float cumulative;

 

    //void Update()
    //{
    //    SpawnTimer = SpawnTimer + 1;
    //    if (SpawnTimer >= 100)
    //    {
    //        SpawnRandom();
    //        SpawnTimer = 0;
    //    }
    //}

    //void SpawnRandom()
    //{
    //    random = Random.value;
    //    cumulative = 0f;

    //    for (int i = 0; i < powerups.Length; i++)
    //    {
    //        cumulative += powerups[i].chance;
    //        if (random < cumulative && Time.time >= powerups[i].delayTime)
    //        {
    //            chosenPowerUp = powerups[i];
    //            break;
    //        }
    //    }
    //    //Debug.Log(points[Random.Range(0, points.Length)].position);
    //    //Instantiate(chosenPowerUp.prefab, points[Random.Range(0, points.Length)].position, points[Random.Range(0, points.Length)].rotation);

    //    //SpawnTimer = 0;
    //    for (int i = 0; i < powerups.Length; i++)
    //    {
    //        for (int x = 0; x < powerups[x].spawnAmount; x++)
    //        {
    //            Vector3 randomPos = new Vector3(Random.Range(-powerups[x].positionX, powerups[x].positionX), powerups[x].positionY, Random.Range(-powerups[x].positionZ, powerups[x].positionZ)) + transform.position; //offest to prevent object spawning out of bounds
    //            GameObject cloneObject = Instantiate(powerups[x].prefab, randomPos, Quaternion.identity);
    //            cloneObject.name = powerups[x].name;
    //            cloneObject.tag = powerups[x].prefab.tag;
    //        }

    //    }
      
    //}

  


  
}
