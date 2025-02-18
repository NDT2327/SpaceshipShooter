using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2f;
    private AudioSource audioSource;

    private Vector2 moveDirection;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

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
        if (collision.CompareTag("PlayerShipTag"))
        {
            collision.GetComponent<PlayerControl>().AddBulletPosition(); // Gọi phương thức ăn PowerUp
            audioSource.Play();

            // Ẩn hình ảnh PowerUp để tránh hiện tượng "bị đứng" trên màn hình trong khi âm thanh đang phát
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, audioSource.clip.length); // Hủy PowerUp sau khi ăn
        }
    }
}
