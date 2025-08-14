using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class LocalPlayerUIManager : MonoBehaviour
{
    // ������������
    public string meetingSceneName = "meetingscene";

    // ��ť���µĻص�
    public void OnPressButton()
    {
        Debug.Log($"�����л�������: {meetingSceneName}");

        // ���ü��س����ĺ���

        LoadMeetingScene();
    }

    // �������ط���
    private void LoadMeetingScene()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(meetingSceneName);
    }
}
