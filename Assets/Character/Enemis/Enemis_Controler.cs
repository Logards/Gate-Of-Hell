using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemis_Controler : Character_Controler
{
    public override void Die()
    {
        Destroy(gameObject);
    }
}
