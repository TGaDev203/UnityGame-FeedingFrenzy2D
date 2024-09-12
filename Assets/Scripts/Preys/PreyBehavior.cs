using UnityEngine;

public class PreyBehavior : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float avoidanceDistance = 5f;
    [SerializeField] private float boundaryBuffer = 1f; // khoảng cách thêm vào để kiểm tra ranh giới

    [Header("Boundary")]
    [SerializeField] private Vector2 boundaryMin;
    [SerializeField] private Vector2 boundaryMax;

    private Transform characterTransform;

    private void Start()
    {
        // Lấy đối tượng nhân vật từ trong cùng một scene
        GameObject character = GameObject.FindGameObjectWithTag("Player");
        if (character != null)
        {
            characterTransform = character.transform;
        }
    }

    private void Update()
    {
        if (characterTransform != null)
        {
            MoveFreely();
            AvoidCharacter();
        }
    }

    private void MoveFreely()
    {
        // Di chuyển cá tự do trong khu vực
        Vector3 newPosition = transform.position + new Vector3(
            Mathf.Sin(Time.time) * speed * Time.deltaTime,
            Mathf.Cos(Time.time) * speed * Time.deltaTime,
            0
        );

        // Giới hạn di chuyển trong khu vực
        newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x + boundaryBuffer, boundaryMax.x - boundaryBuffer);
        newPosition.y = Mathf.Clamp(newPosition.y, boundaryMin.y + boundaryBuffer, boundaryMax.y - boundaryBuffer);

        transform.position = newPosition;
    }

    private void AvoidCharacter()
    {
        // Kiểm tra khoảng cách giữa cá và nhân vật
        float distanceToCharacter = Vector3.Distance(transform.position, characterTransform.position);

        if (distanceToCharacter < avoidanceDistance)
        {
            // Di chuyển cá ra ngoài khu vực chơi nếu gần nhân vật
            Vector3 directionAwayFromCharacter = (transform.position - characterTransform.position).normalized;
            transform.position += directionAwayFromCharacter * speed * Time.deltaTime;
        }

        // Kiểm tra nếu cá ra ngoài ranh giới
        if (IsOutOfBounds())
        {
            // Di chuyển cá ra ngoài ranh giới và ẩn cá
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, boundaryMin.x - 10, boundaryMax.x + 10),
                Mathf.Clamp(transform.position.y, boundaryMin.y - 10, boundaryMax.y + 10),
                transform.position.z
            );
            gameObject.SetActive(false); // Ẩn cá
            // Hoặc bạn có thể gọi Destroy(gameObject) để xóa cá hoàn toàn
        }
    }

    private bool IsOutOfBounds()
    {
        return transform.position.x < boundaryMin.x || transform.position.x > boundaryMax.x ||
               transform.position.y < boundaryMin.y || transform.position.y > boundaryMax.y;
    }
}
