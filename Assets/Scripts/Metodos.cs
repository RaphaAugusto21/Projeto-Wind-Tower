using System;
using UnityEngine;
using UnityEngine.UI;

public class Metodos : MonoBehaviour
{
    public Beans beans;
    private float vectorVelocit;
    public float coutdownTime = 20f;
    public GameObject fumaca;

    private AudioSource controleAudio;

    void Start() {
        beans = GetComponent<Beans> ();
        controleAudio = GetComponent<AudioSource> ();
    }

    #region METODO POTENCIAL
    float EnergiaPotencialUtil()
    {
        return (((beans.Potencial/100f) * beans.constanteAr * beans.pi * (beans.RaioPa * beans.RaioPa)
                *(((beans.VelocidadeAr) * (beans.VelocidadeAr) * (beans.VelocidadeAr))
                *((float) Math.Cos((beans.pi * beans.AnguloTetaVelocidade) / 180f)))) / 2f);
    }

  
    #endregion

    #region Metodos a Ligar e Desligar o Sistema
    public void Ligar(bool ligado)
    {
        ControleInfo(ligado);
        Rotacao(ligado);
        Sensor(ligado);
        ValorRandomVelocity(ligado);
        SetInformacaoVetorVento(ligado);

        Seguranca(ligado);
        infoGrafico(ligado);
        InfoConsumoSimples(ligado);
    }

    public void Desligar(bool Desligado)
    {
        ControleTempo(Desligado);
        ControleInfo(Desligado);
        Rotacao(Desligado);
        Sensor(Desligado);
        SetInformacaoVetorVento(Desligado);
        Seguranca(Desligado);
        infoGrafico(Desligado);
    }
    #endregion

    #region Metodo de Controle Sonoro "Não esta rodando, é para passos futuros"
    public void AudioControlleStop()
    {
        if(GameObject.Find("InfoVeew").activeSelf == false)
            controleAudio.Stop();
        else
            controleAudio.Play();
    }
    #endregion

    #region ColorirButões
    public void ColorirButoes(Color turnON, Color Seguranca, Color variantVelocit)
    {
        if (GameObject.Find("Ligar"))
        {
            beans.TurnOn = GameObject.Find("Ligar").GetComponent<UnityEngine.UI.Image>();
            beans.TurnOn.color = turnON;
        }
        if (GameObject.Find("Seguranca"))
        {
            beans.SegurancaOn = GameObject.Find("Seguranca").GetComponent<UnityEngine.UI.Image>();
            beans.SegurancaOn.color = Seguranca;
        }

        if (GameObject.Find("VariacaoVento"))
        {
            beans.SensorColor = GameObject.Find("VariacaoVento").GetComponent<UnityEngine.UI.Image>();
            beans.SensorColor.color = variantVelocit;
        }

    }
    #endregion

    #region Metodo de Controle de Tempo
    public void ControleTempo(bool verificar)
    {
        if (verificar)
        {
            beans.Tempo += Time.deltaTime * beans.MultTempo;
            beans.Controletempo.text = "Dias " + beans.CountDias.ToString() + " Horas: " + beans.CountHoras.ToString() + ":" + beans.MinutosText + beans.SegundosText;

            if (GameObject.Find("TempoDeProducao"))
            {
                GameObject.Find("TempoDeProducao").GetComponentInChildren<Text>().text = beans.Controletempo.text;
            }


            if (beans.Tempo < 10f)
            {
                beans.SegundosText = ":" + "0" + ((int)beans.Tempo).ToString();
            }
            else
            {
                beans.SegundosText = ":" + ((int)beans.Tempo).ToString();
            }

            if (beans.CountMinutos < 10)
            {
                beans.MinutosText = "0" + beans.CountMinutos.ToString();
            }
            else
            {
                beans.MinutosText = beans.CountMinutos.ToString();
            }


            if (beans.Tempo > 60f)
            {
                beans.CountMinutos++;
                
                if (beans.CountMinutos == 60)
                {
                    beans.CountHoras++;
                    beans.CountGraficoHoras++;

                    if (beans.CountHoras == 24)
                    {
                        beans.CountDias++;
                        beans.CountGraficoDias++;

                        beans.ContRandom = 0;
                        beans.CountHoras = 0;
                        beans.CountMinutos = 0;
                        beans.Tempo = 0f;
                    }
                    else
                    {
                        beans.CountMinutos = 0;
                        beans.Tempo = 0f;
                    }
                }
                else
                {
                    beans.Tempo = 0f;
                }
            }
        }
    }
    #endregion

