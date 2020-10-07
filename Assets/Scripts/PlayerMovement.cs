using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private VariableJoystick joystick;

    private PhotonView photonView;
    private Rigidbody2D myBody;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<VariableJoystick>();

#if UNITY_ANDROID
#else
        if (joystick) Destroy(joystick.gameObject);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            myBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * 5000, Input.GetAxis("Vertical") * Time.deltaTime * 5000), ForceMode2D.Force);

            if (Input.GetKey(KeyCode.Q))
            {
                myBody.AddTorque(1f, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                myBody.AddTorque(-1, ForceMode2D.Force);
            }

            if (joystick)
            {
                myBody.AddForce(new Vector2(joystick.Horizontal * Time.deltaTime * 5000, joystick.Vertical * Time.deltaTime * 5000), ForceMode2D.Force);
            }
        }
    }
}
