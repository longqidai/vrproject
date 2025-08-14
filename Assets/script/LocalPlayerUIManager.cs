using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class LocalPlayerUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GoHome_Button; // 返回首页按钮

    void Start()
    {
        GoHome_Button.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene);
    }

    void Update()
    {
        // 其他更新逻辑
    }
}
