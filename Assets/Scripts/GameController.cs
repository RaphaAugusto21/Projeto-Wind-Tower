using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Beans beans;
    public Metodos metodos;

    // Start is called before the first frame update
    void Start() {
        beans = GetComponent<Beans>();
        beans.QuebrouMotor = false;
        metodos = GetComponent<Metodos>();
    }

    // Update is called once per frame
    void Update() {
        if (beans.Ligarsistema == true) {
            metodos.Ligar(beans.Ligarsistema);
            metodos.Sensor(beans.LigarSensor);
            metodos.ControleTempo(beans.Ligarsistema);
        } else {
            metodos.Desligar(beans.Ligarsistema);   
            metodos.Sensor(beans.LigarSensor);
            metodos.ControleTempo(beans.Ligarsistema);
        }
    }
     
    #region Ligar Motor Eolico
    public void SetBoolLigar() {
        if (beans.Ligarsistema == true) {
            metodos.ColorirButoes(Color.red, Color.red, Color.red);
            beans.Ligarsistema = false;
        } else {
            if (beans.QuebrouMotor == false)
            {
                metodos.ColorirButoes(Color.green, Color.green, Color.green);
                beans.SegurancaDoSistema = true;
                beans.LigarVelocidadeRandom = true;
                beans.Ligarsistema = true;
            }
        }
    }
    #endregion Sair da Aplicação

    #region Controle de Restar e Quit do game  
    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void RestarGame()
    {
        metodos.ColorirButoes(Color.red, Color.red, Color.red);
        beans.Ligarsistema = false;


        SceneManager.LoadScene(1);   
    }
    #endregion

    #region Ligar Sensor de velocidade e direção do vento
    public void SetBoolLigarvelocidadeRandom()
    {
        if (beans.LigarVelocidadeRandom == true)
        {
            metodos.ColorirButoes(Color.Lerp(beans.TurnOn.color, beans.TurnOn.color, 1),
                                 Color.Lerp(beans.SegurancaOn.color, beans.SegurancaOn.color, 1), Color.red);
            beans.LigarVelocidadeRandom = false;
        }
        else
        {
            metodos.ColorirButoes(Color.Lerp(beans.TurnOn.color, beans.TurnOn.color, 1),
                                 Color.Lerp(beans.SegurancaOn.color, beans.SegurancaOn.color, 1), Color.green);
            beans.LigarVelocidadeRandom = true;
        }
    }
    #endregion

    #region Ligar segurança do modelo Eolico
    public void SetBoolLigarSeguranca() {
        if (beans.SegurancaDoSistema == true) {
            metodos.ColorirButoes(Color.Lerp(beans.TurnOn.color, beans.TurnOn.color, 1),
                                 Color.red, Color.Lerp(beans.SensorColor.color, beans.SensorColor.color, 1));
            beans.SegurancaDoSistema = false;
        } else {
            metodos.ColorirButoes(Color.Lerp(beans.TurnOn.color, beans.TurnOn.color, 1),
                                 Color.green, Color.Lerp(beans.SensorColor.color, beans.SensorColor.color, 1));
            beans.SegurancaDoSistema = true;
        }
    }
    #endregion

    #region Informações do grafico
    public void InformaçãoDoGrafico()
    {
        if (GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text == "Horas") {
            GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text = "Dias";
        }
        else
        {
            GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text = "Horas";
        }
    }

    public void LigarGrafico()
    {
        if (GameObject.Find("LigarInfoGrafico").GetComponent<Image> ().color == Color.red)
        {
            GameObject.Find("LigarInfoGrafico").GetComponent<Image>().color = Color.green;
            beans.LigarInfoGrafico = true;
        } else {
            if(GameObject.Find("LigarInfoGrafico"))
            GameObject.Find("LigarInfoGrafico").GetComponent<Image>().color = Color.red;
            beans.LigarInfoGrafico = false;
        }
    }
    #endregion

    #region Multiplicador de Tempo
    public void MultTime()
    {
        if (beans.MultTempo < 1024) 
        {
            for (int x = 1; x < 2; x++)
            {
                beans.MultTempo = beans.MultTempo * 2;
            }
        }
        else
        {
            beans.MultTempo = 1;       
        }

        beans.TextoMultTime = GameObject.Find("Duplicar").GetComponentInChildren<Text>();
        beans.TextoMultTime.text = "X" + beans.MultTempo.ToString();
    }
    #endregion

    #region Controle de Variaveis Velocidade, Raio e Rend.
    public void SetValoresInfoVelocidade() {
        beans.VelocitScrolBar = GameObject.Find("ScrollbarVelocit").GetComponent<Scrollbar>();
        beans.VelocitScronllBarText = GameObject.Find("ScrollbarVelocit").GetComponentInChildren<Text>();
        
        beans.ValorVelocitDeTroca = (float)((beans.VelocitScrolBar.value) * 25f);
        beans.VelocitScronllBarText.text = (Math.Round(beans.ValorVelocitDeTroca, 2)).ToString() + "m/s";
    }
    public void SetValoresInfoRaio() {
        beans.RaioScrolBar = GameObject.Find("ScrollbarRaio").GetComponent<Scrollbar>();
        beans.RaioScrollBarText = GameObject.Find("ScrollbarRaio").GetComponentInChildren<Text>();

        beans.RaioPa = (float)((beans.RaioScrolBar.value) * 90f);
        beans.RaioScrollBarText.text = (Math.Round(beans.RaioPa, 2)).ToString() + "m";
    }
    public void SetValoresInfoRend() {
        beans.RendScrolBar = GameObject.Find("ScrollbarRend").GetComponent<Scrollbar>();
        beans.RendScrollBarText = GameObject.Find("ScrollbarRend").GetComponentInChildren<Text>();

        beans.Potencial = (float)((beans.RendScrolBar.value) * 40f);
        beans.RendScrollBarText.text = (Math.Round(beans.Potencial, 2)).ToString() + "%";
    }
    #endregion
}
