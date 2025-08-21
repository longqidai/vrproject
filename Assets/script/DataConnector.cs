using MySqlConnector;
using System;
using UnityEngine;
using TMPro;
public class DataConnector : MonoBehaviour
{
    private string connectionString = "Server=172.16.0.57;Database=user info;User ID=root;Password=123456;";
    private MySqlConnection connection;
    // 1Awake 在脚本初始化时调用，但不会在编辑模式下自动连接数据库
    private void Awake()
    {
        // 确保仅在运行模式下连接
        if (Application.isPlaying)
        {
            TryConnect();
        }
    }

    // 在程序运行时建立数据库连接
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
    public void InsertUser(string username, string password)
    {
        string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password);";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
        int rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Debug.Log("User inserted successfully.");
        }
        else
        {
            Debug.LogError("Failed to insert user.");
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
            
            reader.Close();
            return true;
        }
        else
        {
            Debug.Log("Invalid username or password.");
            
            reader.Close();
            return false;
        }
    }

    // 添加关闭连接的方法
    private void OnDestroy()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Database connection closed.");
        }
    }
}
