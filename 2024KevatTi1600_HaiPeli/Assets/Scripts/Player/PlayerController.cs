using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform gunTransform;
    public float moveSpeed = 5f;
    public Sprite sideSprite;
    public Sprite topSprite;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    private Master controls;

    private Vector2 moveInput;
    private Vector2 aimInput;
    private void Awake() {
        controls = new Master();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start(){
        GameManager.Instance.playerController = this;
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
        UpdateSpriteDirection();
    }

    void UpdateSpriteDirection()
    {
        if(moveInput.sqrMagnitude > 0.1f){
            if(Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y)){
                spriteRenderer.sprite = sideSprite;
                spriteRenderer.flipX = moveInput.x < 0;
                spriteRenderer.flipY = false;
            }
            else{
                spriteRenderer.sprite = topSprite;
                spriteRenderer.flipY = moveInput.y < 0;
                spriteRenderer.flipX = false;
            }
        }
    }

    void Aim()
    {
        aimInput = controls.Player.Aim.ReadValue<Vector2>();
        if(aimInput.sqrMagnitude > 0.1){

            Vector2 aimDirection = Vector2.zero;
            if(UsingMouse()){
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mousePosition.z = 0;
                aimDirection = mousePosition - gunTransform.position;
            }
            else{
                aimDirection = aimInput;
            }
            float angle = Mathf.Atan2(aimDirection.x, -aimDirection.y) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(0,0,angle);
        }
    }

    private bool UsingMouse(){
        if(Mouse.current.delta.ReadValue().sqrMagnitude > 0.1f){
            return true;
        }

        return false;
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
