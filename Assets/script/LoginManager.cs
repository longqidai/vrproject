using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // ���UI�����ռ�
using Photon.Realtime;
using System.Threading;
public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputField;
    public Button loginButton;
    public Button anonymousButton; // �������������ť����
    public TextMeshProUGUI statusText;
    private bool _isConnecting;

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        _isConnecting = true;
        // �޸���Сд���� ��
        PhotonNetwork.NickName = "Guest_" + Random.Range(1000, 9999);
        // �޸�������ƴд ��
        PhotonNetwork.ConnectUsingSettings();

        // ���UI���� ��
        statusText.text = "Connecting anonymously...";
        statusText.color = Color.yellow;
        anonymousButton.interactable = false; // ���ð�ť
    }

    public void ConnectWithName()
    {
        if (string.IsNullOrEmpty(PlayerName_InputField.text))
        {
            Debug.LogError("Player name is empty!");
            statusText.text = "Name cannot be empty!";
            statusText.color = Color.red;
            return;
        }

        _isConnecting = true;
        // �޸����Դ�Сд ��
        PhotonNetwork.NickName = PlayerName_InputField.text;
        // �޸�������ƴд ��
        PhotonNetwork.ConnectUsingSettings();

        statusText.text = "Login successful as user:"+ PhotonNetwork.NickName;
        Thread.Sleep(1000);
        statusText.color = Color.yellow;
        loginButton.interactable = false; // ���ð�ť
    }
    #endregion

    #region Photon Callback Methods
    public override void OnConnectedToMaster()
    {
        if (!_isConnecting) return;

        Debug.Log($"Connected to Master Server as {PhotonNetwork.NickName}");
        statusText.text = "Connected! Joining lobby...";
        statusText.color = Color.green;

        // �������������OnJoinedLobby��
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Meetingscene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // ����ʧ�ָܻ�UI
        _isConnecting = false;
        statusText.text = $"Connection failed: {cause}";
        statusText.color = Color.red;
        loginButton.interactable = true;
        if (anonymousButton != null) anonymousButton.interactable = true;
    }
    #endregion
}
