using Photon.Pun;
using Photon.Realtime;
using TMPro; // ���TextMeshPro�����ռ�
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI ״̬��ʾ")]
    public TextMeshPro statusText; // VR����ʾ״̬���ı����

    #region Unity Callbacks
    void Start()
    {
        // ��ʼ״̬��ʾ
        if (statusText != null)
            statusText.text = "׼�����뷿��...";
    }
    #endregion

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        if (statusText != null)
            statusText.text = "���ڼ����������...";

        PhotonNetwork.JoinRandomRoom();
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
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);

        if (statusText != null)
            statusText.text = "���䴴���ɹ����ȴ���Ҽ���...";
    }

    public override void OnJoinedRoom()
    {
        string message = $"��� {PhotonNetwork.NickName} �����˷��� {PhotonNetwork.CurrentRoom.Name+"   player count:"+PhotonNetwork.CurrentRoom.PlayerCount}";
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
        string message = $"{newPlayer.NickName} �����˷��䡣��ǰ�����: {PhotonNetwork.CurrentRoom.PlayerCount}";
        Debug.Log(message);

        if (statusText != null)
            statusText.text = message;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (statusText != null)
            statusText.text = $"���ӶϿ�: {cause}";
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        // 1. �������������
        string randomRoomName = "Room_" + Random.Range(0, 10000);

        // 2. ��������ѡ��
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        // 3. ���÷�����Զ������ԣ���ͼ���ͣ�
        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        // ��ѡ��ͼ���ͣ�outdoor(����) �� school(ѧУ)
        string mapType = "school"; // Ĭ��ΪѧУ��ͼ

        // �����Զ��巿�����Թ�ϣ��
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL } };

        // 4. ���÷�������
        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        // 5. ���������뷿��
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        Debug.Log($"���ڴ�������: {randomRoomName} (��ͼ: {mapType})");
    }
    #endregion

}
