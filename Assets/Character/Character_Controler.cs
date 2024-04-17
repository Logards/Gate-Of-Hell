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
    
    public float experience = 0f;
    public int level = 1;
    public float experienceToNextLevel = 10f;




    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel *= 1.5f;
            NextLevel();
        }
    }

    float computeDamage(float damage)
    {
        return damage-this.defenseValue;
    }

    public void takeDamage(float damage)
    {
        damage = computeDamage(damage);
        if (damage <= 0) return;
        if (currentHealth - damage <= 0) { 
            currentHealth = 0;
            this.Die();
        }
        currentHealth -= damage;
    }

    public virtual void Die()
    {
        Debug.Log(this.gameObject.name + " is dead");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void Attack(GameObject target)
    {
        target.GetComponent<Character_Controler>().takeDamage(this.attackDamage);
        StartCoroutine(AttackCooldown());
    }

    public void NextLevel()
    {
        maxHealth *= 1.5f;
        attackDamage *= 1.5f;
        defenseValue *= 1.5f;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(this.attackCooldown);
        canAttack = true;
    }
}
