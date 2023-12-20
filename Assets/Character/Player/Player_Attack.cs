using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform orientation;
    private Character_Controler controller;
    private Rigidbody rb;

    public float dashPower = 16f;
    public float dashUpwardForce = 16f;
    public float dashTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponentInParent<Character_Controler>();

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 forceToApply = orientation.forward * dashPower + orientation.up * dashUpwardForce; 
            rb.AddForce(forceToApply,ForceMode.Impulse);
            Invoke(nameof(ResetDash), dashTime);
            Debug.Log("Attack");
            /* If enemis in reach
            controller.Attack(GameObject.FindGameObjectWithTag("Player"));
            */
            
        }
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
    }
}
