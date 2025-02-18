using UnityEngine;

public class StarsScript : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public int score = 100;
    public float deadZone = -7f;

    public GameObject explosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        if(transform.position.y < deadZone)
        {
            Debug.Log("Star remove");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //thêm hàm cộng score ở đây

        //
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f); // Xóa hiệu ứng sau 1 giây
        }

        // Trì hoãn hủy gameObject để hiệu ứng hiển thị trước khi biến mất
        GetComponent<SpriteRenderer>().enabled = false; // Ẩn sprite ngay khi va chạm
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa collider tránh va chạm tiếp
        Destroy(gameObject, 0.5f); // Chờ 0.5 giây rồi xóa
    }
}
