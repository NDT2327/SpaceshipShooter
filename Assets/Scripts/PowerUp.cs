using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 moveDirection;

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().AddBulletPosition(); // Gọi phương thức ăn PowerUp
            Destroy(gameObject); // Hủy PowerUp sau khi ăn
        }
    }
}
