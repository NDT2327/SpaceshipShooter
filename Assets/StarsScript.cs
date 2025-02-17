using UnityEngine;

public class StarsScript : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public int score = 100;
    public float deadZone = -7f;

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
        Destroy(gameObject);
    }
}
