using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnInterval = 1.5f;          // Thời gian giữa các lần spawn (giây)
    public float spawnAreaWidth = 10f;         // Chiều rộng khu vực spawn
    public float initialSpeed = 3f;          // Tốc độ ban đầu của thiên thạch
    public float maxSpeed = 10f;             // Tốc độ tối đa của thiên thạch
    public int minAsteroidsPerSpawn = 1;     // Số lượng thiên thạch tối thiểu mỗi lần spawn
    public int maxAsteroidsPerSpawn = 2;     // Số lượng thiên thạch tối đa mỗi lần spawn
    public int maxAsteroidLimit = 6;
    public float speedIncreaseInterval = 15f; // Thời gian để tăng tốc độ rơi của thiên thạch (giây)
    public float speedIncreaseAmount = 1.5f;  // Mức tăng tốc độ mỗi lần
    public float increaseSpawnInterval = 20f;
    public int asteroidIncreaseAmount = 1;

    private float timeSinceLastSpawn = 0f;
    private float timeSinceLastSpeedIncrease = 0f;
    private float timeSinceLastSpawnIncrease = 0f;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastSpeedIncrease += Time.deltaTime;

        // Tăng tốc độ theo thời gian
        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
            currentSpeed = Mathf.Min(currentSpeed + speedIncreaseAmount, maxSpeed);
            timeSinceLastSpeedIncrease = 0f;
        }

        //tang so luong thien thach spawn theo thoi gian
        if(timeSinceLastSpawnIncrease >= increaseSpawnInterval)
        {
            maxAsteroidsPerSpawn = Mathf.Min(maxAsteroidsPerSpawn + asteroidIncreaseAmount, maxAsteroidLimit);
            timeSinceLastSpawnIncrease = 0f;
        }

        // Kiểm tra xem đã đến thời gian spawn chưa
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnAsteroidWave();
            timeSinceLastSpawn = 0f; // Reset thời gian
        }
    }


    void SpawnAsteroidWave()
    {
        //Tính toán số lượng thiên thạch ngẫu nhiên cho đợt này
        int asteroidCount = Random.Range(minAsteroidsPerSpawn, maxAsteroidsPerSpawn + 1);

        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        // Chọn ngẫu nhiên một prefab
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Tính toán vị trí spawn ngẫu nhiên
        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1f));
        spawnPosition.x += Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);

        // Tạo instance của thiên thạch
        GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Thiết lập tốc độ cho thiên thạch
        Rigidbody2D rb = newAsteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * currentSpeed;
        }
    }
}