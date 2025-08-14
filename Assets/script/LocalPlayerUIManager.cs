using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class LocalPlayerUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GoHome_Button; // ������ҳ��ť

    void Start()
    {
        GoHome_Button.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }

    void Update()
    {
        // ���������߼�
    }
}
