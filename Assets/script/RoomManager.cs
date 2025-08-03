using Photon.Pun;
using Photon.Realtime;
using TMPro; // 添加TextMeshPro命名空间
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI 状态提示")]
    public TextMeshPro statusText; // VR中显示状态的文本组件

    #region Unity Callbacks
    void Start()
    {
        // 初始状态提示
        if (statusText != null)
            statusText.text = "准备加入房间...";
    }
    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        if (statusText != null)
            statusText.text = "正在加入随机房间...";

        PhotonNetwork.JoinRandomRoom();
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

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);

        if (statusText != null)
            statusText.text = "房间创建成功，等待玩家加入...";
    }

    public override void OnJoinedRoom()
    {
        string message = $"玩家 {PhotonNetwork.NickName} 加入了房间 {PhotonNetwork.CurrentRoom.Name+"   player count:"+PhotonNetwork.CurrentRoom.PlayerCount}";
        Debug.Log(message);

        if (statusText != null)
            statusText.text = message;
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("joined room with the map:" + (string)mapType);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string message = $"{newPlayer.NickName} 加入了房间。当前玩家数: {PhotonNetwork.CurrentRoom.PlayerCount}";
        Debug.Log(message);

        if (statusText != null)
            statusText.text = message;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (statusText != null)
            statusText.text = $"连接断开: {cause}";
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        // 1. 生成随机房间名
        string randomRoomName = "Room_" + Random.Range(0, 10000);

        // 2. 创建房间选项
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        // 3. 设置房间的自定义属性（地图类型）
        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        // 可选地图类型：outdoor(室外) 或 school(学校)
        string mapType = "school"; // 默认为学校地图

        // 创建自定义房间属性哈希表
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL } };

        // 4. 配置房间属性
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        // 5. 创建并加入房间
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        Debug.Log($"正在创建房间: {randomRoomName} (地图: {mapType})");
    }
    #endregion

}
