using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleAmbient : MonoBehaviour
{
    public GameObject sun;
    private Beans beansItem;
    private Metodos metodosItens;
    private float rootSun = 0f;
    void Start()
    {
        beansItem = GetComponent<Beans>();
        metodosItens = GetComponent<Metodos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beansItem.Ligarsistema) {
            rootSun += (Time.deltaTime * beansItem.MultTempo)/(2.2f * 60f);
            sun.transform.localRotation = Quaternion.Euler(45f, 150f + rootSun, 0f);
        }
    }
}
