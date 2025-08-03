using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        // ����Ĭ�ϼ��������
        DrawDefaultInspector();

        // �����Ϣ��ʾ��
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon Servers.", MessageType.Info);

        // ��ȡ��ǰ����Ŀ�����LoginManagerʵ����
        LoginManager loginManager = (LoginManager)target;

        // ���"Connect Anonymously"��ť
        if (GUILayout.Button("Connect Anonymously"))
        {
            // ����LoginManager���������ӷ���
            loginManager.ConnectAnonymously();
        }
    }
}

