using UnityEngine;

public class StarSpawnerScript : MonoBehaviour
{
    public GameObject star; // Prefab của ngôi sao
    public float minSpawnTime = 0f;  // Thời gian spawn tối thiểu
    public float maxSpawnTime = 20f; // Thời gian spawn tối đa
    public float minX = -8f, maxX = 8f; // Phạm vi spawn theo trục X
    //public float minY = -4f, maxY = 4f; // Phạm vi spawn theo trục Y

    private float nextSpawnTime; // Lưu thời gian spawn tiếp theo

    void Start()
    {
        // Xác định thời gian spawn đầu tiên
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnStar();
            SetNextSpawnTime();
        }
    }

    void SpawnStar()
    {
        float randomX = Random.Range(minX, maxX);
        //float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        Instantiate(star, spawnPosition, Quaternion.identity);
    }

    void SetNextSpawnTime()
    {
        // Random thời gian cho lần spawn tiếp theo
        float randomInterval = Random.Range(minSpawnTime, maxSpawnTime);
        nextSpawnTime = Time.time + randomInterval;
    }
}
