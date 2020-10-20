using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string username = "";
    public Color playerColor;

    public string Username => GetComponent<PlayerID>().PlayerDisplayName;
    public Color PlayerColor => GetComponent<PlayerAvatar>().PlayerDisplayColor;

    public PhotonView photonView;
    public Rigidbody2D myBody;
    private Animator animator;
    private bool isDead = false;

    public Powerup.PowerupType powerup = Powerup.PowerupType.None;

    public int health = 3;

    public int selectedOption = -1;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (PlayerDataManager.ValueExists(PlayerDataManager.PlayerColor))
        {
            if (ColorUtility.TryParseHtmlString(PlayerDataManager.LoadData(PlayerDataManager.PlayerColor), out playerColor))
            {
                //Debug.Log("Player color successfully set");
            }
        }

        if (PlayerDataManager.ValueExists(PlayerDataManager.PlayerUsername))
        {
            username = PlayerDataManager.LoadData(PlayerDataManager.PlayerUsername);
        }

        gameObject.AddComponent<PlayerAvatar>();
        gameObject.AddComponent<PlayerID>();

    }


    /// <summary>
    /// Informs the player that a round is done. Checks if player answered correctly and changes its health accordingly. If health reaches 0 player loses.
    /// </summary>
    public void RegisterRoundDone()
    {
        // return if dead ---------------
        if (isDead) return;
        //if(!IsMe) return;
        //Debug.Log($"Player {username} selected {selectedOption} option");

        // Selected option to check against the correct answer
        if (selectedOption != QuizManager.currentCorrectAnswerID)
        {
            health--;
            Sound.PlayScreamSound(Sound.screamSound);
            ToggleMovement(false);
            animator.SetTrigger((selectedOption != -1) ? "Die" : "Melt");

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(BackToMiddle());
            }

        }
        else
        {
            Sound.PlayCorrectSound(Sound.correctSound);
        }
    }

    [PunRPC]
    void RPC_BeAffectedByPowerup(Powerup.PowerupType powerupType, Vector2 powerupPosition)
    {
        Debug.Log("Got affected by a powerup");

        Sound.PlayBombSound(Sound.bombSound);

        switch (powerupType)
        {
            case Powerup.PowerupType.Bomb:
                //myBody.AddForce(powerupPosition - (Vector2)transform.position, ForceMode2D.Impulse);
                myBody.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
                break;
        }
    }

    private IEnumerator BackToMiddle()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.position = Vector2.zero;
        StartCoroutine(EnableMovementAfterTime());
        yield return null;
    }

    private void ToggleMovement(bool active)
    {
        if (TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = active;
        }
    }

    private IEnumerator EnableMovementAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        ToggleMovement(true);
        yield return null;
    }

    public void UsePowerup()
    {
        Debug.Log($"{Username} used a powerup");

        Sound.PlayBombSound(Sound.bombSound);

        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.photonView.RPC("RPC_BeAffectedByPowerup", RpcTarget.All, powerup, (Vector2)transform.position);
        }

        powerup = Powerup.PowerupType.None;
    }

    /// <summary>
    /// Player's movement is disabled and the player is taken back to the center of the game screen.
    /// </summary>
    private void Die()
    {
        //Adds players username to a list in the order they were eliminated
        QuizManager.eliminationList.Add(Username);
        //Debug.Log($"Adding {Username} to the elimination list");

        PlayerList.UpdateList();

        // Temporary
        Destroy(gameObject.GetComponent<PlayerMovement>());
        transform.position = new Vector2(100, 100f);
        isDead = true;
    }

    public void PickUpPowerup(Powerup.PowerupType powerupType)
    {
        powerup = powerupType;
    }

    /// <summary>
    /// Checks if the player object belongs to the player currently controling the game.
    /// </summary>
    public bool IsMe => photonView.IsMine;

    /// <summary>
    /// Returns the unique identifier of the player.
    /// </summary>
    public string PlayerID => photonView.ViewID.ToString();

    /// <summary>
    /// Returns the player username or the unique ID if the username field is empty.
    /// </summary>
    public string PlayerName => ((username.Length < 2) ? PlayerID : username);

    /// <summary>
    /// Returns the player object that belongs to the actual player.
    /// </summary>
    public static Player Me
    {
        get
        {
            foreach (Player player in FindObjectsOfType<Player>())
            {
                if (player.IsMe)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
