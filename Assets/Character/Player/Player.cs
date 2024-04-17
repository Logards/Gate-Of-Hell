using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Transform orientation;
    private Character_Controler controller;
    private Rigidbody rb;

    public bool canAttack { get; set; } = true;
    public GameObject target {  get; set; }

    public float dashPower = 16f;
    public float dashUpwardForce = 16f;
    public float dashTime = 0.2f;
    
    public float maxHealth = 10f;
    private float currentHealth;

    public float defenseValue;
    public float attackDamage = 1f;

    public float attackCooldown = 1f;
    
    public float experience = 0f;
    public int level = 1;
    public float experienceToNextLevel = 10f;
    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponentInParent<Character_Controler>();
        rb = GetComponentInParent<Rigidbody>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(aAttack());
            
        }

        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel *= 1.5f;
            NextLevel();
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
    private void Dash()
    {
        Vector3 forceToApply = orientation.forward * dashPower + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashTime);
    }

    private void ResetDash()
    {
        rb.velocity = Vector3.zero;
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
