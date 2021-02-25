using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pauseb : MonoBehaviour
{
    public GameObject  pause, ButtonPause, PanelPausa;
    public GameObject[] Botones;
    public int i;

    // Start is called before the first frame update

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        PanelPausa.SetActive(true);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume = 0.1f;
        Time.timeScale = 0;
        OcultarBotones();
    }

    // Update is called once per frame
    public void Resume()
    {
        PanelPausa.SetActive(false);
        GameObject.Find("AudioManager").GetComponent<AudioSource>().volume = 1f;
        Time.timeScale = 1;
        MostrarBotones();
    }

    public void OcultarBotones()
    {
        for (i = 0; i <= Botones.Length - 1; i++)
        {
            Botones[i].SetActive(false);
        }
    }

    public void MostrarBotones()
    {
        for (i = 0; i <= Botones.Length - 1; i++)
        {
            Botones[i].SetActive(true);
        }
    }
}
