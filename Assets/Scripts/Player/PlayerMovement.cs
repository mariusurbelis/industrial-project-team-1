using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private VariableJoystick joystick;

    private void Awake()
    {
        player = GetComponent<Player>();
        joystick = FindObjectOfType<VariableJoystick>();

#if UNITY_ANDROID
#else
        if (joystick) Destroy(joystick.gameObject);
#endif
    }

    void Update()
    {
        if (player.IsMe)
        {
            player.myBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * 5000, Input.GetAxis("Vertical") * Time.deltaTime * 5000), ForceMode2D.Force);

            /*if (Input.GetKey(KeyCode.Q))
            {
                player.myBody.AddTorque(Time.deltaTime * 500, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                player.myBody.AddTorque(-Time.deltaTime * 500, ForceMode2D.Force);
            }*/

            if (joystick)
            {
                player.myBody.AddForce(new Vector2(joystick.Horizontal * Time.deltaTime * 5000, joystick.Vertical * Time.deltaTime * 5000), ForceMode2D.Force);
            }
        }
    }
}
