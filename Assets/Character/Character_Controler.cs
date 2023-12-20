using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    public float defenseValue;
    public float attackDamage = 1f;

    public float attackCooldown = 1f;

    public bool canAttack = true;




    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float computeDamage(float damage)
    {
        return damage-this.defenseValue;
    }

    public void takeDamage(float damage)
    {
        damage = computeDamage(damage);
        if (damage <= 0) return;
        if (currentHealth - damage < 0) { 
            currentHealth = 0;
        }
        currentHealth -= damage;
    }

    public void Attack(GameObject target)
    {
        target.GetComponent<Character_Controler>().takeDamage(this.attackDamage);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(this.attackCooldown);
        canAttack = true;
    }
}
