using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float maxStateDuration = 5f; // Thời gian giữ trạng thái 5 viên

    private float powerDownTime = 5f; // Thời gian chờ trước khi giảm đạn
    private Coroutine powerDownCoroutine; // Coroutine để xử lý giảm đạn

    private List<GameObject> bulletPositions = new List<GameObject>();
    private int currentState = 0; // Trạng thái hiện tại

    private AudioSource audioSource;
    public AudioClip laserSound;
    private LogicScript logicScript; // Tham chiếu đến script quản lý điểm


    //heart
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] heartImages;
    private int health;


    //Blink effect
    public float blinkDuration = 1f;
    public int blinkCount = 5;
    private bool isInvincible = false; // Biến kiểm soát trạng thái bất tử
    public float invincibleDuration = 2f; // Thời gian bất tử sau khi nổ

    void Start()
    {
        bulletPositions.Add(BulletPosition01);
        bulletPositions.Add(BulletPosition02);

        // Ẩn BulletPosition03, BulletPosition04, BulletPosition05
        BulletPosition03.SetActive(false);
        BulletPosition04.SetActive(false);
        BulletPosition05.SetActive(false);

        //lấy audiosource component
        audioSource = GetComponent<AudioSource>();
        //heath initial
        health = heartImages.Length;

        logicScript = GameObject.FindGameObjectWithTag("LogicScore").GetComponent<LogicScript>();



    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSource.PlayOneShot(laserSound);
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

    //public void AddBulletPosition()
    //{
    //    switch (currentState)
    //    {
    //        case 0:
    //            BulletPosition03.SetActive(true);
    //            bulletPositions.Add(BulletPosition03);
    //            currentState = 1;
    //            break;

    //        case 1:
    //            BulletPosition03.SetActive(false);
    //            bulletPositions.Remove(BulletPosition03);

    //            BulletPosition04.SetActive(true);
    //            BulletPosition05.SetActive(true);
    //            bulletPositions.Add(BulletPosition04);
    //            bulletPositions.Add(BulletPosition05);

    //            currentState = 2;
    //            break;

    //        case 2:
    //            BulletPosition03.SetActive(true);
    //            bulletPositions.Add(BulletPosition03);
    //            currentState = 3;

    //            // Bắt đầu Coroutine để reset trạng thái sau maxStateDuration giây
    //            StartCoroutine(ResetToPreviousState(maxStateDuration));
    //            break;

    //        case 3:
    //            break;
    //    }
    //}
    public void AddBulletPosition()
    {
        switch (currentState)
        {
            case 0: // 2 viên -> 3 viên
                BulletPosition03.SetActive(true);
                bulletPositions.Add(BulletPosition03);
                currentState = 1;
                break;

            case 1: // 3 viên -> 5 viên
                BulletPosition03.SetActive(false);
                bulletPositions.Remove(BulletPosition03);

                BulletPosition04.SetActive(true);
                BulletPosition05.SetActive(true);
                bulletPositions.Add(BulletPosition04);
                bulletPositions.Add(BulletPosition05);

                currentState = 2;
                break;

            case 2: // 5 viên -> 5 viên + tăng thời gian giữ
                BulletPosition03.SetActive(true);
                bulletPositions.Add(BulletPosition03);
                currentState = 3;
                break;

            case 3:
                break;
        }

        // Reset thời gian giảm đạn nếu đang chạy
        if (powerDownCoroutine != null)
        {
            StopCoroutine(powerDownCoroutine);
        }
        powerDownCoroutine = StartCoroutine(PowerDown());
    }

    IEnumerator PowerDown()
    {
        while (currentState > 0)
        {
            yield return new WaitForSeconds(powerDownTime);

            // Giảm đạn xuống cấp độ thấp hơn
            switch (currentState)
            {
                case 3: // 5 viên -> 3 viên
                    BulletPosition03.SetActive(false);
                    bulletPositions.Remove(BulletPosition03);
                    currentState = 2;
                    break;

                case 2: // 3 viên -> 2 viên
                    BulletPosition04.SetActive(false);
                    BulletPosition05.SetActive(false);
                    bulletPositions.Remove(BulletPosition04);
                    bulletPositions.Remove(BulletPosition05);

                    BulletPosition03.SetActive(true);
                    bulletPositions.Add(BulletPosition03);
                    currentState = 1;
                    break;

                case 1: // 3 viên -> 2 viên (trạng thái mặc định)
                    BulletPosition03.SetActive(false);
                    bulletPositions.Remove(BulletPosition03);
                    currentState = 0;
                    break;
            }
        }
    }


    IEnumerator ResetToPreviousState(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentState == 3)
        {
            // Quay lại trạng thái trước đó (chỉ có BulletPosition01, 02, 04, 05)
            BulletPosition03.SetActive(false);
            bulletPositions.Remove(BulletPosition03);
            currentState = 2;
        }
    }

    IEnumerator BlinkEffect()
    {
        if (heartImages == null || heartImages.Length == 0)
        {
            yield break;
        }

        for (int i = 0; i < blinkCount; i++)
        {
            foreach (Image heartImage in heartImages)
            {
                if (heartImage != null)
                {
                    Color color = heartImage.color;
                    color.a = 0f; // Làm mờ trái tim (ẩn)
                    heartImage.color = color;
                }
            }

            yield return new WaitForSeconds(blinkDuration / (2f * blinkCount));

            foreach (Image heartImage in heartImages)
            {
                if (heartImage != null)
                {
                    Color color = heartImage.color;
                    color.a = 1f; // Hiển thị lại trái tim
                    heartImage.color = color;
                }
            }

            yield return new WaitForSeconds(blinkDuration / (2f * blinkCount));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AsteroidTag"))
        {
            TakeDamage();
            PlayerExplosion();
            //Destroy(gameObject);
        }
    }

    void TakeDamage()
    {
        if (health > 0)
        {
            health--;
            StartCoroutine(BlinkEffect());
            UpdateHealthUI();
            StartCoroutine(TemporaryInvincibility()); // Bắt đầu bất tử tạm thời
        }
    }

    void PlayerExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGO);
        explosion.transform.position = transform.position;
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health)
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }

        if (health <= 0)
        {
            PlayerExplosion();
            logicScript.gameOver();
            Destroy(gameObject);
        }
    }
    // Coroutine để tạo bất tử tạm thời
    IEnumerator TemporaryInvincibility()
    {
        isInvincible = true; // Kích hoạt bất tử
        yield return new WaitForSeconds(invincibleDuration); // Chờ trong khoảng thời gian bất tử
        isInvincible = false; // Kết thúc bất tử
    }
}
