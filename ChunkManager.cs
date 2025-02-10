using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    // Adjust the radius (in chunks) around the player that you wish to load.
    public int loadRadius = 2;
    // Reference to your chunk prefab (which might contain a Tilemap or custom tile rendering logic).
    public GameObject chunkPrefab;
    // Reference to the player’s transform.
    public Transform player;
    
    // Track loaded chunks by their chunk coordinates.
    private Dictionary<Vector2Int, GameObject> loadedChunks = new Dictionary<Vector2Int, GameObject>();

    // Set your chunk size (should match your Chunk.width/height)
    public int chunkSize = 100;
    public int seed = 42; // Use any seed for procedural variation.

    void Update()
    {
        Vector2Int currentChunkCoord = new Vector2Int(
            Mathf.FloorToInt(player.position.x / chunkSize),
            Mathf.FloorToInt(player.position.y / chunkSize)
        );

        // Load all chunks in a square around the player.
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(currentChunkCoord.x + x, currentChunkCoord.y + y);
                if (!loadedChunks.ContainsKey(chunkCoord))
                {
                    LoadChunk(chunkCoord);
                }
            }
        }

        // (Optional) Unload chunks that are far away to free memory.
        // You can iterate through loadedChunks and remove any that fall outside the loadRadius.
    }

    void LoadChunk(Vector2Int chunkCoord)
    {
        // Calculate the world position of this chunk.
        Vector3 chunkPosition = new Vector3(chunkCoord.x * chunkSize, chunkCoord.y * chunkSize, 0);
        GameObject newChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity, transform);

        // Optionally pass parameters (like seed and chunkCoord) to your chunk prefab’s script.
        ChunkRenderer chunkRenderer = newChunk.GetComponent<ChunkRenderer>();
        if (chunkRenderer != null)
        {
            chunkRenderer.Init(seed, chunkCoord);
        }
        loadedChunks.Add(chunkCoord, newChunk);
    }
}
