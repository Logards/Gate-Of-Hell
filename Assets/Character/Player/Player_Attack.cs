using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform orientation;
    private Character_Controler controller;
    private Rigidbody rb;

    public bool canAttack { get; set; }
    public GameObject target {  get; set; }

    public float dashPower = 16f;
    public float dashUpwardForce = 16f;
    public float dashTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponentInParent<Character_Controler>();
        rb = GetComponentInParent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(aAttack());
            
        }
    }

    private IEnumerator aAttack()
    {

        Debug.Log(this.canAttack);
        if (canAttack)
        {
            try
            {
                controller.Attack(target);
            }
            catch (MissingReferenceException)
            {
                target = null;
                canAttack = false;
            }
        
        }
        else
        {
            this.Dash();
            yield return new WaitForSeconds(dashTime);
            if (canAttack)
            {
                controller.Attack(target);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canAttack && other.CompareTag("Enemis"))
        {
            canAttack = true;
            target = other.GameObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (canAttack && other.GameObject() == target) {
            canAttack = false;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Dash()
    {
        Vector3 forceToApply = orientation.forward * dashPower + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashTime);
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
    }
}
