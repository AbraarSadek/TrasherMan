using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("How often trash spawns (in seconds)")]
    public float spawnInterval = 3f;          // Time between spawns

    [Tooltip("Maximum number of trash items allowed at once (0 = unlimited)")]
    public int maxTrashCount = 8;

    [Header("Spawn Objects")]
    [Tooltip("Drag your 5 temporary trash prefabs here")]
    public GameObject[] trashPrefabs;         // Array so you can assign all 5 types

    [Header("Spawn Area")]
    [Tooltip("Spawn area size (width, height, depth)")]
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);

    [Tooltip("Height offset above the spawner position")]
    public float spawnHeight = 2f;

    private float spawnTimer = 0f;
    private int currentTrashCount = 0;

    void Start()
    {
        // Optional: Spawn one immediately when game starts
        SpawnTrash();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            if (currentTrashCount < maxTrashCount || maxTrashCount == 0)
            {
                SpawnTrash();
            }
            spawnTimer = 0f;        // Reset timer
        }
    }

    private void SpawnTrash()
    {
        if (trashPrefabs == null || trashPrefabs.Length == 0)
        {
            Debug.LogWarning("No trash prefabs assigned to TrashSpawner!");
            return;
        }

        // Randomly pick one of the trash prefabs
        int randomIndex = Random.Range(0, trashPrefabs.Length);
        GameObject selectedPrefab = trashPrefabs[randomIndex];

        if (selectedPrefab == null)
        {
            Debug.LogWarning("One of the trash prefabs is missing!");
            return;
        }

        // Calculate random spawn position within the area
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            spawnHeight,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );

        Vector3 spawnPosition = transform.position + randomOffset;

        // Spawn the object
        GameObject newTrash = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Optional: Parent to spawner for better hierarchy organization
        newTrash.transform.SetParent(transform);

        // Increment count
        currentTrashCount++;

        // Subscribe to destruction event so we can decrease count when trash is thrown into bin
        TrashDestructionTracker tracker = newTrash.AddComponent<TrashDestructionTracker>();
        tracker.onDestroyed += OnTrashDestroyed;
    }

    // Called when trash is destroyed (by bin or other means)
    private void OnTrashDestroyed()
    {
        if (currentTrashCount > 0)
            currentTrashCount--;
    }

    // Visual help in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position + new Vector3(0, spawnHeight, 0);
        Gizmos.DrawWireCube(center, spawnAreaSize);
    }
}