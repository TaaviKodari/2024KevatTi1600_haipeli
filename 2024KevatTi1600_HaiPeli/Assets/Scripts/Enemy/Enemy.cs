using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float currentSpeed = 3f;
    private Rigidbody2D body;
    [SerializeField]
    private Transform playerTransform;

    void Awake(){
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(playerTransform == null)
        {
            GetPlayer();
            return;
        }
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        body.MovePosition(body.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    void GetPlayer()
    {
        playerTransform = GameManager.Instance.playerController.transform;
    }
}
