﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actividad6 : MonoBehaviour
{
    public Conexiones conexiones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void act6()
    {
        SceneManager.LoadScene("CargaDest");
        conexiones.PlayTimeDestreza();
    }
}