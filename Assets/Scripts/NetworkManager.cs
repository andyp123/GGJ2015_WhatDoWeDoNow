using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	public bool offlineMode = false;

	void Start()
	{
		Connect();
	}

	void Connect()
	{
		if(offlineMode)
		{
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		}
		else
		{
			PhotonNetwork.ConnectUsingSettings("andyp123_GGJ2015");
		}
	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		Debug.Log("Joined Lobby");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Creating Room");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom()
	{
		SpawnMyPlayer();
	}

	void SpawnMyPlayer()
	{
		// sync level data

		// instantiate local player and enable relevant components
		// GameObject player = (GameObject)PhotonNetwork.Instantiate("PlayerController", spawnPoint.transform.position, spawnPoint.transform.rotation, 0);
		// ((MonoBehaviour)player.GetComponent("CharacterMotor")).enabled = true;
		// ((MonoBehaviour)player.GetComponent("FPSInputController")).enabled = true;
		// ((MonoBehaviour)player.GetComponent("MouseLook")).enabled = true;
		// ((MonoBehaviour)player.GetComponent("PlayerShooting")).enabled = true;
		// player.transform.FindChild("Camera").gameObject.SetActive(true);
	}
}
