using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    // Array of gem prefabs to randomly pick from
    public GameObject[] gemsPrefab;

    // How many gems to spawn
    public int count = 20;

    // Reference to the active terrain in the scene
    private Terrain m_Terrain;

    void Start()
    {
        // Grab the active terrain
        m_Terrain = Terrain.activeTerrain;

        // Spawn the gems
        Spawn();
    }

    void Spawn()
    {
        // Cache terrain data and position for faster access
        TerrainData data = m_Terrain.terrainData;
        Vector3 terrainPos = m_Terrain.transform.position;

        for (int i = 0; i < count; i++)
        {
            // Random point inside terrain's local XZ bounds
            float x = Random.Range(0f, data.size.x);
            float z = Random.Range(0f, data.size.z);

            // Get ground height at that point
            float y = m_Terrain.SampleHeight(new Vector3(x, 0f, z) + terrainPos);

            // Convert to world position
            Vector3 spawnPos = new Vector3(x, y, z) + terrainPos;

            // Pick a random gem prefab and spawn it at the calculated position
            Instantiate(gemsPrefab[Random.Range(0, gemsPrefab.Length)], spawnPos, Quaternion.identity);
        }
    }
}