using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dorbie_Script : MonoBehaviour
{
    public float salto;
    public float jumpTime;
    public float localGravityScale;
    public bool enSuelo = true;
    public bool go = true;
    public Transform comprobarSuelo;
    float comprobarRadio = 0.5f;
    public LayerMask mascaraSuelo;
    private Animator anim;
    // Variables para salto v2.0
    private float jumpTimeCounter;
    private bool isJumping;
    private bool firstTime = true;

    //
    public GameObject GameC;
    public GameObject generadorEnemigos;
    public Rigidbody2D rbd;
    //
    public AudioSource musicCamera;
    public AudioSource dorbieDeath;
    public AudioClip audioInicio;
    public AudioClip audioSalto;
    public AudioClip generalLoose;
    public AudioSource randomAudios;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameC.GetComponent<GameController>().gameState == GameState.Playing)
        {
            anim.Play("Dorbie_Caminar");
            rbd.gravityScale = localGravityScale;
        }

        else if (GameC.GetComponent<GameController>().gameState == GameState.Idle)
        {
            anim.Play("Dorbie_Idle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = GameC.GetComponent<GameController>().gameState == GameState.Playing;
        if (gamePlaying && ((Input.GetKeyDown(KeyCode.Mouse0)) || (Input.GetKeyDown(KeyCode.Space))) && enSuelo)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rbd.velocity = Vector2.up * salto;
            anim.Play("Dorbie_Caminar");
            AudioSource.PlayClipAtPoint(audioSalto, Camera.main.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Touch", go);
            rbd.gravityScale = localGravityScale;
        }
        if (Input.GetKey(KeyCode.Mouse0) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rbd.velocity = Vector2.up * salto;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isJumping = false;
        }

        if (firstTime == true && Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Sound");
            AudioSource.PlayClipAtPoint(audioInicio, Camera.main.transform.position);
            firstTime = false;
        }

    }

    private void FixedUpdate()
    {
        if (GameC.GetComponent<GameController>().gameState == GameState.Idle)
        {
            enSuelo = false;
        }
        else
        {
            enSuelo = Physics2D.OverlapCircle(comprobarSuelo.position, comprobarRadio, mascaraSuelo);
            anim.SetBool("Grounded", enSuelo);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo" || col.gameObject.tag == "deadZone")
        {
            musicCamera.Stop();
            dorbieDeath.Play();
            GameController.instance.Alperder.Play();
           // AudioSource.PlayClipAtPoint(generalLoose, Camera.main.transform.position, 0.3f);
            anim.Play("Dorbie_Muerte");
            GameC.GetComponent<GameController>().gameState = GameState.End;
            Time.timeScale = 0f;
        }
    }
}
