using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis_Attack : MonoBehaviour
{
    private bool playerInRange = false;
    private Character_Controler controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<Character_Controler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && controller.canAttack)
        {
            controller.Attack(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
