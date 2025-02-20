using UnityEngine;

public class StarsScript : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public int score = 100;
    public float deadZone = -7f;

    public GameObject explosionPrefab;
    private AudioSource audioSource;
    private LogicScript logicScript; // Tham chiếu đến script quản lý điểm


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        logicScript = GameObject.FindGameObjectWithTag("LogicScore").GetComponent<LogicScript>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem Star có chạm vào PlayerShip không
        if (collision.CompareTag("PlayerShipTag"))
        {
            // Thêm hàm cộng điểm ở đây nếu cần
            // Example: ScoreManager.Instance.AddScore(10);

            // Hiển thị hiệu ứng nổ nếu có
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.5f); // Xóa hiệu ứng sau 0.5 giây
            }
            audioSource.Play();
            // Trì hoãn hủy gameObject để hiệu ứng hiển thị trước khi biến mất
            GetComponent<SpriteRenderer>().enabled = false; // Ẩn sprite ngay khi va chạm
            GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa collider tránh va chạm tiếp
            Destroy(audioSource, audioSource.clip.length);
            Destroy(gameObject, 0.5f); // Chờ 0.5 giây rồi xóa
            logicScript.addScore(score); // Cộng điểm khi thiên thạch nổ

        }
    }

}
