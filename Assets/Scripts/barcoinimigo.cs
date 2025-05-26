using UnityEngine;

public class BarcoInimigo : MonoBehaviour
{
    public Transform canhaoEsquerdo;
    public Transform canhaoDireito;
    public GameObject projetilPrefab;
    public float velocidadeMovimento = 2f;
    public float intervaloTiro = 2f;
    public float forcaTiro = 10f;

    private Transform jogador;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("BarcoJogador").transform;
        InvokeRepeating("Atirar", 1f, intervaloTiro);
    }

    void Update()
    {
        // Movimento automático pra frente
        transform.Translate(Vector2.left * velocidadeMovimento * Time.deltaTime);

        // Opcional: virar os canhões na direção do jogador
        Vector2 direcaoJogador = (jogador.position - transform.position).normalized;

        // Mira os canhões
        canhaoEsquerdo.right = direcaoJogador;
        canhaoDireito.right = direcaoJogador;
    }

    void Atirar()
    {
        AtirarDoCanhao(canhaoEsquerdo);
        AtirarDoCanhao(canhaoDireito);
    }

    void AtirarDoCanhao(Transform canhao)
    {
        GameObject projetil = Instantiate(projetilPrefab, canhao.position, canhao.rotation);
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        rb.AddForce(canhao.right * forcaTiro, ForceMode2D.Impulse);
    }
}
