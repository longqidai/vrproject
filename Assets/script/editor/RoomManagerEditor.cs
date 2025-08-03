using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomManager))] // ָ���˱༭�����RoomManager����
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        // 1. ����Ĭ�ϵ�Inspector����
        DrawDefaultInspector();

        // 2. �����Ϣ��ʾ��
        EditorGUILayout.HelpBox(
            "This script is responsible for creating and joining rooms",
            MessageType.Info);

        // 3. ��ȡ��ǰ����RoomManagerʵ��
        RoomManager roomManager = (RoomManager)target;

        // 4. ���"Join Random Room"��ť
        if (GUILayout.Button("Join Random Room"))
        {
            // ����RoomManager�ļ���������䷽��
            roomManager.JoinRandomRoom();
        }
    }
}
