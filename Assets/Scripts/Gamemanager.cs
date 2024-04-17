using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    private static Gamemanager _instance;
    public static List<ZoneData> MapList = new List<ZoneData>();
    private static Dictionary<(int x, int y), GameObject> InstancedMap = new Dictionary<(int x, int y), GameObject>();
    public GameObject zone;
    public static (int x, int y) playerPosition;
    // Start is called before the first frame update
    void Start()
    {

       DungeonGenerator dg = gameObject.GetComponent<DungeonGenerator>();
        dg.GenMap();
        UpdateMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        _instance = this;
    }

    public static void UpdateMap()
    {
        foreach (var item in MapList)
        {
            bool xPos = (new int[] { playerPosition.x + 1, playerPosition.x - 1, playerPosition.x }.Contains(item.ZonePosition.x));
            bool yPos = (new int[] { playerPosition.y + 1, playerPosition.y - 1, playerPosition.y }.Contains(item.ZonePosition.y));


            if ((xPos && yPos))
            {
                if (!item.isLoad)
                {
                    Vector3 vector3 = new Vector3(item.ZonePosition.x * 2, 0, item.ZonePosition.y * 2);
                    GameObject z = Instantiate(_instance.zone, vector3, _instance.transform.rotation);
                    z.GetComponent<Zone>().id = item.ZoneID;
                    z.GetComponent<Zone>().zonePos = item.ZonePosition;
                    InstancedMap.Add(item.ZonePosition, z);
                    item.isLoad = true;
                }

            }
            else if (InstancedMap.Keys.Contains(item.ZonePosition))
            {
                if (InstancedMap.TryGetValue(item.ZonePosition, out GameObject i))
                {
                    Destroy(i);
                    item.isLoad = false;
                    InstancedMap.Remove(item.ZonePosition);
                }
            }

        }
    }
}
