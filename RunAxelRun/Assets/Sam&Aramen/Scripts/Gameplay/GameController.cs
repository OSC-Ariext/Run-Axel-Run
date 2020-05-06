using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, End, YoshisState };

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject Axel;
    public GameObject Caballero;
    public GameObject Princesa;
    public GameObject Dorbie;
    public GameObject DorbieC;
    public GameObject DorbieP;
    private bool princessSong = false;
    private bool dorbiePSong = false;

    public GameObject enemigos1;
    public GameObject enemigos2;
    public GameObject RewardPanel;
    public GameObject buttonReward;
    public GameObject groundController;
    public float speedMulti;

    public Image fadePanel;
    public Image spriteIniciar;//se hacen fade en el ienumerator
    public TextMeshProUGUI textoIniciarImagen;
    public GameObject reinicio;
    public Image scorePanel;
    public GameObject dorbiePanel;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    public AudioSource Alperder;
    
    public Button[] botones;

    public int personaje;
    private Color color;
    public int score;
    public GameState gameState;

    public AudioSource hiScorePrincess;
    

    private bool JUSTONE;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Idle;
        Time.timeScale = 1;
        StartCoroutine("FadeIn");
        personaje = PlayerPrefs.GetInt("Character");
        score = 0;
        JUSTONE = false;

        switch (personaje) {
            case 0://axel
                Axel.SetActive(true);
                break;
            case 1://caballero
                Caballero.SetActive(true);
                break;
            case 2://princesa
                Princesa.SetActive(true);
                princessSong = true;
                break;
            case 3://DorbieSolo
                Dorbie.SetActive(true);
                break;
            case 4://Dorbie con Caballero
                DorbieC.SetActive(true);
                break;
            case 5://Dorbie Con Princesa
                DorbieP.SetActive(true);
                dorbiePSong = true;
                break;   
        }

        int reward = PlayerPrefs.GetInt("startReward");

        if (reward == 1)
        {
            int scoreR = PlayerPrefs.GetInt("LastScore");
            score = score + scoreR;
            scoreTxt.text = "" + score;

            spriteIniciar.enabled = false;
            textoIniciarImagen.enabled = false;
            starGameRward();
            PlayerPrefs.SetInt("startReward", 0);
        }

        int BtnRe = PlayerPrefs.GetInt("ButtonReward");

        if (BtnRe <= 2)
        {
          // buttonReward.SetActive(false);
            PlayerPrefs.SetInt("ButtonReward", BtnRe + 1);
        }
        if (BtnRe >= 2)
        {
            buttonReward.SetActive(true);
            PlayerPrefs.SetInt("ButtonReward", 0);
        }

        checkForMusic();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Idle && (Input.GetKeyDown(KeyCode.Mouse0)) && JUSTONE == false)
        {
            JUSTONE = true;
            StartCoroutine(startG2());   
        }
        

        else if(gameState == GameState.Playing)
        {
            if (score > 4 && score > PlayerPrefs.GetInt("HiScore") && princessSong == true)
            {
                StartCoroutine("PrincesaHighScore");
                StopCoroutine("PrincesaHighScore");
     // Comprueba si el sonido de la princesa suena para quue no se empalmen
                if (Princesa.gameObject.GetComponent<Princesal_Script>().randomAudios.isPlaying)
                {
                    Princesa.gameObject.GetComponent<Princesal_Script>().randomAudios.Stop();
                }
            }
            if (score > 4 && score > PlayerPrefs.GetInt("HiScore") && dorbiePSong == true)
            {
                StartCoroutine("PrincesaHighScore");
                StopCoroutine("PrincesaHighScore");
     // Comprueba si el sonido de la princesa suena para quue no se empalmen
                if (DorbieP.gameObject.GetComponent<DorbieP_Script>().randomAudios.isPlaying)
                {
                    DorbieP.gameObject.GetComponent<DorbieP_Script>().randomAudios.Stop();
                }
            }
        }

        else if (gameState == GameState.End)
        {
            
            GroundManager.instance.StopAllCoroutines();
            StopAllCoroutines();
            scorePanel.enabled = false;
            scoreTxt.enabled = false;
            reinicio.SetActive(true);
            
            //Comporbar si el dorbie esta activo
            if (PlayerPrefs.GetInt("DorbieFlag") != 1 && PlayerPrefs.GetInt("HiScore") >= 200)
            {
                
                PlayerPrefs.SetInt("DorbieFlag", 1);
                dorbiePanel.SetActive(true);
            }

            //Compara la puntuacion de la run con la puntuacion mas alta
            if (score > PlayerPrefs.GetInt("HiScore"))
                PlayerPrefs.SetInt("HiScore", score);

            finalScore.text = "Score: " + score;
            PlayerPrefs.SetInt("LastScore", score);
            highScore.text = "Best: " + PlayerPrefs.GetInt("HiScore");
            
        } 
    }
    IEnumerator startG2()
    {
        yield return new WaitForSeconds(0.25f);
        gameState = GameState.Playing;
        Invoke("StartGame", 0.5f); 
    }
    
    
    void StartGame()
    {
        if (fadePanel.color.a <= 0)
        {
            fadePanel.enabled = false;
        }

        StartCoroutine("FadeOut");
        //enemigos2.SetActive(true);

        GroundManager gg = groundController.GetComponent<GroundManager>();
        
        gg.StartCoroutine("ProceduralFloor");
        StartCoroutine("ScoreManager");        
    }

    IEnumerator FadeOut()
    {
        float t = spriteIniciar.color.a;
        float u = scorePanel.color.a;

        while (t > 0)
        {
            t -= Time.deltaTime * 2f;

            spriteIniciar.color = new Color(spriteIniciar.color.r, spriteIniciar.color.g, spriteIniciar.color.b, t);
            textoIniciarImagen.color = new Color(textoIniciarImagen.color.r, textoIniciarImagen.color.g, textoIniciarImagen.color.b, t);
            yield return 0;
        }

        while(u < 1)
        {
            u += Time.deltaTime * 2;
            scorePanel.color = new Color(scorePanel.color.r, scorePanel.color.g, scorePanel.color.b, u);
            scoreTxt.color = new Color(255, 255, 255, u);
            yield return 0;
        }

        spriteIniciar.enabled=false;
    }

    IEnumerator FadeIn()
    {
        float t = fadePanel.color.a;
        while(t > 0)
        {
            t -= Time.deltaTime;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, t);
            yield return 0;
        }

        fadePanel.enabled = false;
    }
    
    IEnumerator ScoreManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.85f);
            score++;
            
            scoreTxt.text = ""+ score;
        }
    }

    IEnumerator PrincesaHighScore()
    {
        hiScorePrincess.Play();
        princessSong = false;
        yield return 0;
        
    }

    public void starGameRward()
    {
        Time.timeScale = 1;
        JUSTONE = true;
        gameState = GameState.Playing;
        Invoke("StartGame", 0.5f);
    }

    public void checkForMusic()
    {
        int pF = PlayerPrefs.GetInt("mainTheme");
        if (pF == 0)
        {
            OnOffSound.instance.tema.enabled = true;
        }
        else if (pF == 1)
        {
            OnOffSound.instance.tema.enabled = false;
        }
    }
}
