using UnityEngine;
using Photon.Pun;

public class Spawn_manager : MonoBehaviour
{
    [SerializeField]
    GameObject GenericVRPlayerPrefab;  // 玩家预制体（在Inspector中分配）

    public Vector3 spawnPosition;     // 生成位置坐标

    void Start()
    {
        // 确保网络连接就绪后再生成玩家
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // 修复Instantiate方法调用语法（添加了点号和小括号）
            GameObject player = PhotonNetwork.Instantiate(
                GenericVRPlayerPrefab.name,
                spawnPosition,
                Quaternion.identity
            );
            Debug.Log($"玩家已生成在位置: {spawnPosition}");
        }
    }

    void Update()
    {
        // 可添加每帧更新的逻辑
    }
}
