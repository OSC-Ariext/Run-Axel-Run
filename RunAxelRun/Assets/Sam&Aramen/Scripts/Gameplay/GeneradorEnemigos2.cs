using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos2 : MonoBehaviour
{
    public GameObject enemigoAereo;
    public float maxY, minY,minTiempo,maxTiempo;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Enemigos2", 60, 20);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  Enemigos2()
    {
        float rand = Random.Range(minY, maxY);
        Vector3 pos = transform.position;
        pos.y = rand;
        pos.x = 16.15f;
        transform.position = pos;
        Instantiate(enemigoAereo , transform.position, Quaternion.identity);
       
    }
}
