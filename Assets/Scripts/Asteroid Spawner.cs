using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float spawnRate = 1.5f;
    public float spawnXMin = -8f;
    public float spawnXMax = 8f;
    public float spawnY = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnAsteroid", 1f, spawnRate);
    }

    void SpawnAsteroid()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroid = asteroidPrefabs[randomIndex];

        Instantiate(asteroid, spawnPosition, Quaternion.identity);
    }
}
