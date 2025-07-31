using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [Header("����������")]
    public List<Transform> spawnPoints = new List<Transform>(); // ���������λ��
    [Tooltip("�Ƿ�����Ϸ��ʼʱ�Զ���λ�����еĳ�����")]
    public bool autoFindSpawnPoints = true;

    [Header("�������")]
    public GameObject playerPrefab; // ���Ԥ����
    [Tooltip("�Ƿ��ڹ���������ʱ�Զ��������")]
    public bool spawnPlayerOnStart = true;

    void Awake()
    {
        // ����ģʽȷ��ȫ��ֻ��һ��������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �糡������
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // �Զ����ҳ����еĳ�����
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

    // �Զ����ҳ����б��Ϊ"SpawnPoint"�Ķ���
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
            Debug.Log($"�ҵ� {spawnPoints.Count} ��������");
        }
        else
        {
            Debug.LogWarning("δ�ҵ����Ϊ'SpawnPoint'�Ķ��󣬽�ʹ��Ĭ��λ��");
            CreateDefaultSpawnPoint();
        }
    }

    // ����Ĭ�ϳ�����
    private void CreateDefaultSpawnPoint()
    {
        GameObject defaultPoint = new GameObject("DefaultSpawnPoint");
        defaultPoint.transform.position = Vector3.zero;
        defaultPoint.tag = "SpawnPoint";
        spawnPoints.Add(defaultPoint.transform);
    }

    // �������
    public GameObject SpawnPlayer()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("û�п��õĳ�����!");
            return null;
        }

        // ���ѡ��һ��������
        Transform spawnPoint = GetRandomSpawnPoint();

        // ʵ�������
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"�����������λ��: {spawnPoint.position}");

        return player;
    }

    // ��ȡ���������
    public Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0) return null;
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    // ����³����㣨��������ʱ���ã�
    public void AddSpawnPoint(Transform newPoint)
    {
        if (!spawnPoints.Contains(newPoint))
        {
            spawnPoints.Add(newPoint);
            Debug.Log($"����ӳ�����: {newPoint.name}");
        }
    }
}
