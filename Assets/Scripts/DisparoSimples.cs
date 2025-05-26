using UnityEngine;
using System.Collections.Generic;

public class DisparoSimples : MonoBehaviour
{
    public GameObject prefabDaEsfera;
    public List<Transform> canhoesEsquerda = new List<Transform>();
    public List<Transform> canhoesDireita = new List<Transform>();
    public float forcaDisparo = 700f;
    public float tempoEntreTiros = 0.2f;
    public float tempoEntreSalvas = 1.0f; // tempo extra após cada salva de 3 tiros
    public int tirosPorSalva = 3;

    private int indexEsquerda = 0;
    private int indexDireita = 0;
    private int contadorTirosEsquerda = 0;
    private int contadorTirosDireita = 0;

    private float proximoTiroPermitido = 0f;

    void Update()
    {
        if (Time.time >= proximoTiroPermitido)
        {
            if (Input.GetMouseButton(0) && canhoesEsquerda.Count > 0)
            {
                AtirarEsquerda();
            }
            else if (Input.GetMouseButton(1) && canhoesDireita.Count > 0)
            {
                AtirarDireita();
            }
        }
    }

    void AtirarEsquerda()
    {
        Transform canhao = canhoesEsquerda[indexEsquerda];
        DispararDoCanhao(canhao);

        indexEsquerda = (indexEsquerda + 1) % canhoesEsquerda.Count;
        contadorTirosEsquerda++;

        if (contadorTirosEsquerda >= tirosPorSalva)
        {
            proximoTiroPermitido = Time.time + tempoEntreSalvas;
            contadorTirosEsquerda = 0;
        }
        else
        {
            proximoTiroPermitido = Time.time + tempoEntreTiros;
        }
    }

    void AtirarDireita()
    {
        Transform canhao = canhoesDireita[indexDireita];
        DispararDoCanhao(canhao);

        indexDireita = (indexDireita + 1) % canhoesDireita.Count;
        contadorTirosDireita++;

        if (contadorTirosDireita >= tirosPorSalva)
        {
            proximoTiroPermitido = Time.time + tempoEntreSalvas;
            contadorTirosDireita = 0;
        }
        else
        {
            proximoTiroPermitido = Time.time + tempoEntreTiros;
        }
    }

    void DispararDoCanhao(Transform canhao)
    {
        GameObject esfera = Instantiate(prefabDaEsfera, canhao.position, canhao.rotation);
        Rigidbody rb = esfera.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(canhao.forward * forcaDisparo);
        }
    }
}
