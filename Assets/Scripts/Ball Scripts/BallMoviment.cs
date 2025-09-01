using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BallMoviment : MonoBehaviour
{

    // Velocidade inicial da bolinha
    public float initialSpeed = 2f;


    // Aumento de velocidade por cada toque
    public float speedIncrement = .1f;


    // Velocidade máxima da bolinha
    public float maxSpeed = 30f;


    // Variaveis para configurar a bolinha.
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 direction;
    private float delayBeforeStart = 2f;

    // Função chamada ao iniciar o jogo.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;


        currentSpeed = initialSpeed;
    }

    // Função que é chamada a cada frame.
    void FixedUpdate()
    {
        if (!rb.isKinematic)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
    }

    // Função para lançar a bolinha.
    public void LaunchBall()
    {
        direction = GetRandomDiagonalDirection();
        currentSpeed = initialSpeed;
        rb.isKinematic = false;
        rb.linearVelocity = direction * currentSpeed;
    }

    // Função para pegar uma direção aleatória para os primeiros lances na diagonal.
    private Vector3 GetRandomDiagonalDirection()
    {
        float x = Random.Range(0.6f, 1f) * (Random.value < 0.5f ? -1 : 1);
        float z = Random.Range(0f, 0.4f) * (Random.value < 0.5f ? -1 : 1);

        return new Vector3(x, 0f, z).normalized;
    }

    // Adiciona velocidade a cada toque.
    public void AddSpeed()
    {
        if (rb.isKinematic) return;

        currentSpeed = Mathf.Min(currentSpeed + Random.Range(speedIncrement, 1f), maxSpeed);
        rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
    }


    // Para a bolinha
    public void StopBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    // Define a posição da bolinha
    public void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, 1f, position.z);
    }

    // Reseta a bolinha (posição e velocidade).
    public void ResetGame()
    {
        StopBall();
        SetPosition(Vector3.zero);
        Invoke(nameof(ResetScene), delayBeforeStart);
    }

    // Reseta a cena do jogo.
    public void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Reseta a velocidade da bolinha.
    public void ResetSpeed()
    {
        currentSpeed = initialSpeed;
    }
}