    #region Metodo de Controle de Informação
    void ControleInfo(bool Verificar)
    {
        if (Verificar)
        {
            beans.VariavelMult = (beans.CountDias * 24 * 60 * 60) + 
                                 (beans.CountHoras * 60 * 60) + 
                                 (beans.CountMinutos * 60) + (int)beans.Tempo;

            if (EnergiaPotencialUtil() < 1000)
            {
                beans.EnergiaUtilTxt.text = ((int)EnergiaPotencialUtil()).ToString() + " W";
            }
            else
            {
                if(EnergiaPotencialUtil() > 1000)
                beans.EnergiaUtilTxt.text = Math.Round((EnergiaPotencialUtil() / 1000), 2).ToString() + " KW";

                if (EnergiaPotencialUtil() > (1000 * 1000))
                beans.EnergiaUtilTxt.text = Math.Round(EnergiaPotencialUtil() / (1000 * 1000), 2).ToString() + " MW";
                
                
                if (EnergiaPotencialUtil() > (1000 * 1000 * 1000))
                beans.EnergiaUtilTxt.text = Math.Round(EnergiaPotencialUtil() / (1000 * 1000 * 1000), 2).ToString() + " GW";
            }

            
                
            if ((EnergiaPotencialUtil() * beans.VariavelMult) < (1000f * 1000f))
            {
                beans.EnergiaTotal = (EnergiaPotencialUtil() / 1000) * beans.VariavelMult;
                beans.UnidadeText = " KJ";
                beans.TotalenergiText = Math.Round(beans.EnergiaTotal, 2).ToString() + beans.UnidadeText;

            }
            else
            {
                if ((EnergiaPotencialUtil() * beans.VariavelMult) > (1000f * 1000f))
                {
                    beans.EnergiaTotal = (EnergiaPotencialUtil() / (1000f * 1000f)) * beans.VariavelMult;
                    beans.UnidadeText = " MJ";
                    beans.TotalenergiText = Math.Round(beans.EnergiaTotal, 2).ToString() + beans.UnidadeText;
                }

                if ((EnergiaPotencialUtil() * beans.VariavelMult) > (1000f * 1000f * 1000f))
                {
                    beans.EnergiaTotal = (EnergiaPotencialUtil() / (1000f * 1000f * 1000f)) * beans.VariavelMult;
                    beans.UnidadeText = " GJ";
                    beans.TotalenergiText  = Math.Round(beans.EnergiaTotal, 2).ToString() + beans.UnidadeText;
                }

                if ((EnergiaPotencialUtil() * beans.VariavelMult) > (1000f * 1000f * 1000f * 1000f))
                {
                    beans.EnergiaTotal = (EnergiaPotencialUtil()     / (1000f * 1000f * 1000f * 1000f)) * beans.VariavelMult;
                    beans.UnidadeText = " TJ";
                    beans.TotalenergiText = Math.Round(beans.EnergiaTotal, 2).ToString() + beans.UnidadeText;
                }
            }

            if (GameObject.Find("EnergiaTotalProduzida"))
            {
                GameObject.Find("EnergiaTotalProduzida").GetComponentInChildren<Text>().text = beans.TotalenergiText;
            }


            beans.VelocidadeVentoTxt.text = "Vel.: " + Math.Round(beans.VelocidadeAr * ((float)Math.Cos((beans.pi * beans.AnguloTetaVelocidade) / 180f)), 2).ToString() + " m/s";
            beans.RaioDaPatxt.text = "Raio: " + Math.Round(beans.RaioPa, 2).ToString() + " m";
            beans.RendimentoTorreTxt.text = "Rend.: " + Math.Round(beans.Potencial, 2).ToString() + " %";
        }
    }
    #endregion

