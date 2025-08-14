using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    public static VirtualWorldManager Instance; // ����ʵ��

    #region Unity Callbacks
    private void Awake()
    {
        // ����ģʽ��ʼ��
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

    }
    #endregion

    #region Public Methods
    public void LeaveRoomAndLoadHomeScene()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region Photon Callback Methods
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to: " +
                 "Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        // �뿪�����Ͽ�����
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // �Ͽ����Ӻ����������
        PhotonNetwork.LoadLevel("HomeScene");
    }
    #endregion
}
