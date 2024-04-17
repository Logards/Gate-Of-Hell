using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{

    public int id = 0;
    private (int x, int y) _zonePos;
    public GameObject chunk;
    public (int x, int y) zonePos { 
        get
        {
            return _zonePos;
        }
        set
        {
            _zonePos = value; 
            chunk.GetComponent<Chunk>().zonePos = value;
        }
    }
}
