using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidType { Small, Medium, Large }
    public AsteroidType Type;

    public GameObject explosionEffect;
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 50f;

    private int health = 1;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Di chuyển theo hướng ngẫu nhiên
        Vector2 moveDirection = Random.insideUnitSphere.normalized;
        float speed = Random.Range(minSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(0, -speed);
        //quay random
        rb.angularVelocity = Random.Range(-rotationSpeed, rotationSpeed);
        //set máu theo loại thiên thạch
        switch (Type)
        {
            case AsteroidType.Small:
                health = 3;
                break;
            case AsteroidType.Medium: health = 5; break;
            case AsteroidType.Large: health = 7; break;
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBulletTag"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Debug.Log($"Thiên thạch {gameObject.name} đang phát nổ."); // Thêm dòng này
        GameObject explode =  Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explode, 0.5f);
        Destroy(gameObject);
    }

}
