using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorbieC_Script : MonoBehaviour
{
    public float salto;
    public float jumpTime;
    public float localGravityScale;
    public bool enSuelo = true;
    public bool go = true;
    public bool death = true;
    public Transform comprobarSuelo;
    float comprobarRadio = 0.3f;
    public LayerMask mascaraSuelo;
    // Variables para salto v2.0
    private float jumpTimeCounter;
    private bool isJumping;
    private float time = 1;
    private float rand;
    //
    private Animator anim;
    public GameObject GameC;
    public GameObject generadorEnemigos;
    public BoxCollider2D col1;
    public PolygonCollider2D col2;
    public Rigidbody2D rbd;
    public Rigidbody2D caballeroRb;
    
    public bool lose = false;
    bool lose2 = false;
    public bool firstTime = false;
    // Audios
    public AudioClip audioInicio;
    public AudioClip audioSalto;
    public AudioSource dorbieDeath;
    public AudioSource dorbieCry;
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
        rand = Random.Range(15, 30);
        if (GameC.GetComponent<GameController>().gameState == GameState.Playing)
        {
            anim.Play("DorbieC_Caminar");
            rbd.gravityScale = localGravityScale;
        }

        else if (GameC.GetComponent<GameController>().gameState == GameState.Idle)
        {
            anim.Play("DorbieC_Idle");
            firstTime = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool gamePlaying = GameC.GetComponent<GameController>().gameState == GameState.Playing;
        if (gamePlaying && ((Input.GetKeyDown(KeyCode.Mouse0))) && enSuelo && lose2==false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rbd.velocity = Vector2.up * salto;
            anim.Play("DorbieC_Caminar");
            AudioSource.PlayClipAtPoint(audioSalto, Camera.main.transform.position);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Touch", go);
            rbd.gravityScale = localGravityScale;
        }
        ////////DOBLE SALTO/////////////
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

        if (lose == true)
        {
            Vector3 pos = transform.position;
            Vector3 pos2 = transform.position;
            pos2.x = -7.20f;
            transform.position = pos2;

            pos.x += -0.05f;
            transform.position = pos;
            lose2 = true;
        }

        if (GameC.GetComponent<GameController>().gameState == GameState.End)
        {
            lose = false;
            rbd.gravityScale = 3;
        }
        if (firstTime == true && Input.GetKey(KeyCode.Mouse0))
        {
            AudioSource.PlayClipAtPoint(audioInicio, Camera.main.transform.position);
            firstTime = false;
        }

        time += Time.deltaTime;

        if (time >= rand && GameC.GetComponent<GameController>().score >= 5&&lose==false)
        {
            Debug.Log("this is Random audio" + audioClipArray);
            time = 1f;
            randomAudios.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
            randomAudios.PlayOneShot(randomAudios.clip);
        }
        if (time + 1 >= rand && GameC.GetComponent<GameController>().score >= 60 && lose == false)
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Enemigo")
        {
            Destroy(col.gameObject);
            col1.enabled = false;
            col2.enabled = false;

            caballeroRb.gravityScale = 3;
            anim.Play("DorbieC_Muerte");
            //GameC.GetComponent<GameController>().gameState = GameState.Playing;
            GameC.GetComponent<GameController>().Caballero.SetActive(true);
            Destroy(gameObject, 2f);
            lose = true;
            StartCoroutine(playAudio());
            
        }
         if (col.gameObject.tag == "deadZone")
        {
            
            col1.enabled = false;
            col2.enabled = false;
            
            anim.Play("DorbieC_Muerte");
            //GameC.GetComponent<GameController>().gameState = GameState.Playing;
            GameC.GetComponent<GameController>().Caballero.SetActive(true);
           
            caballeroRb.AddForce(transform.up *800);
            Destroy(gameObject, 2f);
            lose = true;
            StartCoroutine(playAudio());

        }
    }
    IEnumerator playAudio()
    {
        dorbieCry.Play();
        yield return new WaitWhile(() => dorbieCry.isPlaying);
        dorbieDeath.Play();
        
    }
}
