using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCol : MonoBehaviour
{

    public GameObject GameC;
    public AudioSource musicCamera;
    public AudioClip princessDeath;
    public AudioClip generalLoose;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemigo")
        {
                AudioSource.PlayClipAtPoint(princessDeath, Camera.main.transform.position);
            AudioSource.PlayClipAtPoint(generalLoose, Camera.main.transform.position, 0.3f);
                musicCamera.Stop();
                GameC.GetComponent<GameController>().gameState = GameState.End;
                Time.timeScale = 0f; 
        }
    }
}
