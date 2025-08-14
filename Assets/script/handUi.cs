using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristButtonController : MonoBehaviour
{
    public GameObject menuPanel; // 拖入"Hand Menu ScrollView"

    void Start()
    {
        // 1. 获取交互组件
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();

        // 2. 绑定选择事件（按钮按下）
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // 3. 切换菜单显示状态
        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive); // 反转当前状态

            Debug.Log($"菜单状态: {!isActive}");
        }
    }
}
