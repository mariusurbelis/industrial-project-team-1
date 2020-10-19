﻿using Photon.Pun;
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

    /// <summary>
    /// Instantiates a player in the scene.
    /// </summary>
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    }
}
