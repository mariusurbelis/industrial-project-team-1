using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private VariableJoystick joystick;

    private PhotonView photonView;
    private Rigidbody myBody;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody>();
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
            myBody.AddForce(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5000, Input.GetAxis("Vertical") * Time.deltaTime * 5000, 0), ForceMode.Force);

            if (Input.GetKey(KeyCode.Q))
            {
                myBody.AddTorque(Vector3.forward);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                myBody.AddTorque(-Vector3.forward);
            }

            if (joystick)
            {
                myBody.AddForce(new Vector3(joystick.Horizontal * Time.deltaTime * 5000, joystick.Vertical * Time.deltaTime * 5000, 0), ForceMode.Force);
            }
        }
    }
}
