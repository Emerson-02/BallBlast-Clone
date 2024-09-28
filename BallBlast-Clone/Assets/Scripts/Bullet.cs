using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        DestroyBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para destruir a bala após 5 segundos
    public void DestroyBullet()
    {
        Destroy(gameObject, 5f);
    }
}
