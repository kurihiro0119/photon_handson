using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    /// This client's version number.
    /// Users are separated from each other by gameVersion (which allows you to make breaking changes
    string gameVersion = "1";

    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [SerializeField]
    private GameObject controlPanel;

    [SerializeField]
    private GameObject progressLabel;

    private void Awake()
    {
        // this makes sure we can use PhotonNetwork.LoadLevel()
        // on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

   public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN ConnectedToMater");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnectCause");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
}
