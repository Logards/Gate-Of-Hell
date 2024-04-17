using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Attack attack;

    void Start()
    {
        attack.dashPower = 10f;
        attack.dashUpwardForce = 10f;
    }
    
    private void Dash ()
    {
        attack.Dash();
    }
}