    #region Setando informações do Grafico
    void infoGrafico(bool desligado)
    {
        if (beans.LigarInfoGrafico)
        {
            if (GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text == "Horas")
            {
                if (beans.CountGraficoHoras < 10)
                {
                    if (beans.CountBarras == beans.CountGraficoHoras)
                    {
                        if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f))
                        {
                            GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                            (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f));

                            GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                            Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f), 2).ToString() + " KJ";

                        }
                        else
                        {
                            if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f) & EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) <= (1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 10f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f), 2).ToString() + " MJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 10f) & EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) <= (1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 100f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f), 2).ToString() + " MJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 100f) & EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f), 2).ToString() + " MJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f) & EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 10f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f), 2).ToString() + " GJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 10f) & EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 100f));
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f), 2).ToString() + " GJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 1000f));
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f * 1000f), 2).ToString() + " TJ";
                            }
                        }
                    }
                    else
                    {
                        if(beans.CountBarras < beans.CountGraficoHoras)
                            beans.CountBarras++;

                        if (beans.CountBarras > beans.CountGraficoHoras)
                            beans.CountBarras--;
                    }
                }
                else
                {
                    beans.CountGraficoHoras = 0;

                    if (GameObject.Find("LigarInfoGrafico"))
                    {
                        GameObject.Find("LigarInfoGrafico").GetComponent<Image>().color = Color.red;
                        beans.LigarInfoGrafico = false;
                    }
                }
            }
            else if (GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text == "Dias")
            {
                if (beans.CountGraficoDias < 10)
                {
                    if (beans.CountBarras == beans.CountGraficoDias)
                    {
                        if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f))
                        {
                            GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                            (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f));

                            GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                            Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f), 2).ToString() + " KJ";
                        }
                        else
                        {
                            if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 10f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f), 2).ToString() + " MJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 10f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 100f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f), 2).ToString() + " MJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 100f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f), 2).ToString() + " GJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 10f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f *1000f), 2).ToString() + " GJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 10f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 100f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f), 2).ToString() + " GJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 100f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 1000f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 1000f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f * 1000f), 2).ToString() + " TJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 1000f) & EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) < (1000f * 1000f * 1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 1000f * 10f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f * 1000f), 2).ToString() + " TJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 1000f * 10f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 1000f * 100f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f * 1000f), 2).ToString() + " TJ";
                            }
                            else if (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) > (1000f * 1000f * 1000f * 1000f * 100f))
                            {
                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponent<RectTransform>().sizeDelta = new Vector2(80,
                                (EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) * 646) / (1000f * 1000f * 1000f * 1000f * 1000f));

                                GameObject.Find("Barra" + beans.CountBarras.ToString()).GetComponentInChildren<Text>().text =
                                Math.Round(EnergiaPotencialUtil() * ((beans.CountHoras * 60 * 60) + (beans.CountMinutos * 60) + (int)beans.Tempo) / (1000f * 1000f * 1000f * 1000f * 1000f), 2).ToString() + " PJ";
                            }
                        }
                    }
                    else
                    {
                        if (beans.CountBarras < beans.CountGraficoDias)
                            beans.CountBarras++;

                        if (beans.CountBarras > beans.CountGraficoDias)
                            beans.CountBarras--;
                    }
                }
                else
                {
                    beans.CountGraficoDias = 0;

                    if (GameObject.Find("LigarInfoGrafico"))
                    {
                        GameObject.Find("LigarInfoGrafico").GetComponent<Image>().color = Color.red;
                        beans.LigarInfoGrafico = false;
                    }
                }
            }
        }
        else
        {
            if (GameObject.Find("LigarInfoGrafico"))
            {
                GameObject.Find("LigarInfoGrafico").GetComponent<Image>().color = Color.red;
                beans.LigarInfoGrafico = false;
            }
        }
    }
    #endregion

    #region Informações de Consumo
    public void InfoConsumoSimples(bool verificar)
    {
        if (verificar)
        {
            if (GameObject.Find("ConsumoValueText"))
            {
                GameObject.Find("ConsumoValueText").GetComponentInChildren<Text> ().text = Math.Round((EnergiaPotencialUtil()/(264)), 1).ToString() + " C";
            }
        }
    }
    #endregion

    #region Controle de Direção do Vento
    void SetInformacaoVetorVento(bool desligado)
    {
        if (GameObject.Find("vetorDirecaoVento"))
        {
            beans.AnguloTetaVelocidade = ((GameObject.Find("vetorDirecaoVento").GetComponent<Scrollbar>().value - 0.5f) * 180f);

            GameObject.Find("vetorDirecaoVento").GetComponentInChildren<Text> ().text = Math.Round(beans.AnguloTetaVelocidade, 1).ToString() + " °";
        }
    }
    #endregion
    
    #region ValorRandom da velocidade do vento
    void ValorRandomVelocity(bool desligado)
    {
        if (beans.LigarVelocidadeRandom == false) {
            if (GameObject.Find("ButtonModificar"))
            {
                if (GameObject.Find("ButtonModificar").GetComponentInChildren<Text>().text == "Horas") {
                    if (beans.CountHoras == beans.ContRandom)
                    {
                        beans.ValorVRandom = UnityEngine.Random.Range(-2f, 2f);
                        beans.VelocidadeAr = beans.ValorVelocitDeTroca + beans.ValorVRandom;
                        beans.ContRandom++;
                    }
                }
                else
                {
                    if (beans.CountDias == beans.ContRandom)
                    {
                        beans.ValorVRandom = UnityEngine.Random.Range(-2f, 2f);
                        beans.VelocidadeAr = beans.ValorVelocitDeTroca + beans.ValorVRandom;
                        beans.ContRandom++;
                    }
                }
            }
            else
            {
                if (beans.CountHoras == beans.ContRandom)
                {
                    beans.ValorVRandom = UnityEngine.Random.Range(-2f, 2f);
                    beans.VelocidadeAr = beans.ValorVelocitDeTroca + beans.ValorVRandom;
                    beans.ContRandom++;
                }
                else
                {
                    beans.VelocidadeAr = beans.ValorVelocitDeTroca + beans.ValorVRandom;
                }
            }
        }
        else
        {
            if (beans.CountHoras == beans.ContRandom)
            {

                beans.VelocidadeAr = beans.ValorVelocitDeTroca;
                beans.ContRandom++;
            }
            else
            {
                beans.VelocidadeAr = beans.ValorVelocitDeTroca;
            }
        }
    }
    #endregion

    #region Controle eletronico da torre Rotação e Sensores
    void Rotacao(bool verificar)
    {
        if (verificar)
        {

            if (GameObject.Find("Rotor"))
            {

                beans.Rotor = GameObject.Find("Rotor").transform;
                vectorVelocit += beans.VelocidadeAr * ((float)Math.Cos((beans.pi * beans.AnguloTetaVelocidade) / 180f)) * Time.deltaTime * beans.MultTempo;

                beans.Rotor.localRotation = Quaternion.Euler(0f, 0f, vectorVelocit);
            }
        }
        else
        {
            if (GameObject.Find("Rotor"))
            {
                if (vectorVelocit > 0f)
                {

                    beans.Rotor = GameObject.Find("Rotor").transform;
                    vectorVelocit = vectorVelocit - Time.deltaTime;
                    beans.Rotor.localRotation = Quaternion.Euler(0f, 0f, vectorVelocit);
                }
            }
        }
    }
    public void Sensor(bool verificar)
    {
        if (verificar)
        {
            if (GameObject.Find("RootMotorX"))
            {
                beans.Sensor = GameObject.Find("RootMotorX").GetComponent<Transform>();
                beans.Sensor.localRotation = Quaternion.Euler(0f, -beans.AnguloTetaVelocidade, 0f);
            }
        }
    }
    #endregion

    #region Metodo de Segurança
    void Seguranca(bool desligado)
    {
        if (GameObject.Find("Canvas") & beans.Canvas == null)
        {
            if (GameObject.Find("Tempo"))
            {
                beans.Controletempo = GameObject.Find("Tempo").GetComponentInChildren<Text>();
            }
            
            if (GameObject.Find("Velocidade"))
            {
                beans.VelocidadeVentoTxt = GameObject.Find("Velocidade").GetComponentInChildren<Text>();
            }

            if (GameObject.Find("Watts"))
            {
                beans.EnergiaUtilTxt = GameObject.Find("Watts").GetComponentInChildren<Text>();
            }
            
            if (GameObject.Find("Raio"))
            {
                beans.RaioDaPatxt = GameObject.Find("Raio").GetComponentInChildren<Text>();
            }

            if (GameObject.Find("ValorUtil"))
            {
                beans.RendimentoTorreTxt = GameObject.Find("ValorUtil").GetComponentInChildren<Text>();
            }

            if (GameObject.Find("Vento")) { beans.Vento = GameObject.Find("Vento").transform; }
       


            beans.Canvas = GameObject.Find("Canvas") as GameObject;
        }

        if (beans.SegurancaDoSistema)
        {
            coutdownTime = 30f;
            if (beans.VelocidadeAr > 15f)
            {
                beans.VelocidadeAr = 15f;
            }
        }
        else if (beans.Ligarsistema)
        {
            if (coutdownTime > 0f)
            {
                if ((beans.VelocidadeAr * ((float)Math.Cos((beans.pi * beans.AnguloTetaVelocidade) / 180f))) > 15f)
                {
                    coutdownTime -= Time.deltaTime;
                }
            }
            else
            {
                ColorirButoes(Color.red, Color.red, Color.red);
                Instantiate(fumaca, GameObject.Find("RootMotorX").transform.position, fumaca.transform.rotation);
                beans.QuebrouMotor = true;
                beans.Ligarsistema = false; 
            }
        }
    }
    #endregion
}
