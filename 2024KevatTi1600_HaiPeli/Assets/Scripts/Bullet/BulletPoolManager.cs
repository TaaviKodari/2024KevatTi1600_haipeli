using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;
    public int poolSize = 20;
    public GameObject bulletPrefab;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake(){
        Instance = this;
        InitializePool();
    }

    private void InitializePool(){
        for(int i = 0; i < poolSize; i++){
            Debug.Log("Luodaan ammus " + i);
        }
    }
}
