using MySqlConnector;
using System;
using UnityEngine;
using TMPro;
public class DataConnector : MonoBehaviour
{
    private string connectionString = "Server=172.16.0.57;Database=user info;User ID=root;Password=123456;";
    private MySqlConnection connection;
    public TextMeshPro statustext;
    // 1Awake �ڽű���ʼ��ʱ���ã��������ڱ༭ģʽ���Զ��������ݿ�
    private void Awake()
    {
        // ȷ����������ģʽ������
        if (Application.isPlaying)
        {
            TryConnect();
        }
    }

    // �ڳ�������ʱ�������ݿ�����
    private void TryConnect()
    {
        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();

            if (connection.State == System.Data.ConnectionState.Open)
            {
                Debug.Log("Database connected successfully.");
            }
            else
            {
                Debug.LogError("Database connection failed: Could not open connection.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Database connection error: {ex.Message}");
        }
    }
    public bool Login(string username, string password)
    {
        string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password;";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        MySqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            Debug.Log("Login successful for username: " + username);
            statustext.text = "login successful";
            statustext.color = Color.green;
            reader.Close();
            return true;
        }
        else
        {
            Debug.Log("Invalid username or password.");
            statustext.text = "login failed";
            statustext.color = Color.red;
            reader.Close();
            return false;
        }
    }

    // ��ӹر����ӵķ���
    private void OnDestroy()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Database connection closed.");
        }
    }
}
