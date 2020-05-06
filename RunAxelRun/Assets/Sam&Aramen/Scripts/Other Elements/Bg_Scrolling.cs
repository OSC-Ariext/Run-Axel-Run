using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bg_Scrolling : MonoBehaviour
{
    public float scrollSpeed, startPosition;
    private float lenght;

    // Start is called before the first frame update
    void Start()
    {
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= 0 - lenght) transform.position = new Vector2(startPosition, transform.position.y);
    }
}
