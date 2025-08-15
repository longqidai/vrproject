using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MicPermissionHelper : MonoBehaviour
{
    // ע���� - ԭ�ƻ������Ի������
    // GameObject dialog = null;

    // Start����Ϸ��ʼ��ʱ����
    void Start()
    {
#if PLATFORM_ANDROID
        // ����û��Ƿ�����Ȩ��˷�Ȩ��
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // ������˷�Ȩ��
            Permission.RequestUserPermission(Permission.Microphone);
            
            // ע���� - ԭ�ƻ������Ի������
            // dialog = new GameObject();
        }
#endif

        // ��־�����⵽����˷��豸
        Debug.Log(Microphone.devices.ToString());
    }

    // ��������ȱʧ�ķ���
    void Update()
    {
        // �����ڴ˴����Ȩ��״̬����߼�
    }
}
