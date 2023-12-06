using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis_Attack : MonoBehaviour
{
    private bool canAttack = true;
    private bool playerInRange = false;
    public float attackCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canAttack)
        {
            Attack(GameObject.Find("Player"));
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

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(this.attackCooldown);
        canAttack = true;
    }

    void Attack(GameObject target)
    {
        Debug.Log("Attack");
        //target.GetComponent<PlayerController>().takeDamage();
        StartCoroutine(AttackCooldown());
    }
}
