using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // 添加UI命名空间
using Photon.Realtime;
using System.Threading;
public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputField;
    public Button loginButton;
    public Button anonymousButton; // 建议添加匿名按钮引用
    public TextMeshProUGUI statusText;
    private bool _isConnecting;

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        _isConnecting = true;
        // 修复大小写错误 ↓
        PhotonNetwork.NickName = "Guest_" + Random.Range(1000, 9999);
        // 修复方法名拼写 ↓
        PhotonNetwork.ConnectUsingSettings();

        // 添加UI反馈 ↓
        statusText.text = "Connecting anonymously...";
        statusText.color = Color.yellow;
        anonymousButton.interactable = false; // 禁用按钮
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
        // 修复属性大小写 ↓
        PhotonNetwork.NickName = PlayerName_InputField.text;
        // 修复方法名拼写 ↓
        PhotonNetwork.ConnectUsingSettings();

        statusText.text = "Login successful as user:"+ PhotonNetwork.NickName;
        Thread.Sleep(1000);
        statusText.color = Color.yellow;
        loginButton.interactable = false; // 禁用按钮
    }
    #endregion

    #region Photon Callback Methods
    public override void OnConnectedToMaster()
    {
        if (!_isConnecting) return;

        Debug.Log($"Connected to Master Server as {PhotonNetwork.NickName}");
        statusText.text = "Connected! Joining lobby...";
        statusText.color = Color.green;

        // 加入大厅（触发OnJoinedLobby）
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Meetingscene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 连接失败恢复UI
        _isConnecting = false;
        statusText.text = $"Connection failed: {cause}";
        statusText.color = Color.red;
        loginButton.interactable = true;
        if (anonymousButton != null) anonymousButton.interactable = true;
    }
    #endregion
}
