using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    }
}
