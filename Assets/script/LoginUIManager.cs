using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectOptionsPanelGameObject;  // 连接选项面板
    public GameObject ConnectWithNamePanelGameObject; // 用户名连接面板

    #region Unity Methods
    // 初始化方法（游戏启动时自动调用）
    void Start()
    {
        // 激活连接选项面板，隐藏用户名连接面板
        ConnectOptionsPanelGameObject.SetActive(true);
        ConnectWithNamePanelGameObject.SetActive(false);
    }
    #endregion
}