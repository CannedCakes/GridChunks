using UnityEngine;

public class Chunk
{
    public int width = 100;
    public int height = 100;
    // A simple representation: 0 = empty/floor, 1 = wall
    public int[,] tiles;

    // A seed can be used to offset noise coordinates.
    public Chunk(int seed, Vector2Int chunkCoord)
    {
        tiles = new int[width, height];
        GenerateChunk(seed, chunkCoord);
    }

    void GenerateChunk(int seed, Vector2Int chunkCoord)
    {
        // Adjust this scale value to change the “roughness”
        float scale = 0.1f;
        // The chunk's world offset ensures that noise values are continuous across chunks.
        int offsetX = chunkCoord.x * width;
        int offsetY = chunkCoord.y * height;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise((offsetX + x + seed) * scale,
                                                     (offsetY + y + seed) * scale);
                // Adjust the threshold to change cave density.
                tiles[x, y] = (noiseValue > 0.5f) ? 1 : 0;
            }
        }
    }
}
