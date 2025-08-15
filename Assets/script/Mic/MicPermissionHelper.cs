using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class MicPermissionHelper : MonoBehaviour
{
    // 注释行 - 原计划创建对话框对象
    // GameObject dialog = null;

    // Start在游戏初始化时调用
    void Start()
    {
#if PLATFORM_ANDROID
        // 检查用户是否已授权麦克风权限
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // 请求麦克风权限
            Permission.RequestUserPermission(Permission.Microphone);
            
            // 注释行 - 原计划创建对话框对象
            // dialog = new GameObject();
        }
#endif

        // 日志输出检测到的麦克风设备
        Debug.Log(Microphone.devices.ToString());
    }

    // 补充完整缺失的方法
    void Update()
    {
        // 可以在此处添加权限状态检测逻辑
    }
}
