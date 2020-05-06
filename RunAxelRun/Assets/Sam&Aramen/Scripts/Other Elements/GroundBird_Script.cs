using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBird_Script : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        float parentSpeed = this.gameObject.GetComponentInParent<Ground_Script>().scrollSpeed;
        //this.gameObject.GetComponent<EnemyMov>().velocidad = parentSpeed + 0.05f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * (parentSpeed/5);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DestruirEnemigo")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "DorbieP" || col.gameObject.tag == "DorbieC")
        {
            Destroy(gameObject);
        }
    }
}
