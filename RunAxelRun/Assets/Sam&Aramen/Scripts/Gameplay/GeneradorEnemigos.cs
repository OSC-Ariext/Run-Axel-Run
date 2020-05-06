using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject enemigo1;
    public float tiempo = 2.75f;
    

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("Enemigos1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CrearEnemigo()
    {
        Instantiate(enemigo1, transform.position, Quaternion.identity);
    }

    IEnumerator Enemigos1()
    {
        yield return new WaitForSeconds(tiempo);
        Instantiate(enemigo1, transform.position, Quaternion.identity);
        StartCoroutine("Enemigos1");

        //Debug.Log("enemigo 1 creado");
    }

}
