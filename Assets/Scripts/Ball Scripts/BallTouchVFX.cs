using UnityEngine;

public class BallTouchVFX : MonoBehaviour
{
    public GameObject collisionEffectPrefab;
    public GameObject collisionEffectPrefab2;
    public LayerMask layerToCheck; // Um campo para configurar o Layer no Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Pega o ponto de contato
        ContactPoint contact = collision.contacts[0];
        Vector3 contactPoint = contact.point;

        // Verifica se o objeto colidido está no layer especificado
        if (((1 << collision.gameObject.layer) & layerToCheck) != 0)
        {
            // Instancia o efeito visual no ponto de colisão
            if (collisionEffectPrefab)
            {
                GameObject effect = Instantiate(collisionEffectPrefab, contactPoint, Quaternion.identity);
                // destrói após 1 segundo
                Destroy(effect, 2f); 
            }
        }
        else if (collisionEffectPrefab2)
        {
            GameObject effect = Instantiate(collisionEffectPrefab2, contactPoint, Quaternion.identity);
            // destrói após 1 segundo
            Destroy(effect, 2f);
        }
    }
}
