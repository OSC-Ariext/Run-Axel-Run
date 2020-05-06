using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{

    public float velocidad;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "DestruirEnemigo")
        {
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "DorbieP" || col.gameObject.tag == "DorbieC")
        {
            Destroy(gameObject);
        }
    }
}
