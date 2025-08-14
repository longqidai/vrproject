using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristButtonController : MonoBehaviour
{
    public GameObject menuPanel; // ����"Hand Menu ScrollView"

    void Start()
    {
        // 1. ��ȡ�������
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();

        // 2. ��ѡ���¼�����ť���£�
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // 3. �л��˵���ʾ״̬
        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive); // ��ת��ǰ״̬

            Debug.Log($"�˵�״̬: {!isActive}");
        }
    }
}
