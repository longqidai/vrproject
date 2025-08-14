using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI ״̬��ʾ")]
    public TextMeshPro statusText;
    public TextMeshProUGUI OccupancyRateText_ForSchool;
    public TextMeshProUGUI OccupancyRateText_ForOutdoor;
    private string mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL; 

    #region Unity Callbacks
    void Start()
    {
        if (statusText != null)
            statusText.text = "׼�����뷿��...";

        PhotonNetwork.AutomaticallySyncScene = true;
        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        if (statusText != null)
            statusText.text = "���ڼ����������...";

        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        var expectedProps = new ExitGames.Client.Photon.Hashtable();
        expectedProps[MultiplayerVRConstants.MAP_TYPE_KEY] = mapType;
        PhotonNetwork.JoinRandomRoom(expectedProps, 0);
    }

    public void OnEnterButtonClicked_School()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        var expectedProps = new ExitGames.Client.Photon.Hashtable();
        expectedProps[MultiplayerVRConstants.MAP_TYPE_KEY] = mapType;
        PhotonNetwork.JoinRandomRoom(expectedProps, 0);
    }
    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        if (statusText != null)
            statusText.text = "����ʧ�ܣ����ڴ����·���...";

        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"���䴴��: {PhotonNetwork.CurrentRoom.Name} ��ͼ:{mapType}");

        if (statusText != null)
            statusText.text = "���䴴���ɹ����ȴ���Ҽ���...";
    }

    public override void OnJoinedRoom()
    {
        string message = $"{PhotonNetwork.NickName} ���뷿�� {PhotonNetwork.CurrentRoom.Name} �����:{PhotonNetwork.CurrentRoom.PlayerCount}";
        Debug.Log(message);

        if (statusText != null)
            statusText.text = message;

        
        if (PhotonNetwork.IsMasterClient)
        {
            LoadSceneBasedOnMapType();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        if (!PhotonNetwork.IsMasterClient && statusText != null)
        {
            string message = $"{newPlayer.NickName} ����. ��ǰ���: {PhotonNetwork.CurrentRoom.PlayerCount}";
            statusText.text = message;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (statusText != null)
            statusText.text = $"���ӶϿ�: {cause}";
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(roomList.Count==0)
        {
            OccupancyRateText_ForSchool.text = 0 + "/" + 20;
            OccupancyRateText_ForOutdoor.text = 0 + "/" + 20;
        }
        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if(room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                OccupancyRateText_ForOutdoor.text = room.PlayerCount + "/" + 20;
            }
            else if(room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
            {
                OccupancyRateText_ForOutdoor.text = room.PlayerCount + "/" + 20;
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" +mapType+ Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 20,
            
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { MultiplayerVRConstants.MAP_TYPE_KEY, this.mapType }
            },
            CustomRoomPropertiesForLobby = new[] { MultiplayerVRConstants.MAP_TYPE_KEY }
        };

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        Debug.Log($"��������: {randomRoomName} ��ͼ:{mapType}");

        if (statusText != null)
            statusText.text = $"��������: {randomRoomName}...";
    }

    private void LoadSceneBasedOnMapType()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(
            MultiplayerVRConstants.MAP_TYPE_KEY,
            out object mapTypeObj))
        {
            string roomMapType = (string)mapTypeObj;
            Debug.Log($"���ͻ��˼��ص�ͼ: {roomMapType}");

            if (roomMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL)
            {
                PhotonNetwork.LoadLevel("World_School");
            }
            else if (roomMapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
            {
                PhotonNetwork.LoadLevel("World_Outdoor");
            }
        }
    }
    #endregion
}
