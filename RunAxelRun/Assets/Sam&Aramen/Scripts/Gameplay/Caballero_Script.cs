using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caballero_Script : MonoBehaviour
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
    private float time = 1;
    private float rand;
    //
    public GameObject GameC;
    public GameObject generadorEnemigos;
    public Rigidbody2D rbd;
    // inVulnerable
    public BoxCollider2D colliederVulnerable;
    public bool inVul = false;
    public GameObject dorbieefe;
    // Audios
    public bool firstTime = false;
    public AudioSource musicCamera;
    public AudioClip deathAudio;
    public AudioClip audioInicio;
    public AudioClip audioSalto;
    public AudioClip generalLoose;
    public AudioSource randomAudios;
    public AudioClip[] audioClipArray;
    public AudioClip[] audioClipArrayHard;

    void Awake()
    {
        anim = GetComponent<Animator>();
        randomAudios = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        rand = Random.Range(15, 30);

        if (GameC.GetComponent<GameController>().gameState == GameState.Playing)
        {
            anim.Play("Caballero_Caminar");
            rbd.gravityScale = localGravityScale;
            
        }
        if (GameC.GetComponent<GameController>().gameState == GameState.YoshisState)
        {
            anim.Play("Caballero_Caminar");
            rbd.gravityScale = localGravityScale;
        }

        else if (GameC.GetComponent<GameController>().gameState == GameState.Idle)
        {
            anim.Play("Caballero_Idle");
            firstTime = true;
        }


        if (dorbieefe.GetComponent<DorbieC_Script>().lose == true)
        {
            inVul = true;
            colliederVulnerable.enabled = false;
            sp.color = new Color(255, 255, 255, 0.5f);
            StartCoroutine("Enabled");
            StartCoroutine("Color");

            Vector3 pos = transform.position;
            pos.x = -4f;
            transform.position = pos;
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = GameC.GetComponent<GameController>().gameState == GameState.Playing;
        if (gamePlaying && (Input.GetKeyDown(KeyCode.Mouse0)) && enSuelo)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rbd.velocity = Vector2.up * salto;
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
        ////////////////////////////////////////////////////////
        if (GameC.GetComponent<GameController>().gameState == GameState.End)
        {
            anim.Play("Caballero_Muerte");
            GameC.GetComponent<GameController>().gameState = GameState.End;
            Time.timeScale = 0f;
        }
        ////////////////////////////////////////////
        Vector3 pos = transform.position;
        pos.x -= 0.01f;
        transform.position = pos;
        if (pos.x <= -5)
        {
            pos.x = -5;
            transform.position = pos;
        }

        if (firstTime == true && Input.GetKey(KeyCode.Mouse0))
        {
            randomAudios.PlayOneShot(audioInicio);
            firstTime = false;
        }
        time += Time.deltaTime;

        if (time >= rand && GameC.GetComponent<GameController>().score >= 5)
        {
            time = 1f;
            randomAudios.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
            randomAudios.PlayOneShot(randomAudios.clip);
        }
        if (time + 1 >= rand && GameC.GetComponent<GameController>().score >= 60)
        {
            Debug.Log("this is Random audio HARD" + time);
            time = 1f;
            randomAudios.clip = audioClipArrayHard[Random.Range(0, audioClipArrayHard.Length)];
            randomAudios.PlayOneShot(randomAudios.clip);
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

    IEnumerator Enabled()
    {
        yield return new WaitForSeconds(1.5f);
        colliederVulnerable.enabled = true;
    }
    IEnumerator Color()
    {
        yield return new WaitForSeconds(1.5f);
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "deadZone")
        {
            musicCamera.Stop();
            randomAudios.PlayOneShot(deathAudio);
            GameController.instance.Alperder.Play();
            //AudioSource.PlayClipAtPoint(generalLoose, Camera.main.transform.position, 0.3f);
            anim.Play("Caballero_Muerte");
            GameC.GetComponent<GameController>().gameState = GameState.End;
            Time.timeScale = 0f;
        }
    }
}
