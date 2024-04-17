using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Attack attack;
    public GameObject

    private void Start()
    {
        attack.dashPower = 10f;
        attack.dashUpwardForce = 10f;
    }
    
    private void Dash ()
    {
        attack.Dash();
    }

    private IEnumerator Wait()
    {
        if 
        yield return new WaitForSeconds(1f);
    }
}
