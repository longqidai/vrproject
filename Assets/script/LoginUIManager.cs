using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectOptionsPanelGameObject;  // ����ѡ�����
    public GameObject ConnectWithNamePanelGameObject; // �û����������

    #region Unity Methods
    // ��ʼ����������Ϸ����ʱ�Զ����ã�
    void Start()
    {
        // ��������ѡ����壬�����û����������
        ConnectOptionsPanelGameObject.SetActive(true);
        ConnectWithNamePanelGameObject.SetActive(false);
    }
    #endregion
}