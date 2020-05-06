using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudiosCharacters : MonoBehaviour
{

    public GameObject gameC;

    public AudioSource axelAudio;
    public AudioSource caballeroAudio;
    public AudioSource princesaAudio;
    public AudioSource dorbieAudio;
    public AudioSource dorbiePrincesaAudio;
    public AudioSource dorbieCaballeroAudio;

    public Button buttonAxel;
    public Button buttonPrincess;
    public Button buttonCaballero;
    public Button buttonDorbie;
    public Button buttonDorbieP;
    public Button buttonDorbieC;
    public Button buttonPInicial;
    public Button buttonCInicial;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Audios

    public void AxelAudio()
    {
        buttonAxel.interactable = false;
        StartCoroutine("AxelSound");
    }
    public void PrincessAudio()
    {
        buttonPrincess.interactable = false;
        buttonPInicial.interactable = false;
        StartCoroutine("PrincessSound");
    }
    public void CaballeroAudio()
    {
        buttonCaballero.interactable = false;
        buttonCInicial.interactable = false;
        StartCoroutine("CaballeroSound");
    }
    public void DorbieAudio()
    {
        buttonDorbie.interactable = false;
        StartCoroutine("DorbieSound");
    }
    public void DorbieCaballeroAudio()
    {
        buttonDorbieC.interactable = false;
        StartCoroutine("DorbieCSound");
    }
    public void DorbiePrincesaAuio()
    {
        buttonDorbieP.interactable = false;
        StartCoroutine("DorbiePSound");
    }

    IEnumerator AxelSound()
    {
        axelAudio.Play();
        buttonAxel.interactable = buttonAxel.interactable;
        yield return new WaitWhile(() => axelAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }
    IEnumerator PrincessSound()
    {
        princesaAudio.Play();
        buttonPrincess.interactable = buttonPrincess.interactable;
        buttonPInicial.interactable = buttonPInicial.interactable;
        yield return new WaitWhile(() => princesaAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }
    IEnumerator CaballeroSound()
    {
        caballeroAudio.Play();
        buttonCaballero.interactable = buttonCaballero.interactable;
        buttonCInicial.interactable = buttonCInicial.interactable;
        yield return new WaitWhile(() => caballeroAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }
    IEnumerator DorbieSound()
    {
        dorbieAudio.Play();
        buttonDorbie.interactable = buttonDorbie.interactable;
        yield return new WaitWhile(() => dorbieAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }
    IEnumerator DorbieCSound()
    {
        dorbieCaballeroAudio.Play();
        buttonDorbieC.interactable = buttonDorbieC.interactable;
        yield return new WaitWhile(() => dorbieCaballeroAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }
    IEnumerator DorbiePSound()
    {
        dorbiePrincesaAudio.Play();
        buttonDorbieP.interactable = buttonDorbieP.interactable;
        yield return new WaitWhile(() => dorbiePrincesaAudio.isPlaying);
        SceneManager.LoadScene("Gameplay_Aramen");
    }


}
