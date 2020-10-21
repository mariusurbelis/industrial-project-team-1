using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TransferOwnership : MonoBehaviourPun, IPunOwnershipCallbacks
{   
    //run on enable
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    //run on disabled
    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
    {
        if (targetView != base.photonView)
        {
            return;
        }
        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
    {
        if (targetView != base.photonView)
        {
            return;
        }
    }

    private void OnDeletingObject()
    {
        base.photonView.RequestOwnership();
    }
   

    
}
