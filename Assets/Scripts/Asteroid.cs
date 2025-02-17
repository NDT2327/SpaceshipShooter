using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidType { Small, Medium, Large }
    public AsteroidType Type;

    public GameObject explosionEffect;
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;
    public float rotationSpeed = 50f;

    private int health;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //set máu theo loại thiên thạch
        switch (Type)
        {
            case AsteroidType.Small:
                health = 1;
                break;
            case AsteroidType.Medium: health = 2; break;
            case AsteroidType.Large: health = 3; break;
        }

        //Di chuyển theo hướng ngẫu nhiên
        Vector2 moveDirection = Random.insideUnitSphere.normalized;
        float speed = Random.Range(minSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(0, -speed);
        //quay random
        rb.angularVelocity = Random.Range(-rotationSpeed, rotationSpeed);
    }

    void OnBecameInvisible()
    {
        Explode();    
    }

    void Explode()
    {
        if(explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
