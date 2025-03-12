using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheese : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(player.position);
    }
}
