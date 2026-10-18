using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatchSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject firstPatchPrefab;   // ✅ Drag your first fixed patch here
    public GameObject[] patchPrefabs;     // ✅ Pool of random patches
    public int patchesAhead = 3;
    public float patchLength = 70.64f;

    private Queue<GameObject> activePatches = new Queue<GameObject>();
    private float spawnZ = 0;
    private int playerPassedCount = 0;
    private bool firstPatchSpawned = false; // ✅ To control first patch logic

    void Start()
    {
        for (int i = 0; i < patchesAhead; i++)
        {
            SpawnPatch();
        }
    }

    void Update()
    {
        if (player.position.z > (playerPassedCount + 1) * patchLength)
        {
            playerPassedCount++;
            StartCoroutine(RemoveOldPatch());
            SpawnPatch();
        }
    }

    void SpawnPatch()
    {
        GameObject prefabToSpawn;

        if (!firstPatchSpawned)
        {
            // ✅ First patch is always fixed
            prefabToSpawn = firstPatchPrefab;
            firstPatchSpawned = true;
        }
        else
        {
            // ✅ After first → pick random patch
            int randomIndex = Random.Range(0, patchPrefabs.Length);
            prefabToSpawn = patchPrefabs[randomIndex];
        }

        // ✅ Always spawn at X = 3.70
        Vector3 spawnPos = new Vector3(3.70f, 0f, spawnZ);

        GameObject newPatch = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        activePatches.Enqueue(newPatch);

        spawnZ += patchLength;
    }

    IEnumerator RemoveOldPatch()
    {
        yield return new WaitForSeconds(1.5f);

        if (activePatches.Count > patchesAhead)
        {
            GameObject oldPatch = activePatches.Dequeue();
            Destroy(oldPatch);
        }
    }
}
