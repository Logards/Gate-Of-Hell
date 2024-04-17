using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public (int x, int y) zonePos;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Contains("Player")) return;
        Gamemanager.playerPosition = zonePos;
        Gamemanager.UpdateMap();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.tag.Contains("Player")) return;
        Gamemanager.playerPosition = zonePos;
        Gamemanager.UpdateMap();
    }
}
