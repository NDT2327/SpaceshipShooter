using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float spawnAreaWidth = 10f;
    public int asteroidsPerWave = 6;
    public float speedIncreaseRate = 0.1f;
    public float spawnDelay = 0.5f; // Độ trễ giữa các lần spawn
    public float spawnIncreaseDuration = 5f; // Thời gian để tăng dần số lượng và tốc độ spawn
    public int maxAsteroidsPerWave = 10; // Giới hạn số thiên thạch tối đa mỗi lần spawn
    public float minSpawnDelay = 0.2f; // Giới hạn tối thiểu của spawn delay
    public float maxSpeedMultiplier = 3f;
    private float currentSpeedMultiplier = 1f;

    private float nextSpawnTime;
    private int currentAsteroidsPerWave = 1;
    private float timeSinceStart = 0f;
    

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime;
        currentSpeedMultiplier = Mathf.Min(1 + timeSinceStart* speedIncreaseRate, maxSpeedMultiplier);


    }
    

    IEnumerator SpawnWaveWithDelay()
    {
        for (int i = 0; i < asteroidsPerWave; i++)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            Debug.LogError("Không có thiên thạch nào để spawn!");
            return;
        }

        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroidToSpawn = asteroidPrefabs[randomIndex];

        if (asteroidToSpawn == null)
        {
            Debug.LogError("Thiên thạch bị lỗi hoặc bị phá hủy!");
            return;
        }

        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1f));
        spawnPosition.x += Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        spawnPosition.y += 2f;

        Instantiate(asteroidToSpawn, spawnPosition, Quaternion.identity);
    }

    // Coroutine để tăng số lượng thiên thạch và giảm thời gian spawn trong 5 giây
    IEnumerator IncreaseAsteroidSpawnRate()
    {
        float elapsedTime = 0f;
        int startAsteroids = asteroidsPerWave;
        float startSpawnDelay = spawnDelay;

        while (elapsedTime < spawnIncreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / spawnIncreaseDuration;

            // Tăng số lượng thiên thạch theo thời gian (giới hạn maxAsteroidsPerWave)
            asteroidsPerWave = Mathf.RoundToInt(Mathf.Lerp(startAsteroids, maxAsteroidsPerWave, progress));

            // Giảm thời gian delay giữa các lần spawn (không nhỏ hơn minSpawnDelay)
            spawnDelay = Mathf.Lerp(startSpawnDelay, minSpawnDelay, progress);

            yield return null; // Đợi frame tiếp theo
        }
    }
}
