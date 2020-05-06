using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, End };

public class GameController : MonoBehaviour
{
    

    public GameObject Axel;
    public GameObject Caballero;
    public GameObject Princesa;
    public GameObject Dorbie;
    public GameObject DorbieC;
    public GameObject DorbieP;

    public GameObject enemigos1;
    public GameObject enemigos2;

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
    private int score;
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
       

        gameState = GameState.Idle;
        Time.timeScale = 1;
        StartCoroutine("FadeIn");
        personaje = PlayerPrefs.GetInt("Character");
        speedMulti = 1.0f;
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

    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Idle && (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            Time.timeScale = 1;
            if (fadePanel.color.a <= 0)
            {
                fadePanel.enabled = false;
            }
            gameState = GameState.Playing;

            StartCoroutine("FadeOut");
            enemigos2.SetActive(true);

            GroundGenerator gg = groundController.GetComponent<GroundGenerator>();
            gg.StartCoroutine("ProceduralFloor");
            StartCoroutine("ScoreManager");
           
//Aqui podria ponerse lo del fondo, cuando da clic se empieza a mover todo
        }

        else if (gameState == GameState.Playing)
        {
            //spriteIniciar.SetActive(false);
        }

        else if(gameState == GameState.End)
        {
            StopAllCoroutines();
            scorePanel.enabled = false;
            scoreTxt.enabled = false;
            reinicio.SetActive(true);

            //Comporbar si el dorbie esta activo
            if (PlayerPrefs.GetInt("DorbieFlag") != 1 && PlayerPrefs.GetInt("HiScore") >= 10)
            {
                PlayerPrefs.SetInt("DorbieFlag", 1);
                dorbiePanel.SetActive(true);
            }
                
            //Compara la puntuacion de la run con la puntuacion mas alta
            if (score > PlayerPrefs.GetInt("HiScore"))
                PlayerPrefs.SetInt("HiScore", score);

            finalScore.text = "Score: " + score;
            highScore.text = "Best: " + PlayerPrefs.GetInt("HiScore");
        } 
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
        while(true)
        {
            yield return new WaitForSeconds(2.5f - (speedMulti - 1));
            score++; 
            scoreTxt.text = "" + score;

            if(score != 0 && score %3 == 0 && speedMulti < 2.0f)
            {
                speedMulti += 0.1f;
                Debug.Log("Mult: " + speedMulti);
            }
        }
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
  
    
}
