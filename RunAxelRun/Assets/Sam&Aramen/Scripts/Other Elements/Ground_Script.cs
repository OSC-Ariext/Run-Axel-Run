using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Script : MonoBehaviour
{
    public float scrollSpeed;
    private float destroyPos;
    private GameObject gameCon;

    // Start is called before the first frame update
    void Start()
    {
       gameCon = GameObject.Find("GameController");
       destroyPos = -20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = gameCon.GetComponent<GameController>().gameState == GameState.Playing;
        float mult = gameCon.GetComponent<GameController>().speedMulti;
        if (gamePlaying)
            transform.Translate(Vector2.left * (scrollSpeed) * Time.deltaTime);

        if (transform.position.x <= destroyPos)
            Destroy(this.gameObject);
    }
}
