using UnityEngine;

[System.Serializable]
public class ChunkData
{
    public bool isDestroyed;
    public GameObject chunkObject;

    public ChunkData(GameObject obj)
    {
        isDestroyed = false;
        chunkObject = obj;
    }
}
