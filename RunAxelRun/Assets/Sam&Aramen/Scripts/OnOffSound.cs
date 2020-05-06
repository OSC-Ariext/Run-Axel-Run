using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffSound : MonoBehaviour
{
    public static OnOffSound instance;

    public AudioSource tema;
    public Button button;
    public Sprite img1,img2;

    int count;
    

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }


        
           
    }


    
    private void Update()
    {
        count = PlayerPrefs.GetInt("mainTheme");
        if (count == 0)
        {
            button.image.overrideSprite = img1;

        }

        if (count == 1)
        {
            button.image.overrideSprite = img2;

        }
    }

    public void changeButton()
    {
       

        if (count == 0)
        {
            button.image.overrideSprite = img1;
            count += 1;
            PlayerPrefs.SetInt("mainTheme", 1);

        }
        else
        {
            button.image.overrideSprite = img2;
            count -= 1;
            PlayerPrefs.SetInt("mainTheme", 0);
        }


        Debug.Log("count sound " +count);
    }

}
