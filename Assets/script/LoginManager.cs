using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputName;
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion

    #region UI Callback methods
    public void ConnectAnoymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (PlayerName_InputName != null)
        {
            PhotonNetwork.NickName = PlayerName_InputName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion

    #region Photon Callback Methods
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called. The server is available!");
        PhotonNetwork.LoadLevel("Meetingscene");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Serve with player name:" + PhotonNetwork.NickName);
    }
    #endregion
}