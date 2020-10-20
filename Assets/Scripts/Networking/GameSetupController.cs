using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameSetupController : MonoBehaviour
{
    [SerializeField] private Button homeButton = null;
    void Start()
    {
        CreatePlayer();
        //homeButton.onClick.RemoveAllListeners();
        //homeButton.onClick.AddListener(QuizManager.LoadHomeScreen);
    }

    /// <summary>
    /// Instantiates a player in the scene.
    /// </summary>
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
    }
}
