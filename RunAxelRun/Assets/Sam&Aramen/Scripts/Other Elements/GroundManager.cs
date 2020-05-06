using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager instance;

    public GameObject[] groundBlock;
    public GameObject gameCon;

    private float spawnPosition;
    private float lenght;
    private float groundSec, fallSec;
    private float scrollSp, spMult;
    private readonly int[] noiseValues = new int[5];
    private int blockValue;
    private int aux;
    private int count;
    private bool dangerGround, forceJump;
    private bool checkBird;


    private void Awake()
    {
        if (instance == null) {
            instance=this;
        }
    }

    void Start()
    {
        scrollSp = 2.75f;
        dangerGround = false;
        lenght = groundBlock[0].GetComponent<SpriteRenderer>().bounds.size.x;
        spawnPosition = -15.0f;
        forceJump = true;
        count = 0;

        for (int i = 0; i < 6; i++)
        {
            GameObject plancha = (GameObject)Instantiate(groundBlock[(int)Random.Range(0, (groundBlock.Length)-7)], new Vector3(spawnPosition, -4.5f, 0), Quaternion.identity);
            plancha.GetComponent<Ground_Script>().scrollSpeed = scrollSp * 1.3f;
            spawnPosition += (lenght - 0.115f);
        }
    }

    IEnumerator ProceduralFloor()
    {
        while (true)
        {
            GameObject[] onPlay = GameObject.FindGameObjectsWithTag("Ground");
            aux = gameCon.GetComponent<GameController>().score;
            int maxRange;
            //Definir parametros de instancia
            #region 
            if (aux >= 0 && aux <=9)
            {
                groundSec = 1.81f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.3f;
                checkBird = false;
            }
            if (aux > 9 && aux <= 18)
            {
                groundSec = 1.68f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.4f;
                checkBird = false;
            }
            if (aux > 18 && aux <= 27)
            {
                groundSec = 1.555f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.5f;
                checkBird = false;
            }
            if (aux > 27 && aux <= 36)
            {
                groundSec = 1.465f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.6f;
                checkBird = false;
            }
            if (aux > 36 && aux <= 45)
            {
                groundSec = 1.385f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.7f;
                checkBird = false;
            }
            if (aux >= 45 && aux <= 54)
            {
                groundSec = 1.295f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.8f;
                checkBird = false;
            }
            if (aux > 54 && aux <= 63)
            {
                groundSec = 1.235f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 1.9f;
                checkBird = false;
            }
            if (aux > 63 && aux <= 72)
            {
                groundSec = 1.17f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.0f;
                checkBird = true;
            }
            if (aux > 72 && aux <= 81)
            {
                groundSec = 1.11f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.1f;
                checkBird = true;
            }
            if (aux > 81 && aux <= 90)
            {
                groundSec = 1.07f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.2f;
                checkBird = true;
            }
            if (aux > 90 && aux <= 99)
            {
                groundSec = 1.0055f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.3f;
                checkBird = true;
            }
            if (aux > 99 && aux <= 108)
            {
                groundSec = 0.98f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.4f;
                checkBird = true;
            }
            if (aux > 108 && aux <= 117)
            {
                groundSec = 0.935f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.5f;
                checkBird = true;
            }
            if (aux > 117 && aux <= 126)
            {
                groundSec = 0.9f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.6f;
                checkBird = true;
            }
            if (aux > 126 && aux <= 135)
            {
                groundSec = 0.855f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.7f;
                checkBird = true;
            }
            if (aux > 135 && aux <= 144)
            {
                groundSec = 0.82f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.8f;
                checkBird = true;
            }
            if (aux > 144 && aux <= 153)
            {
                groundSec = 0.787f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 2.9f;
                checkBird = true;
            }
            if (aux >= 153 && aux < 162)
            {
                groundSec = 0.775f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.0f;
                checkBird = true;
            }
             if(aux >= 162 && aux < 172)
            {
                groundSec = 0.742f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.1f;
                checkBird = true;
            }
             if (aux >= 171 && aux < 180)
            {
                groundSec = 0.725f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.2f;
                checkBird = true;
            }
             if (aux >= 180 && aux < 189)
            {
                groundSec = 0.685f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.3f;
                checkBird = true;
            }
            if (aux >= 189 && aux < 200)
            {
                groundSec = 0.665f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.4f;
                checkBird = true;
            }
            
            if (aux >= 200)
            {
                groundSec = 0.64f;
                fallSec = 0.75f;
                scrollSp = 2.75f * 3.5f;
                checkBird = true;
            }
            #endregion

            //Ajustar la velocidad de los elementos ya en juego.
            foreach (GameObject item in onPlay)
            {
                item.GetComponent<Ground_Script>().scrollSpeed = scrollSp;
            }

            //Checar pajaros del suelo activados
            if (checkBird == true)
            {
                maxRange = groundBlock.Length;
            }
            else
            {
                maxRange = groundBlock.Length - 3;
            }

            //Generar la plancha de elementos
            for (int i = 0; i < noiseValues.Length; i++)
            {
                noiseValues[i] = (int)Random.Range(0, 29);
                //Generar una plancha de piso
                if (((noiseValues[i] % 3 == 0 || noiseValues[i] % 4 == 0 || dangerGround)) && forceJump == false)
                {
                    GameObject bloque = (GameObject)Instantiate(groundBlock[(int)Random.Range(0, maxRange)], new Vector3(spawnPosition, -4.5f, 0), Quaternion.identity);
                    bloque.GetComponent<Ground_Script>().scrollSpeed = scrollSp;
                    dangerGround = false;
                    yield return new WaitForSeconds(groundSec);
                }
                else //Generar un espacio vacio
                {
                    dangerGround = true;
                    if (forceJump == true)
                    {
                        forceJump = false;
                        yield return new WaitForSeconds(fallSec - 0.5f);
                    }
                    else yield return new WaitForSeconds(fallSec);
                }
            }
        }
    }
}
