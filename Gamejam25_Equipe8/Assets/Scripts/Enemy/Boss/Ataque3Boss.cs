using UnityEngine;

public class Ataque3Boss : MonoBehaviour
{
    [Header("Refer�ncias")]
    public Transform maoEsquerda;
    public Transform maoDireita;
    public Transform cabeca;
    public GameObject laserPrefab;
    public LineRenderer quadradoRenderer;

    [Header("Configura��o")]
    public float laserSpeed = 20f;
    public Vector2 maoOffset = new Vector2(0, 0.5f);
    public Vector2 cabecaOffset = new Vector2(0.5f, 0);

    private bool isAttacking = false;

    public void StartAttack()
    {
        if (isAttacking) return;
        isAttacking = true;
        InvokeRepeating(nameof(DispararLasers), 0f, 0.5f);
        if (quadradoRenderer != null) quadradoRenderer.enabled = true;
    }

    public void StopAttack()
    {
        if (!isAttacking) return;
        isAttacking = false;
        CancelInvoke(nameof(DispararLasers));
        if (quadradoRenderer != null) quadradoRenderer.enabled = false;
    }

    private void DispararLasers()
    {
        InstanciarLaser(maoEsquerda.position + (Vector3)maoOffset, Vector2.right);
        InstanciarLaser(maoDireita.position - (Vector3)maoOffset, Vector2.left);
        InstanciarLaser(cabeca.position + (Vector3)cabecaOffset, Vector2.up);
        InstanciarLaser(cabeca.position - (Vector3)cabecaOffset, Vector2.down);
        DesenharQuadrado();
    }

    private void InstanciarLaser(Vector3 pos, Vector2 direction)
    {
        if (laserPrefab == null) return;
        GameObject laser = Instantiate(laserPrefab, pos, Quaternion.identity);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = direction.normalized * laserSpeed;
    }

    private void DesenharQuadrado()
    {
        if (quadradoRenderer == null) return;

        Vector3[] pontos = new Vector3[5];
        float size = 1f;
        pontos[0] = cabeca.position + new Vector3(-size, -size, 0);
        pontos[1] = cabeca.position + new Vector3(-size, size, 0);
        pontos[2] = cabeca.position + new Vector3(size, size, 0);
        pontos[3] = cabeca.position + new Vector3(size, -size, 0);
        pontos[4] = pontos[0];
        quadradoRenderer.positionCount = pontos.Length;
        quadradoRenderer.SetPositions(pontos);
    }
}
