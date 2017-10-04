using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

	void Update()
	{
		if(!photonView.isMine)
		{
			// update local
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting) // local player : send data
		{
		}
		else // remote player : receive data
		{
		}
	}
}
