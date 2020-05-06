using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetFps : MonoBehaviour
{
    private void Awake()
    {
       
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        
    }
}
