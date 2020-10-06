using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;
    private Rigidbody myBody;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            myBody.AddForce(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5000, Input.GetAxis("Vertical") * Time.deltaTime * 5000, 0), ForceMode.Force);
        }
    }
}
