using System.Collections;
using UnityEngine;

public class PowerUpSpawner1 : MonoBehaviour
{
    public GameObject powerUpPrefab; // Prefab của PowerUp
    public Transform[] spawnPositions; // Danh sách vị trí spawn
    public float spawnInterval; // Thời gian spawn PowerUp

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    void Spawn()
    {
        if (powerUpPrefab == null)
        {
            Debug.LogError("powerUpPrefab chưa được gán trong Inspector!");
            return;
        }

        if (spawnPositions == null || spawnPositions.Length == 0)
        {
            Debug.LogError("spawnPositions trống! Hãy thêm vị trí spawn trong Inspector.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPositions.Length);
        Transform spawnPoint = spawnPositions[randomIndex];

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint bị null! Kiểm tra danh sách spawnPositions.");
            return;
        }

        GameObject newPowerUp = Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
        newPowerUp.GetComponent<PowerUp>().SetDirection(Vector2.down); // Cho PowerUp rơi xuống
    }

}
