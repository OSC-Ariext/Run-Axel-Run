using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button_Actions : MonoBehaviour
{
    public static Button_Actions instance;

    public GameObject gC;
    public bool isLocked;
    private Button button;
    public Image dorbie;
    private Color color;

    public int charIndexA, canvasIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        if (isLocked)
        {
            if (PlayerPrefs.GetInt("DorbieFlag") != 1)
            {
                button = gameObject.GetComponent<Button>();
                dorbie.color = new Color(255, 255, 255, 0.5f);
                button.interactable = false;
            }
            else
            {
                dorbie.color = new Color(255, 255, 255, 1f);
            }
        }
        
    }

    private void Update()
    {
        
    }

    public void SingleSet()
    {
        Character_Manager setOption = gC.GetComponent<Character_Manager>();
        setOption.SingleSet(charIndexA);
    }

    public void MultiSet()
    {
        Character_Manager setOption = gC.GetComponent<Character_Manager>();
        if (PlayerPrefs.GetInt("DorbieFlag") == 1) setOption.SetPanelOn(canvasIndex);
        else setOption.SingleSet(charIndexA);
    }

    
}
