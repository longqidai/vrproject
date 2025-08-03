using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        // 绘制默认检查器界面
        DrawDefaultInspector();

        // 添加信息提示框
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon Servers.", MessageType.Info);

        // 获取当前检查的目标对象（LoginManager实例）
        LoginManager loginManager = (LoginManager)target;

        // 添加"Connect Anonymously"按钮
        if (GUILayout.Button("Connect Anonymously"))
        {
            // 调用LoginManager的匿名连接方法
            loginManager.ConnectAnonymously();
        }
    }
}

