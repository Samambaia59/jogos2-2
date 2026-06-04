using UnityEngine;

public class Atirador : MonoBehaviour
{
    [Header("Configurações de Tiro")]
    public GameObject prefabDaBala;
    public Transform firePoint;
    public float velocidadeDaBala = 30f; // Aumentei um pouco para ficar mais realista
    public Camera cameraJogador; // A câmera para calcular o centro da tela

    [Header("Conexão")]
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && gameManager.municao > 0)
        {
            AtirarComMira();
            gameManager.GastarMunicao();
        }
    }

    void AtirarComMira()
    {
        // 1. Cria um raio invisível saindo do centro exato da tela (onde está o crosshair)
        Ray raioDaMira = cameraJogador.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 pontoDeDestino;

        // 2. Verifica se o raio da mira bateu em alguma coisa ou se foi pro infinito
        if (Physics.Raycast(raioDaMira, out hit))
        {
            pontoDeDestino = hit.point; // O ponto exato onde você mirou
        }
        else
        {
            pontoDeDestino = raioDaMira.GetPoint(1000); // Um ponto muito longe no horizonte
        }

        // 3. Cria a bala no cano da arma
        GameObject novaBala = Instantiate(prefabDaBala, firePoint.position, firePoint.rotation);

        // 4. Calcula a direção: do cano da arma até o ponto para onde você mirou
        Vector3 direcaoDoTiro = (pontoDeDestino - firePoint.position).normalized;

        // 5. Aplica a força na bala na direção correta
        novaBala.GetComponent<Rigidbody>().velocity = direcaoDoTiro * velocidadeDaBala;
    }
}