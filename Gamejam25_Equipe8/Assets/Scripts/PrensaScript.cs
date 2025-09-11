using UnityEngine;

public class PrensaScript : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    [SerializeField] private float velocidadeIda = 2f;     // Velocidade indo para frente
    [SerializeField] private float velocidadeVolta = 1f;   // Velocidade voltando

    [Header("Limites da Prensa")]
    [SerializeField] private Transform limiteFrente;       // Até onde a prensa vai para frente
    [SerializeField] private Transform limiteTras;         // Até onde a prensa volta

    private bool indoPraFrente = true;

    private void Update()
    {
        if (indoPraFrente)
        {
            transform.position = Vector2.MoveTowards(transform.position, limiteFrente.position, velocidadeIda * Time.deltaTime);
            if (Vector2.Distance(transform.position, limiteFrente.position) < 0.01f)
                indoPraFrente = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, limiteTras.position, velocidadeVolta * Time.deltaTime);
            if (Vector2.Distance(transform.position, limiteTras.position) < 0.01f)
                indoPraFrente = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStatus.instance.Die();
        }

    }
}
