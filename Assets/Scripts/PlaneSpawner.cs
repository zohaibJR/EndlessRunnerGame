using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Assign obstacle prefabs in Inspector
    public int obstaclesPerTile = 2;     // Number of obstacles per tile
    public float obstacleY = 0.5f;       // Y position to place obstacles on plane
    private float[] laneX = new float[] { -2f, 0f, 2f }; // 3 lanes: left, mid, right

    public Transform player;              // Reference to the player
    public GameObject planePrefab;        // Your plane prefab
    public float planeLength = 10f;       // Length of each tile
    public int bufferTiles = 3;           // Number of tiles ahead to generate

    private int lastPlayerTileIndex = -1; // Player's current tile
    private Dictionary<int, GameObject> spawnedTiles = new Dictionary<int, GameObject>();

    void Start()
    {
        // Initial spawn (n = 0)
        UpdateTiles(0);
    }

    void Update()
    {
        int currentTileIndex = Mathf.FloorToInt(player.position.z / planeLength);

        if (currentTileIndex != lastPlayerTileIndex)
        {
            lastPlayerTileIndex = currentTileIndex;
            UpdateTiles(currentTileIndex);
        }
    }

    void UpdateTiles(int playerTile)
    {
        // Spawn tiles from n to n + 3
        for (int i = playerTile; i <= playerTile + bufferTiles; i++)
        {
            if (!spawnedTiles.ContainsKey(i))
            {
                Vector3 spawnPosition = new Vector3(0, 0, i * planeLength);
                GameObject tile = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
                spawnedTiles.Add(i, tile);

                // ✅ Spawn obstacles on this tile
                SpawnObstaclesOnTile(i * planeLength);
            }
        }

        // Deactivate tiles from < n - 2
        List<int> tilesToRemove = new List<int>();
        foreach (var key in spawnedTiles.Keys)
        {
            if (key < playerTile - 2)
            {
                Destroy(spawnedTiles[key]);
                tilesToRemove.Add(key);
            }
        }

        // Clean up removed tiles from dictionary
        foreach (int key in tilesToRemove)
        {
            spawnedTiles.Remove(key);
        }
    }


    void SpawnObstaclesOnTile(float tileZ)
    {
        for (int i = 0; i < obstaclesPerTile; i++)
        {
            // Pick a random lane (X position)
            float x = laneX[Random.Range(0, laneX.Length)];

            // Pick a random Z offset within the tile
            float zOffset = Random.Range(2f, planeLength - 2f); // not at edge

            // Final spawn position
            Vector3 spawnPos = new Vector3(x, obstacleY, tileZ + zOffset);

            // Pick a random obstacle prefab
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Instantiate the obstacle
            Instantiate(obstacle, spawnPos, Quaternion.identity);
        }
    }

}
