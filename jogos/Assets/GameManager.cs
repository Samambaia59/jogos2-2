using UnityEngine;
using TMPro; // Obrigatório para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("Textos da Interface")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoAmmo;
    public GameObject textoGameOver; // Este é o GameObject inteiro para podermos ligar/desligar

    [Header("Valores do Jogo")]
    public int score = 0;
    public int municao = 30;
    public float tempo = 60f; // 60 segundos
    private bool jogoAtivo = true;

    void Start()
    {
        AtualizarTextos();
    }

    void Update()
    {
        if (jogoAtivo)
        {
            // Faz o tempo diminuir a cada segundo
            tempo -= Time.deltaTime;
            AtualizarTextos();

            // Se o tempo acabar ou a munição zerar, o jogo acaba
            if (tempo <= 0 || municao <= 0)
            {
                FimDeJogo();
            }
        }
    }

    public void AdicionarScore(int pontos)
    {
        if (jogoAtivo)
        {
            score += pontos;
            AtualizarTextos();
        }
    }

    public void GastarMunicao()
    {
        if (jogoAtivo && municao > 0)
        {
            municao--;
            AtualizarTextos();
        }
    }

    void AtualizarTextos()
    {
        textoScore.text = "Score: " + score;
        // Mathf.RoundToInt arredonda o tempo para não mostrar números quebrados
        textoAmmo.text = "Ammo: " + municao + " | Tempo: " + Mathf.RoundToInt(tempo); 
    }

    void FimDeJogo()
    {
        jogoAtivo = false;
        tempo = 0; // Crava em zero para não ficar negativo
        textoGameOver.SetActive(true); // Liga a mensagem de Game Over
    }
}