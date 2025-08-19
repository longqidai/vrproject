using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject currentPanel; // ��ǰ����
    public GameObject nextPanel;    // Ŀ�괰��

    public void SwitchPanel()
    {
        currentPanel.SetActive(false); // �رյ�ǰ����
        nextPanel.SetActive(true);     // ���´���
    }
}
