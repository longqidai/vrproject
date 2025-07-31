using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("出生点设置")]
    public List<Transform> spawnPoints = new List<Transform>(); // 多个出生点位置
    [Tooltip("是否在游戏开始时自动定位场景中的出生点")]
    public bool autoFindSpawnPoints = true;

    [Header("玩家设置")]
    public GameObject playerPrefab; // 玩家预制体
    [Tooltip("是否在管理器启动时自动生成玩家")]
    public bool spawnPlayerOnStart = true;

    void Awake()
    {
        // 单例模式确保全局只有一个管理器
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 自动查找场景中的出生点
        if (autoFindSpawnPoints)
        {
            FindSpawnPointsInScene();
        }
    }

    void Start()
    {
        if (spawnPlayerOnStart && playerPrefab != null && spawnPoints.Count > 0)
        {
            SpawnPlayer();
        }
    }

    // 自动查找场景中标记为"SpawnPoint"的对象
    private void FindSpawnPointsInScene()
    {
        spawnPoints.Clear();
        GameObject[] foundPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (foundPoints.Length > 0)
        {
            foreach (GameObject point in foundPoints)
            {
                spawnPoints.Add(point.transform);
            }
            Debug.Log($"找到 {spawnPoints.Count} 个出生点");
        }
        else
        {
            Debug.LogWarning("未找到标记为'SpawnPoint'的对象，将使用默认位置");
            CreateDefaultSpawnPoint();
        }
    }

    // 创建默认出生点
    private void CreateDefaultSpawnPoint()
    {
        GameObject defaultPoint = new GameObject("DefaultSpawnPoint");
        defaultPoint.transform.position = Vector3.zero;
        defaultPoint.tag = "SpawnPoint";
        spawnPoints.Add(defaultPoint.transform);
    }

    // 生成玩家
    public GameObject SpawnPlayer()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("没有可用的出生点!");
            return null;
        }

        // 随机选择一个出生点
        Transform spawnPoint = GetRandomSpawnPoint();

        // 实例化玩家
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"玩家已生成在位置: {spawnPoint.position}");

        return player;
    }

    // 获取随机出生点
    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0) return null;
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    // 添加新出生点（可在运行时调用）
    public void AddSpawnPoint(Transform newPoint)
    {
        if (!spawnPoints.Contains(newPoint))
        {
            spawnPoints.Add(newPoint);
            Debug.Log($"已添加出生点: {newPoint.name}");
        }
    }
}
