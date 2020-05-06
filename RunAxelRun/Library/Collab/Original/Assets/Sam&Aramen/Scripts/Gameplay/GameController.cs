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

    private int personaje;
    private Color color;
    public int score;
    public GameState gameState;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Idle;
        Time.timeScale = 1;
        StartCoroutine("FadeIn");
        personaje = PlayerPrefs.GetInt("Character");
        score = 0;

        switch (personaje) {
            case 0://axel
                Axel.SetActive(true);
                break;
            case 1://caballero
                Caballero.SetActive(true);
                break;
            case 2://princesa
                Princesa.SetActive(true);
                break;
            case 3://DorbieSolo
                Dorbie.SetActive(true);
                break;
            case 4://Dorbie con Caballero
                DorbieC.SetActive(true);
                break;
            case 5://Dorbie Con Princesa
                DorbieP.SetActive(true);
                break;   
        }

        int reward = PlayerPrefs.GetInt("startReward");

        if (reward == 1)
        {
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
    }

    // Update is called once per frame
    void Update()
    {
         if (gameState == GameState.Idle && (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            //Time.timeScale = 1;
            gameState = GameState.Playing;
            Invoke("StartGame", 0.5f);
        }

        else if (gameState == GameState.End)
        {
            GroundManager.instance.StopAllCoroutines();
            StopAllCoroutines();
            scorePanel.enabled = false;
            scoreTxt.enabled = false;
            reinicio.SetActive(true);

            //Comporbar si el dorbie esta activo
            if (PlayerPrefs.GetInt("DorbieFlag") != 1 && PlayerPrefs.GetInt("HiScore") >= 100)
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
    
    void StartGame()
    {
        
        if (fadePanel.color.a <= 0)
        {
            fadePanel.enabled = false;
        }

        StartCoroutine("FadeOut");
        enemigos2.SetActive(true);

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
            scoreTxt.text = "" + score;
        }
    }

    public void starGameRward()
    {
        Time.timeScale = 1;
        int scoreR = PlayerPrefs.GetInt("LastScore");
        score = score + scoreR;
        scoreTxt.text = "" + score;
        gameState = GameState.Playing;
        StartGame();
    }
    
    public void MenuReinicio()
    {
        SceneManager.LoadScene("Main Menu");
        PlayerPrefs.SetInt("panelOn", 1);
    }
    public void JugardeNuevo()
    {
        SceneManager.LoadScene("Gameplay_Aramen");
    }

    public void RewardThePlayer()
    {
        PlayerPrefs.SetInt("startReward", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
