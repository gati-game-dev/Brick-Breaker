using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject threeBallsPrefab;
    public GameObject widePaddlePrefab;
    public float minSpawnTime = 4f;
    public float maxSpawnTime = 7f;
    public float minX = -7f;
    public float maxX = 7f;
    public float spawnY = 6f;

    private float spawnTimer;
    void Start()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsWaiting)
        {
            return;
        }                       // Don't spawn power-ups while waiting for the player to press a key to launch the ball

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            int randomPowerUp = Random.Range(0, 2);

            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            if (randomPowerUp == 0)
            {
                Instantiate(widePaddlePrefab, spawnPosition, Quaternion.identity);  // spawn wide paddle
            }
            else
            {
                Instantiate(threeBallsPrefab, spawnPosition, Quaternion.identity);  // spawn three balls
            }

            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
