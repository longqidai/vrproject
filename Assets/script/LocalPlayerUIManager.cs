using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class LocalPlayerUIManager : MonoBehaviour
{
    // 场景名称配置
    public string meetingSceneName = "meetingscene";

    // 按钮按下的回调
    public void OnPressButton()
    {
        Debug.Log($"正在切换到场景: {meetingSceneName}");

        // 调用加载场景的函数

        LoadMeetingScene();
    }

    // 场景加载方法
    private void LoadMeetingScene()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(meetingSceneName);
    }
}
