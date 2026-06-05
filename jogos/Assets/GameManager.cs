using UnityEngine;
using TMPro; // Obrigatório para usar TextMeshPro
using UnityEngine.SceneManagement; // Necessário para reiniciar a fase

public class GameManager : MonoBehaviour
{
    [Header("Textos da Interface")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoAmmo;
    public GameObject textoGameOver;

    [Header("Valores do Jogo")]
    public int score = 0;
    public int municaoAtual = 10;
    public int municaoMaxima = 10;
    public float tempo = 60f;
    private bool jogoAtivo = true;

    void Start()
    {
        municaoAtual = municaoMaxima; // Jogo começa com munição cheia
        AtualizarTextos();
    }

    void Update()
    {
        // Reinicia o jogo a qualquer momento que você apertar Enter!
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ReiniciarJogo();
        }

        if (jogoAtivo)
        {
            tempo -= Time.deltaTime;
            AtualizarTextos();

            // ⚠️ CORRIGIDO: O jogo agora SÓ acaba se o TEMPO chegar a 0.
            // Quando a munição zerar, o jogo continua ativo para você poder apertar R.
            if (tempo <= 0)
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
        if (jogoAtivo && municaoAtual > 0)
        {
            municaoAtual--;
            AtualizarTextos();
        }
    }

    public void RecarregarMunicao()
    {
        if (jogoAtivo)
        {
            municaoAtual = municaoMaxima;
            AtualizarTextos();
        }
    }

    // Funções auxiliares para o script do Atirador consultar
    public bool PodeAtirar()
    {
        return jogoAtivo && municaoAtual > 0;
    }

    public bool PrecisaRecarregar()
    {
        return municaoAtual < municaoMaxima;
    }

    public void MostrarTextoRecarregando()
    {
        if (textoAmmo != null)
        {
            textoAmmo.text = "Recarregando...";
        }
    }

    void AtualizarTextos()
    {
        if (textoScore != null) textoScore.text = "Score: " + score;

        if (textoAmmo != null)
        {
            // Mostra o formato correto: Atual / Máximo
            textoAmmo.text = "Ammo: " + municaoAtual + " / " + municaoMaxima + " | Tempo: " + Mathf.RoundToInt(tempo);
        }
    }

    void FimDeJogo()
    {
        jogoAtivo = false;
        tempo = 0;
        if (textoGameOver != null) textoGameOver.SetActive(true);
    }

    void ReiniciarJogo()
    {
        string nomeDaCenaAtual = SceneManager.GetActiveScene().name;

        if (string.IsNullOrEmpty(nomeDaCenaAtual))
        {
            Debug.LogError("⚠️ ALERTA: Você precisa salvar sua Fase antes de apertar Enter! Aperte Ctrl+S no Unity.");
            return;
        }

        SceneManager.LoadScene(nomeDaCenaAtual);
    }
}