using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (GameManager.Instance.isGameStopped()) return;
        Vector3 targetPosition = transform.position;

        // Touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            targetPosition = mainCam.ScreenToWorldPoint(touch.position);
        }
        // Mouse
        else
        {
            targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        targetPosition.z = 0f;

        
        Vector3 clampedPos = ClampToScreen(targetPosition);

        transform.position = Vector3.Lerp(
            transform.position,
            clampedPos,
            speed * Time.deltaTime
        );
    }

    Vector3 ClampToScreen(Vector3 targetPos)
    {
        float fixedHeight = mainCam.orthographicSize * 2f;
        float fixedWidth = fixedHeight * (9f / 16f);

        float minX = -fixedWidth / 2f;
        float maxX = fixedWidth / 2f;
        float minY = -fixedHeight / 2f;
        float maxY = fixedHeight / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        return targetPos;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        if (collision.gameObject.CompareTag("Collectible"))
        {
            GameManager.Instance.IncreaseScore();
            Destroy(collision.gameObject);
        }
    }
}