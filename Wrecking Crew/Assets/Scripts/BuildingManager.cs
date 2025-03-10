using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Building Dimensions")]
    public int width = 36;
    public int height = 36;
    public int layers = 1;

    [Header("Chunk Setup")]
    public GameObject chunk;  
    public Sprite[] chunkSprites; 
    private ChunkData[,,] building;

    void Start()
    {
        building = new ChunkData[width, height, layers];

        
        for (int z = 0; z < layers; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 position = new Vector3(x, y, 0);
                    GameObject newChunk = Instantiate(chunk, position, Quaternion.identity);

                    newChunk.transform.SetParent(this.transform);

                    int spriteIndex = y * width + x;

                    SpriteRenderer sr = newChunk.GetComponent<SpriteRenderer>();
                    sr.sprite = chunkSprites[spriteIndex];

                    building[x, y, z] = new ChunkData(newChunk);
                }
            }
        }
    }


    public void DestroyChunkAt(int x, int y, int z, float wreckingBallSpeed)
    {
        if (x < 0 || x >= width ||
            y < 0 || y >= height ||
            z < 0 || z >= layers)
        {
            return;
        }

        ChunkData chunk = building[x, y, z];
        if (chunk == null || chunk.isDestroyed) return;

        bool canDestroy = false;

        float speedThreshold = 10f; 

        if (z == layers - 1)
        {
            canDestroy = true;
        }
        else
        {
            bool aboveIsDestroyed = building[x, y, z + 1].isDestroyed;
            if (aboveIsDestroyed || wreckingBallSpeed >= speedThreshold)
            {
                canDestroy = true;
            }
        }

        if (canDestroy)
        {
            chunk.isDestroyed = true;
            if (chunk.chunkObject != null)
            {
                Destroy(chunk.chunkObject);
            }
        }
    }
}
