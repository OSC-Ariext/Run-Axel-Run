using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axel_Script : MonoBehaviour
{
    public float salto;
    public float jumpTime;
    public float localGravityScale;
    public bool enSuelo = false;
    public bool go = true;
    public Transform comprobarSuelo;
    float comprobarRadio = 0.5f;
    public LayerMask mascaraSuelo;
    private Animator anim;
    private float time = 1;
    private float rand;
    // Variables para salto v2.0
    private float jumpTimeCounter;
    private bool isJumping;
    public bool firstTime = true;
    //
    public GameObject GameC;
    public GameObject generadorEnemigos;
    public Rigidbody2D rbd;
    //
    public AudioClip deathAudio;
    public AudioClip audioInicio;
    public AudioClip audioSalto;
    public AudioClip generalLoose;
    public AudioSource cameraAudio;
    public AudioSource randomAudios;
    public AudioSource randomAudiosHard;
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
        /*Invoke("Audio1", 35); Invoke("Audio3", 125);
        Invoke("Audio2", 45); Invoke("Audio1", 145);
        Invoke("Audio3", 65); Invoke("Audio5", 165);
        Invoke("Audio4", 85); Invoke("Audio2", 195);
        Invoke("Audio5", 105); Invoke("Audio4", 205);*/

        rand = Random.Range(15, 30);

        if (GameC.GetComponent<GameController>().gameState == GameState.Playing)
        {
            anim.Play("Axel_Caminar");
            rbd.gravityScale = localGravityScale;
        }

        else if (GameC.GetComponent<GameController>().gameState == GameState.Idle)
        {
            firstTime = true;
            anim.Play("Axel_Idle");
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
            anim.Play("Axel_Caminar");
            AudioSource.PlayClipAtPoint(audioSalto, Camera.main.transform.position);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("Touch", go);
            rbd.gravityScale = localGravityScale;
        }

        if (Input.GetKey(KeyCode.Mouse0) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rbd.velocity = Vector2.up * salto;
                jumpTimeCounter -= Time.deltaTime;
            }else{
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            isJumping = false;
        }

        if(firstTime == true && Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Sound");
            AudioSource.PlayClipAtPoint(audioInicio, Camera.main.transform.position);
            firstTime = false;
        }

        time += Time.deltaTime;

        if(time >= rand && GameC.GetComponent<GameController>().score >= 5)
        {
            Debug.Log("this is Random audio" + audioClipArray);
            time = 1f;
            randomAudios.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
            randomAudios.PlayOneShot(randomAudios.clip);
        }
        if(time + 1 >= rand && GameC.GetComponent<GameController>().score >= 60)
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
        if (col.gameObject.tag == "Enemigo" || col.gameObject.tag == "deadZone")
        {
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);
            GameController.instance.Alperder.Play();
           // AudioSource.PlayClipAtPoint(generalLoose, Camera.main.transform.position, 0.3f);
            cameraAudio.Stop();
            anim.Play("Axel_Muerte");
            GameC.GetComponent<GameController>().gameState = GameState.End;
            Time.timeScale = 0f;
        }
    }
}
