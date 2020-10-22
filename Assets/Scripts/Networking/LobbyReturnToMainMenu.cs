using UnityEngine;
using UnityEngine.UI;

public class LobbyReturnToMainMenu : MonoBehaviour
{
    [SerializeField] private Button homeButton = null;
    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(QuizManager.LoadHomeScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
