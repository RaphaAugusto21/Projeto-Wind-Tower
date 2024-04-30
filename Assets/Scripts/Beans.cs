using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public class Beans : MonoBehaviour
{
	#region VARIAVEIS PARA CALCULAR A POTENCIA EOLICA
	public float potencial = 25f;
	public float pi = 3.1416f;
	public float constanteAr = 1.2f;
	public float raioPa = 4;
	public float velocidadeAr = 0f;
	public bool quebrouMotor;
	private float anguloTetaVelocidade = 0f;
	public float valorVRandom = 0f;
	public float valorVelocitDeTroca = 10f;
	#endregion

	#region Sounds
	public AudioClip WindIdle_Audio;
    #endregion

    #region Variaveis de Controle
    private float tempo = 0f;
	private float energiaTotal = 0f;
	private int multTempo = 1;
	private int countDown = 0;
	private int countHoras = 0;
	private int countDias = 0;
	private int variavelMult = 0;
	private int countBarras = 0;
	private int contRandom = 0;
	private int countGraficoHoras;
	private int countGraficoDias;

	private Scrollbar velocitScrolBar;
	private Scrollbar raioScrolBar;
	private Scrollbar rendScrolBar;

	private Text textoMultTime;
	private Text controletempo;
	private Text velocidadeVentoTxt;
	private Text energiaUtil;
	private Text raioDaPa;
	private Text rendimentoTorre;
	private Text raioScrollBarText;
	private Text rendScrollBarText;
	private Text velocitScronllBarText;
	private string totalenergiText;
	private string unidadeText;

	private string segundosText;
	private string minutosText;

	private UnityEngine.UI.Image turnOn;
	private UnityEngine.UI.Image segurancaOn;
	private UnityEngine.UI.Image sensorColor;
	
	private bool ligarsistema;
	private bool segurancaDoSistema;
	private bool ligarSensor;
	private bool ligarInfoGrafico;
	private bool ligarVelocidadeRandom;

	private Transform vento;
	private Transform rotor;
	private Transform sensor;
	private GameObject canvas;
	#endregion


	#region METODOS GET E SET
	public float Tempo { get => tempo; set => tempo = value; }
	public int CountMinutos { get => countDown; set => countDown = value; }
	public Text Controletempo { get => controletempo; set => controletempo = value; }
	public string SegundosText { get => segundosText; set => segundosText = value; }
	public string MinutosText { get => minutosText; set => minutosText = value; }
	public UnityEngine.UI.Image TurnOn{get {return turnOn;} set {turnOn = value;}}
    public UnityEngine.UI.Image SegurancaOn { get => segurancaOn; set => segurancaOn = value; }
    public UnityEngine.UI.Image SensorColor { get => sensorColor; set => sensorColor = value; }
    public bool Ligarsistema { get => ligarsistema; set => ligarsistema = value; }
    public bool SegurancaDoSistema { get => segurancaDoSistema; set => segurancaDoSistema = value; }
    public bool LigarSensor { get => ligarSensor; set => ligarSensor = value; }
    public float Potencial { get => potencial; set => potencial = value; }
    public float RaioPa { get => raioPa; set => raioPa = value; }
    public float VelocidadeAr { get => valorVelocitDeTroca; set => valorVelocitDeTroca = value; }
    public Transform Vento { get => vento; set => vento = value; }
    public GameObject Canvas { get => canvas; set => canvas = value; }
    public Text VelocidadeVentoTxt { get => velocidadeVentoTxt; set => velocidadeVentoTxt = value; }
    public Text EnergiaUtilTxt { get => energiaUtil; set => energiaUtil = value; }
    public Text RaioDaPatxt { get => raioDaPa; set => raioDaPa = value; }
    public Text RendimentoTorreTxt { get => rendimentoTorre; set => rendimentoTorre = value; }
    public Scrollbar VelocitScrolBar { get => velocitScrolBar; set => velocitScrolBar = value; }
    public Scrollbar RaioScrolBar { get => raioScrolBar; set => raioScrolBar = value; }
    public Scrollbar RendScrolBar { get => rendScrolBar; set => rendScrolBar = value; }
    public Text RaioScrollBarText { get => raioScrollBarText; set => raioScrollBarText = value; }
    public Text RendScrollBarText { get => rendScrollBarText; set => rendScrollBarText = value; }
    public Text VelocitScronllBarText { get => velocitScronllBarText; set => velocitScronllBarText = value; }
    public int MultTempo { get => multTempo; set => multTempo = value; }
    public Text TextoMultTime { get => textoMultTime; set => textoMultTime = value; }
    public int CountHoras { get => countHoras; set => countHoras = value; }
    public int CountDias { get => countDias; set => countDias = value; }
    public int VariavelMult { get => variavelMult; set => variavelMult = value; }
    public bool LigarInfoGrafico { get => ligarInfoGrafico; set => ligarInfoGrafico = value; }
    public float EnergiaTotal { get => energiaTotal; set => energiaTotal = value; }
    public int CountBarras { get => countBarras; set => countBarras = value; }
    public float AnguloTetaVelocidade { get => anguloTetaVelocidade; set => anguloTetaVelocidade = value; }
    public Transform Sensor { get => sensor; set => sensor = value; }
    public Transform Rotor { get => rotor; set => rotor = value; }
    public bool QuebrouMotor { get => quebrouMotor; set => quebrouMotor = value; }
    public float ValorVRandom { get => valorVRandom; set => valorVRandom = value; }
    public int ContRandom { get => contRandom; set => contRandom = value; }
    public float ValorVelocitDeTroca { get => velocidadeAr; set => velocidadeAr = value; }
    public bool LigarVelocidadeRandom { get => ligarVelocidadeRandom; set => ligarVelocidadeRandom = value; }
    public string TotalenergiText { get => totalenergiText; set => totalenergiText = value; }
    public string UnidadeText { get => unidadeText; set => unidadeText = value; }
    public int CountGraficoHoras { get => countGraficoHoras; set => countGraficoHoras = value; }
    public int CountGraficoDias { get => countGraficoDias; set => countGraficoDias = value; }

    #endregion
}
