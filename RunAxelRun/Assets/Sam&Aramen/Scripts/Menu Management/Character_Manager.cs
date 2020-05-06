using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character_Manager : MonoBehaviour
{

    public static Character_Manager instance;
    public Text dorbieText;
    public GameObject[] uiElements;
    public Button start, exit,volver;
    public Image fadePanel;
    private int checkScore;

    public bool Iniciar = false;

    //
    public AudioSource botones;
    //public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine("FadeIn");
        AdManager.instance.RequestBanner();
        foreach(GameObject panel in uiElements)
        {
            panel.SetActive(false);
            
        }

        uiElements[0].SetActive(true);

        int dorbie = PlayerPrefs.GetInt("DorbieFlag");

        if (dorbie == 1)
            dorbieText.enabled = false;
    }
    private void Update()
    {
        int panelOn = PlayerPrefs.GetInt("panelOn");

        if (panelOn == 1)
        {
            uiElements[0].SetActive(false);
            uiElements[1].SetActive(true);
        }

        

    }
    
    public void SetPanelOn(int on)
    {
        uiElements[on].SetActive(true);
    }
    
    public void SetPlaneOff(int off)
    {
        uiElements[off].SetActive(false);
    }
    public void soundPlay() {
        StartCoroutine(soundStart());
    }
    

    IEnumerator soundStart()
    {
        botones.Play();
        start.interactable = !start.interactable;
        exit.interactable = !exit.interactable;

        yield return new WaitWhile(() => botones.isPlaying);
        SetPanelOn(1);
        SetPlaneOff(0);
        start.interactable = start.interactable;
        exit.interactable = exit.interactable;
    }
    IEnumerator soundExit()
    {
        botones.Play();
        start.interactable = !start.interactable;
        exit.interactable = !exit.interactable;

        yield return new WaitWhile(() => botones.isPlaying);
        Application.Quit();
    }
    IEnumerator soundVolver()
    {
        botones.Play();
        volver.interactable = !volver.interactable;
        start.interactable = true;
        exit.interactable = true;
        yield return new WaitWhile(() => botones.isPlaying);
        foreach (GameObject panel in uiElements)
        {
            panel.SetActive(false);

        }
        
        uiElements[0].SetActive(true);
        volver.interactable = true;
        PlayerPrefs.SetInt("panelOn", 0);
    }

    //Recibe el indice de los botones definiendo al personaje del array
    public void SingleSet(int ind)
    {

            PlayerPrefs.SetInt("Character", ind);
            SceneManager.LoadScene("Gameplay_Aramen");
        
        
    }
    public void exitGame()
    {
        StartCoroutine(soundExit());
    }
    public void Volver() {
        StartCoroutine(soundVolver());
    }

    IEnumerator FadeIn()
    {
        float t = fadePanel.color.a;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, t);
            yield return 0;
        }

        fadePanel.enabled = false;
    }
    private void OnDisable()
    {
        AdManager.instance.HideBanner();
    }

    // Audios
    public void Reproducir()
    {
        
        Debug.Log("Audio");
    }

}
