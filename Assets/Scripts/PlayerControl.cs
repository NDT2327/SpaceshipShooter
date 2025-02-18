using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject PlayerBulletGO;
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;

    public GameObject BulletPosition03;
    public GameObject BulletPosition04;
    public GameObject BulletPosition05;
    public GameObject ExplosionGO;


    public float speed;

    private List<GameObject> bulletPositions = new List<GameObject>();
    private int currentState = 0; // Trạng thái hiện tại (0: chỉ 2 vị trí, 1: +BP03, 2: ẩn BP03, hiện BP04 & BP05, 3: hiện cả 5 vị trí)

    void Start()
    {
        bulletPositions.Add(BulletPosition01);
        bulletPositions.Add(BulletPosition02);

        // Ẩn trước BulletPosition03, BulletPosition04, BulletPosition05
        BulletPosition03.SetActive(false);
        BulletPosition04.SetActive(false);
        BulletPosition05.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            foreach (GameObject bulletPos in bulletPositions)
            {
                GameObject bullet = Instantiate(PlayerBulletGO);
                bullet.transform.position = bulletPos.transform.position;
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.x -= 0.225f;
        min.x += 0.225f;
        max.y -= 0.285f;
        min.y += 0.285f;

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }


    public void AddBulletPosition()
    {
        switch (currentState)
        {
            case 0: // Chỉ có BulletPosition01 & BulletPosition02 -> Bật thêm BulletPosition03
                BulletPosition03.SetActive(true);
                bulletPositions.Add(BulletPosition03);
                currentState = 1;
                break;

            case 1: // Có BulletPosition01, BulletPosition02, BulletPosition03 -> Ẩn BulletPosition03, hiện BulletPosition04 & BulletPosition05
                BulletPosition03.SetActive(false);
                bulletPositions.Remove(BulletPosition03);

                BulletPosition04.SetActive(true);
                BulletPosition05.SetActive(true);
                bulletPositions.Add(BulletPosition04);
                bulletPositions.Add(BulletPosition05);

                currentState = 2;
                break;

            case 2: // Có BulletPosition01, BulletPosition02, BulletPosition04, BulletPosition05 -> Hiện tất cả 5 vị trí
                BulletPosition03.SetActive(true);
                bulletPositions.Add(BulletPosition03);
                currentState = 3;
                break;

            case 3: // Đã có đủ 5 vị trí -> Không làm gì cả
                break;
        }
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
