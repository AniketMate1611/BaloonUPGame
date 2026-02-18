using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Vector2 horizontalLimits = new Vector2(-2f, 2f);
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] Transform baloonTransform;
    [SerializeField] Vector2 obstacleScale;
    [SerializeField] float verticalHeight = 6f;

    float currentSpawnTime;

    int wave = 1;
    int difficultyLevel = 0;
    int nextScoreThreshold = 20;

    private void Update()
    {
        if (GameManager.Instance.score >= nextScoreThreshold)
        {
            IncreaseDifficulty();
            nextScoreThreshold += 20;
        }

        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime >= spawnDelay)
        {
            Spawn();
            currentSpawnTime = 0f;
        }
    }

    void IncreaseDifficulty()
    {
        difficultyLevel++;
        wave++;

        spawnDelay = Mathf.Clamp(spawnDelay - 0.1f, 0.3f, 1f);

        Debug.Log("Difficulty Increased: " + difficultyLevel);
    }

    void Spawn()
    {
        float spawnY = baloonTransform.position.y + verticalHeight;
        float spawnX = Random.Range(horizontalLimits.x, horizontalLimits.y);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        Rigidbody2D obstacleRb = obstacle.GetComponent<Rigidbody2D>();
        obstacleRb.gravityScale += 0.05f * wave;

        float randomScale = Random.Range(obstacleScale.x, obstacleScale.y);
        obstacle.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        Destroy(obstacle, 10f);
    }
}
