using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform gunTransform;
    public float moveSpeed = 5f;
    
    private Rigidbody2D body;

    private Master controls;

    private Vector2 moveInput;
    private Vector2 aimInput;
    private void Awake() {
        controls = new Master();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        controls.Enable();    
    }
 
    private void OnDisable(){
        controls.Disable();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        body.MovePosition(body.position + movement);
    }

    private void Update(){
        Shoot();
        Aim();
    }

    void Aim()
    {
        aimInput = controls.Player.Aim.ReadValue<Vector2>();
        if(aimInput.sqrMagnitude > 0.1){

            Debug.Log(aimInput);
        }
    }

    void Shoot()
    {
        if(controls.Player.Fire.triggered){
            Debug.Log("Ampuu");
            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;

        }
    }
}
