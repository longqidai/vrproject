using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


// ����ദ������������ã����ֱ�����Һ�Զ�����
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    // ָ�򱾵���ҵ�XRװ����Ϸ����
    [Header("XRװ������")]
    [Tooltip("������ҵ�XRװ����Ϸ����")]
    public GameObject LocalXRRigGameObject;

    // ָ���������ģ�͵Ŀ��ӻ�����
    [Header("����Ӿ�")]
    [Tooltip("��ҵ��Ӿ���ʾ")]
    public GameObject PlayerVisuals;
    public GameObject AvatarHeadGameobject;
    public GameObject AvatarBodyGameobject;
    // ����Ϸ��ʼʱ��ʼ���������
    void Start()
    {
        // ���������ʵ���Ƿ��ɵ�ǰ�ͻ��˿��ƣ��Ƿ��Ǳ�����ң�
        if (photonView.IsMine)
        {
            // ���Ǳ������
            Debug.Log("�������ҵ�XR�豸");

            // ���ñ�����ҵ�XRװ������ͷ�ԡ��ֱ��ȣ�
            LocalXRRigGameObject.SetActive(true);

            // ������ҵ��Ӿ�ģ�ͣ���Ϊ������Ҳ���Ҫ�����Լ�������
            PlayerVisuals.SetActive(false);

            SetLayerRecursively(AvatarBodyGameobject, 7);
            SetLayerRecursively(AvatarHeadGameobject, 6);

           
        }
        else
        {
            // ����������ҵ�ʵ��
            Debug.Log("����Զ����ҵ�XR�豸");

            // ���÷Ǳ�����ҵ�XRװ��
            LocalXRRigGameObject.SetActive(false);

            // ������ҵ��Ӿ�ģ�ͣ��Ա㿴��������ҵĽ�ɫ
            PlayerVisuals.SetActive(true);
            SetLayerRecursively(AvatarBodyGameobject, 0);
            SetLayerRecursively(AvatarHeadGameobject, 0);
        }
    }

    // ÿ֡���£��������ڴ���������������ʵʱ������
    void Update()
    {
        // ������Ǳ�����ң���������Ϳ���
        if (photonView.IsMine)
        {
            // ���������ӱ�����ҿ��Ƶ��߼�
            // ���磺�ƶ���������
        }
    }
    // ���ӵݹ����ò㼶�ķ���
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj == null) return;

        obj.layer = layer;

        // �ݹ������Ӷ���㼶
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }

}
