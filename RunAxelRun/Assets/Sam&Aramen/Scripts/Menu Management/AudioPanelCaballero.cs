using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPanelCaballero : MonoBehaviour
{
    public AudioSource AudioSi, AudioNo;
    public Button botonSi, botonNo;
    public Character_Manager characterM;

    public void PlayAudioSi()
    {
        StartCoroutine(buttonSoundSi());
    }
    public void PlayAudioNo()
    {
        StartCoroutine(buttonSoundNo());
    }

    IEnumerator buttonSoundSi()
    {
        AudioSi.Play();
        botonSi.interactable = !botonSi.interactable;
        botonNo.interactable = !botonNo.interactable;

        yield return new WaitWhile(() => AudioSi.isPlaying);
        characterM.SingleSet(4);

    }
    IEnumerator buttonSoundNo()
    {
        AudioNo.Play();
        botonSi.interactable = !botonSi.interactable;
        botonNo.interactable = !botonNo.interactable;

        yield return new WaitWhile(() => AudioNo.isPlaying);
        characterM.SingleSet(1);

    }
}
