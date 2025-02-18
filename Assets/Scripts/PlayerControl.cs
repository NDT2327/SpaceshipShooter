using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBulletGO;
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject ExplosionGO;

    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Bắn đạn khi nhấn phím cách
        if (Input.GetKeyDown("space"))
        {
            // Khởi tạo viên đạn thứ nhất
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = BulletPosition01.transform.position; // Đặt vị trí ban đầu của đạn

            // Khởi tạo viên đạn thứ hai
            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = BulletPosition02.transform.position; // Đặt vị trí ban đầu của đạn
        }


        float x = Input.GetAxisRaw("Horizontal"); // for left, no input, and right
        float y = Input.GetAxisRaw("Vertical"); // for down, no input, and up

        // direction vector, normalized
        Vector2 direction = new Vector2(x, y).normalized;

        // Call the function
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // Find the screen's limits to player's movement
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collion
        if (collision.CompareTag("AsteroidTag"))
        {
            PlayerExplosion();
            Destroy(gameObject);
        }
    }

    void PlayerExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }
}
