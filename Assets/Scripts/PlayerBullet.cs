using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed;
    //public AudioClip audioClip;

    //private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8f;
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AsteroidTag"))
        {
            //audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        }
    }
}
