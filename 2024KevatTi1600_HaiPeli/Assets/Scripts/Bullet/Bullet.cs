using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float currentSpeed = 5f;
    private float lifespan = 2.5f;
    private float lifeTimer;
   
    void OnEnable()
    {
        lifeTimer = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);       
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0)
        {
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable damageable = col.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(1);
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }
}
