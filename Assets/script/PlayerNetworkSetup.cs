using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 这个类处理玩家网络设置，区分本地玩家和远程玩家
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    // 指向本地玩家的XR装备游戏对象
    [Header("XR装备设置")]
    [Tooltip("本地玩家的XR装备游戏对象")]
    public GameObject LocalXRRigGameObject;


    public GameObject AvatarHeadGameobject;
    public GameObject AvatarBodyGameobject;
    // 在游戏开始时初始化玩家设置
    void Start()
    {
        // 检查这个玩家实例是否由当前客户端控制（是否是本地玩家）
        if (photonView.IsMine)
        {
            // 这是本地玩家
            Debug.Log("激活本地玩家的XR设备");

            // 启用本地玩家的XR装备（如头显、手柄等）
            LocalXRRigGameObject.SetActive(true);


            SetLayerRecursively(AvatarBodyGameobject, 10);
            SetLayerRecursively(AvatarHeadGameobject, 9);
        }
        else
        {
            // 这是其他玩家的实例
            Debug.Log("禁用远程玩家的XR设备");

            // 禁用非本地玩家的XR装备
            LocalXRRigGameObject.SetActive(false);


            SetLayerRecursively(AvatarBodyGameobject, 0);
            SetLayerRecursively(AvatarHeadGameobject, 0);
        }
    }

    // 每帧更新（可以用于处理玩家输入或其他实时操作）
    void Update()
    {
        // 如果这是本地玩家，处理输入和控制
        if (photonView.IsMine)
        {
            // 在这里添加本地玩家控制的逻辑
            // 例如：移动、交互等
        }
    }
    // 添加递归设置层级的方法
    private void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj == null) return;

        obj.layer = layer;

        // 递归设置子对象层级
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            SetLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }

}