using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundController : MonoBehaviour
{
    public AudioSource musica;
    public AudioSource sound;

    // Update is called once per frame
    
    public void volumeMusica()
    {
        musica.volume = (GameObject.Find("ScrollbarMusic").GetComponent<Scrollbar>().value)/2f;
        GameObject.Find("PorcentagemMusica").GetComponent<Text>().text = Math.Round(((musica.volume * 2f) * 100f), 1).ToString() + " %";
    }

    public void volumeSound()
    {
        sound.volume = (GameObject.Find("ScrollbarSound").GetComponent<Scrollbar>().value) / 4f;
        GameObject.Find("PorcentagemSom").GetComponent<Text>().text = Math.Round(((sound.volume*4f)*100f), 1).ToString() + " %";
    }
}
