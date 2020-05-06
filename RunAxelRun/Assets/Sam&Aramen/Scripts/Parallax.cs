using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPos;
    public GameObject gameCam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    void Update()
    {
        float aux = (gameCam.transform.position.x * (1 - parallaxEffect));
        float distance = (gameCam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (aux > startPos + lenght) startPos += lenght;
        else if (aux < startPos - lenght) startPos -= lenght;
    }
}
