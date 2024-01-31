using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneData
{
    public int ZoneID;
    public (int x, int y) ZonePosition;
    public bool isLoad;
    public ZoneData(int zoneId, (int x, int y) zonePosition) {
        ZoneID = zoneId;
        ZonePosition = zonePosition;
        isLoad = false;
    }
}
