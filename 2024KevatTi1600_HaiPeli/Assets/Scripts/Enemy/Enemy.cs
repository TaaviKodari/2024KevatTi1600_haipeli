using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int maxHealth = 3;
        //Attack
    public float attackRange = 5f;
    public float attackCooldown = 2f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;

    private float attackTimer = 0f;
    private bool isDashing = false;
    private int currentHealth;
    private float currentSpeed = 3f;
    private Rigidbody2D body;
    [SerializeField]
    private Transform playerTransform;
     Vector2 direction;
    void Awake(){
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable(){
        currentHealth = maxHealth;
    }
    
    void FixedUpdate()
    {
        Move();
        Attack();
    }

    private void Attack()
    {
        if(playerTransform == null){
            return;
        }

        if(attackTimer > 0f){
            attackTimer -= Time.fixedDeltaTime;
        }
        else if( !isDashing && Vector2.Distance(transform.position, playerTransform.position) < attackRange){
            StartCoroutine(DashAttack());
        }

    }

    IEnumerator DashAttack(){
        isDashing = true;
        float starTime = Time.time;
        while(Time.time < starTime + dashDuration){
            body.velocity = direction * dashSpeed;
            yield return null;
        }
        body.velocity = Vector2.zero;
        isDashing = false;
        attackTimer = attackCooldown;
    }

    void Move()
    {
        if(playerTransform == null)
        {
            GetPlayer();
            return;
        }

        if(isDashing){
            return;
        }

        direction = (playerTransform.position - transform.position).normalized;
        body.MovePosition(body.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    void GetPlayer()
    {
        playerTransform = GameManager.Instance.playerController.transform;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die()
    {
        EnemyPoolManager.Instance.ReturnEnemy(gameObject);
    }
}
