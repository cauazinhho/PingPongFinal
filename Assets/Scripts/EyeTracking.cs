using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EyeTracking : MonoBehaviour
{

    public Transform alvo;               // O objeto a ser seguido (ex: jogador)
    public float alcanceMaximo = 2f;     // Distância máxima que o olho pode se mover
    private float alturaFixaY;

    private Vector3 posicaoInicial;

    void Start()
    {
        alturaFixaY = transform.position.y;
        posicaoInicial = transform.position;
    }

    void Update()
    {
        if (alvo == null)
            return;

        // Calcula a direção horizontal (X e Z) do olho para o alvo
        Vector3 direcao = alvo.position - posicaoInicial;
        direcao.y = 0; // Ignora Y, queremos só movimento no plano XZ

        // Limita a distância com base no alcance máximo
        if (direcao.magnitude > alcanceMaximo)
        {
            direcao = direcao.normalized * alcanceMaximo;
        }

        // Nova posição = posição inicial + direção limitada + altura fixa
        Vector3 novaPosicao = posicaoInicial + direcao;
        novaPosicao.y = alturaFixaY; // Mantém o Y fixo

        transform.position = novaPosicao;
    }

}
