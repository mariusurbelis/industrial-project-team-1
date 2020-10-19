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
        public Transform prefab;
        public int spawnAmount;
        public float spawnRate;
    }

    public Spawner[] spawners;
    private int nextSpawn = 0;
    public float timeBetweenSpawn = 5f;
    public float spawnCountDown;
    private float searchCountdown = 1f;

    // gamescreen size for random spawn locations
    private float minX = -8f;
    private float maxX = 8f;
    private float minY = -4f;
    private float maxY = 4f;
  

    public SpawnState state = SpawnState.COUNTING;

      void Start()
    {
      

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
                Debug.Log("All spawns have spawned. Looping now!");
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
            Debug.Log("Powerups spawning");
            state = SpawnState.SPAWNING;
            //loops how many times we want 
            for (int i = 0; i < spawn.spawnAmount; i++)
            {   
                SpawnPowerUp(spawn.prefab, (Powerup.PowerupType)Random.Range(1, 1)); //spawning a random powerup
                yield return new WaitForSeconds(1f/ spawn.spawnRate) ; //time before the next spawn
            }

            // spawn state is at idle to allow the powerup to be either collected by player or despawned
            state = SpawnState.IDLE;
            yield break;
        }
        
        //instantiate powerup
        void SpawnPowerUp(Transform powerup, Powerup.PowerupType powerupType)
        {    
            if (PhotonNetwork.IsMasterClient)
            {
                //spawn powerup
                Debug.Log("Spawning Powerup:" + powerupType.ToString());

                //spawning the powerup in a random location in game
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0f);
                Quaternion spawnPositionRotation = (Quaternion.Euler(Random.Range(minX, maxX), Random.Range(minY, maxY), 0));

                GameObject spawnedObject = PhotonNetwork.Instantiate(Path.Combine("Powerups", powerupType.ToString()),spawnPosition, spawnPositionRotation);
            
            }
        }
    }
  
}
