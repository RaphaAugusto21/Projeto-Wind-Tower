using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadController : MonoBehaviour
{

    public List<Sprite> listaImg;

    public GameObject logomarca;
    public GameObject menuWindTower;
    private GameObject proximo;
    private GameObject anterior;
    private GameObject iniciarSimulator;

    public float tempoDalogo = 2f;
    public float tempoDeTela = 4f;
    public float tempoDetelaMenu = 4f;
    public float tempoDeLoad = 2f;

    private int imagenlist = 0;
    private UnityEngine.UI.Image transparenciaLogo;
    private UnityEngine.UI.Image transparenciaMenu;
    public bool okay = true;

    // Start is called before the first frame update
    void Start ()
    {
        menuWindTower = GameObject.Find("Manual").gameObject;
        logomarca = GameObject.Find("backgroundPET").gameObject;
        proximo = GameObject.Find("Proximo").gameObject;
        anterior = GameObject.Find("Anterior").gameObject;
        iniciarSimulator = GameObject.Find("Simular").gameObject;

        proximo.SetActive (false);
        anterior.SetActive (false);
        iniciarSimulator.SetActive(false);

        transparenciaLogo = logomarca.GetComponent<UnityEngine.UI.Image> ();
        transparenciaMenu = menuWindTower.GetComponentInChildren<UnityEngine.UI.Image> ();

        transparenciaLogo.color = new Vector4 (255f, 255f, 255f, 0f);
        transparenciaMenu.color = new Vector4(255f, 255f, 255f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tempoDalogo > 0f)
        {
            tempoDalogo -= Time.deltaTime;
        }
        else if(okay)
        {
            transparenciaLogo.color = new Vector4(255f, 255f, 255f, transparenciaLogo.color.a + Time.deltaTime);

            if(tempoDeTela > 0f)
            {
                tempoDeTela -= Time.deltaTime;
            }
            else
            {
                logomarca.SetActive(false);

                transparenciaMenu.color = new Vector4(255f, 255f, 255f, transparenciaMenu.color.a + Time.deltaTime);

                if (tempoDetelaMenu > 0f)
                {
                    tempoDetelaMenu -= Time.deltaTime;
                }
                else
                {
                    proximo.SetActive(true);
                    okay = false;
                }
            }
        }
    }


    public void buttonNext()
    {
        if (!okay)
        {
            imagenlist++;

            transparenciaMenu.sprite = listaImg[imagenlist];

            if (imagenlist == 1)
            {
                anterior.SetActive(true);
            }
            else if (imagenlist == 8)
            {
                iniciarSimulator.SetActive(true);
                proximo.SetActive(false);
            }
        }
    }

    public void buttonAnt()
    {
        if (!okay)
        {
            imagenlist--;

            transparenciaMenu.sprite = listaImg[imagenlist];

            if (imagenlist == 0)
            {
                anterior.SetActive(false);
            }
            else if (imagenlist == 7)
            {
                proximo.SetActive(true);
            }
        }

    }

    public void buttonLoad()
    {
        SceneManager.LoadScene(1);
    }
}
