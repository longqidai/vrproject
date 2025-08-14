using UnityEngine;
using Photon.Pun;

public class Spawn_manager : MonoBehaviour
{
    [SerializeField]
    GameObject GenericVRPlayerPrefab;  // ���Ԥ���壨��Inspector�з��䣩

    public Vector3 spawnPosition;     // ����λ������

    void Start()
    {
        // ȷ���������Ӿ��������������
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // �޸�Instantiate���������﷨������˵�ź�С���ţ�
            GameObject player = PhotonNetwork.Instantiate(
                GenericVRPlayerPrefab.name,
                spawnPosition,
                Quaternion.identity
            );
            Debug.Log($"�����������λ��: {spawnPosition}");
        }
    }

    void Update()
    {
        // �����ÿ֡���µ��߼�
    }
}
