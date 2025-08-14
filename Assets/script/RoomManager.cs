using System.Collections.Generic; // 添加此行
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;


public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI 状态提示")]
    public TextMeshPro statusText;
    [Header("房间状态UI")]
    public TextMeshProUGUI OccupancyRateText_ForSchool;
    public TextMeshProUGUI OccupancyRateText_ForOutdoor;


    private string mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL; 

    #region Unity Callbacks
    void Start()
    {
        if (statusText != null)
            statusText.text = "准备加入房间...";

        PhotonNetwork.AutomaticallySyncScene = true;
        if(!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }
    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        if (statusText != null)
            statusText.text = "正在加入随机房间...";

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
            statusText.text = "加入失败，正在创建新房间...";

        CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers again.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"房间创建: {PhotonNetwork.CurrentRoom.Name} 地图:{mapType}");

        if (statusText != null)
            statusText.text = "房间创建成功，等待玩家加入...";
    }

    public override void OnJoinedRoom()
    {
        string message = $"{PhotonNetwork.NickName} 加入房间 {PhotonNetwork.CurrentRoom.Name} 玩家数:{PhotonNetwork.CurrentRoom.PlayerCount}";
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
            string message = $"{newPlayer.NickName} 加入. 当前玩家: {PhotonNetwork.CurrentRoom.PlayerCount}";
            statusText.text = message;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (statusText != null)
            statusText.text = $"连接断开: {cause}";
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            OccupancyRateText_ForSchool.text = 0 + "/" + 20;
            OccupancyRateText_ForOutdoor.text = 0 + "/" + 20;
        }
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                OccupancyRateText_ForOutdoor.text = room.PlayerCount + "/" + 20;
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
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

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    private void LoadSceneBasedOnMapType()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(
            MultiplayerVRConstants.MAP_TYPE_KEY,
            out object mapTypeObj))
        {
            string roomMapType = (string)mapTypeObj;
            Debug.Log($"主客户端加载地图: {roomMapType}");

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
