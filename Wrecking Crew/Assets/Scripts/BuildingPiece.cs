using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPiece : MonoBehaviour
{
    public delegate void ChunkCollision(Collision collision);

    public event ChunkCollision OnChunkCollisionEnter;

    private void OnCollisionEnter(Collision collision)
    {
        OnChunkCollisionEnter(collision);
    }

}
