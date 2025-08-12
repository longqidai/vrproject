using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager))] // 指定此编辑器针对RoomManager类型
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        // 1. 绘制默认的Inspector界面
        DrawDefaultInspector();

        // 2. 添加信息提示框
        EditorGUILayout.HelpBox(
            "This script is responsible for creating and joining rooms",
            MessageType.Info);

        // 3. 获取当前检查的RoomManager实例
        RoomManager roomManager = (RoomManager)target;

       
        if (GUILayout.Button("Join school Room"))
        {
            // 调用RoomManager的加入随机房间方法
            roomManager.OnEnterButtonClicked_School();
        }
        if (GUILayout.Button("Join outdoor Room"))
        {
            // 调用RoomManager的加入随机房间方法
            roomManager.OnEnterButtonClicked_Outdoor();
        }
    }
}
