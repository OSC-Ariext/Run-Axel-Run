using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LogoScene : MonoBehaviour
{
    private VideoPlayer VidPlayer;
 
    // Start is called before the first frame update
    void Start()
    {
       
        VidPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(WaitAndLoad(VidPlayer.length, "MyScene"));
    }


    private IEnumerator WaitAndLoad(double value, string scene)
    {
        yield return new WaitForSeconds((float)value);
        SceneManager.LoadScene("Main Menu");
        PlayerPrefs.SetInt("panelOn", 0);
    }
}
