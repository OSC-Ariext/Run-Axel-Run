using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAudio : MonoBehaviour
{

    public Button Boton;
    public AudioSource Audio;
    public Button_Actions action;
    public Button[] BotonesDesactivar;

    public void PlayAudio()
    {
        StartCoroutine(buttonSound());
    }

    IEnumerator buttonSound()
    {
        Audio.Play();
        Boton.interactable = !Boton.interactable;

        foreach (Button boton in BotonesDesactivar)
            boton.interactable = !boton.interactable;

        yield return new WaitWhile (()=>Audio.isPlaying);
        action.SingleSet();

    }


    
}
