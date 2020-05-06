using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public AudioSource boton;
    public Button[] botones;

    private void Start()
    {
        AdManager.instance.RequestBanner();
    }
    private void OnDisable()
    {
       AdManager.instance.HideBanner();
    }

    IEnumerator JugarDNuevo()
    {
        boton.Play();
        foreach (Button btn in botones)
        {
            btn.interactable = !btn.interactable;
        }
        yield return new WaitWhile(() => boton.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");

    }
    IEnumerator VolverMenu()
    {
        boton.Play();
        foreach (Button btn in botones)
        {
            btn.interactable = !btn.interactable;
        }
        yield return new WaitWhile(() => boton.isPlaying);
        PlayerPrefs.SetInt("panelOn", 1);
        SceneManager.LoadScene("Main Menu");


    }
    IEnumerator GameReward()
    {
        boton.Play();
        foreach (Button btn in botones)
        {
            btn.interactable = !btn.interactable;
        }
        yield return new WaitWhile(() => boton.isPlaying);
        PlayerPrefs.SetInt("startReward", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

   

    public void MenuReinicio()
    {
        StartCoroutine(VolverMenu());
    }
    public void JugardeNuevo()
    {
        StartCoroutine(JugarDNuevo());
    }

    public void RewardThePlayer()
    {
        StartCoroutine(GameReward());
    }
}
