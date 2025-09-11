using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StopBar : MonoBehaviour
{
    [Header("Pointer Variables")]
    [SerializeField] RectTransform pointer;
    [SerializeField] float speed;

    [Header("GreenBar Variables")]
    [SerializeField] RectTransform greenZone;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;

    [Header("Limits of the base bar")]
    [SerializeField] float limitLeft;
    [SerializeField] float limitRight;

    [Header("Reward")]
    [SerializeField] GameObject door;
    [SerializeField] int needToOpen;
    [SerializeField] GameObject gamePainel;

    bool moving = true;
    int neededCorrect = 0;
    float direction = 1f;

    void Start()
    {
        MoveGreenZoneRandomly();
    }

    void Update()
    {
        if (moving)
        {
            // Movimento
            pointer.anchoredPosition += Vector2.right * speed * direction * Time.deltaTime;

            // Inverte a direção ao bater nas bordas
            if (pointer.anchoredPosition.x > limitRight || pointer.anchoredPosition.x < limitLeft)
            {
                direction *= -1f;
                // Garante que não passe do limite
                float clampedX = Mathf.Clamp(pointer.anchoredPosition.x, limitLeft, limitRight);
                pointer.anchoredPosition = new Vector2(clampedX, pointer.anchoredPosition.y);
            }

            // Input
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                moving = false;
                CheckSuccess();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            gamePainel.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            gamePainel.SetActive(false);
    }

    void CheckSuccess()
    {
        Vector3 pointerWorldPos = pointer.TransformPoint(pointer.rect.center);
        Vector3 greenWorldMin = greenZone.TransformPoint(new Vector3(-greenZone.rect.width / 2f, 0f, 0f));
        Vector3 greenWorldMax = greenZone.TransformPoint(new Vector3(greenZone.rect.width / 2f, 0f, 0f));

        if (pointerWorldPos.x >= greenWorldMin.x && pointerWorldPos.x <= greenWorldMax.x)
        {
            Debug.Log("SUCESSO! Parou na zona correta.");
            neededCorrect++;

            if (neededCorrect >= needToOpen)
            {
                Debug.Log("Desbloqueado!");
                door.SetActive(false);

                return;
            }

            ResetGame();
        }
        else
        {
            ResetGame();
        }

        float halfWidth = greenZone.rect.width / 2f;
        float randomX = Random.Range(limitLeft + halfWidth, limitRight - halfWidth);
        greenZone.anchoredPosition = new Vector2(randomX, greenZone.anchoredPosition.y);
    }

    public void ResetGame()
    {
        pointer.anchoredPosition = new Vector2(limitLeft, pointer.anchoredPosition.y);
        direction = 1f;
        moving = true;

        AdjustGreenZoneSize();
        MoveGreenZoneRandomly();

    }

    #region GreenBarAjustment
    void MoveGreenZoneRandomly() //for the position of the greenBar
    {
        float halfWidth = greenZone.rect.width / 2f;
        float randomX = Random.Range(limitLeft + halfWidth, limitRight - halfWidth);
        greenZone.anchoredPosition = new Vector2(randomX, greenZone.anchoredPosition.y);
    }
    void AdjustGreenZoneSize() //for the length of the greenBar
    {
        float newWidth = Random.Range(minSize, maxSize);
        greenZone.sizeDelta = new Vector2(newWidth, greenZone.sizeDelta.y);
    }
    #endregion GreenBarAjustment
}