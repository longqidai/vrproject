using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject currentPanel; // 当前窗口
    public GameObject nextPanel;    // 目标窗口

    public void SwitchPanel()
    {
        currentPanel.SetActive(false); // 关闭当前窗口
        nextPanel.SetActive(true);     // 打开新窗口
    }
}
