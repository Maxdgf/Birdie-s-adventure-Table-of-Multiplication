/*
* Description
* ----------------------------------------------------------------------------------------------
* This script at start, starts spawn periodically decoration objects - clouds in the game scene.
 */

using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float yEnd, yStart, spawnDelay, cloudLifeTime;
    [SerializeField] private GameObject objectToSpawn;


    void Start()
    {
        StartCoroutine(SpawnCloud(spawnDelay, objectToSpawn, yEnd)); // start spawn
    }

    /// <summary>
    /// Spawns 'cloud' game object with delay.
    /// </summary>
    /// <param name="delay">Delay.</param>
    /// <param name="cloud">Cloud game object.</param>
    /// <param name="yEnd">Y-axis max point.</param>
    private IEnumerator SpawnCloud(float delay, GameObject cloud, float yEnd)
    {
        while (true)
        {
            float randomY = Random.Range(yStart, yEnd);
            Vector2 spawnPos = new Vector2(transform.position.x, randomY);

            bool changeSizeChance = Random.Range(0, 1) == 1;

            GameObject spawnedCloud = Instantiate(cloud, spawnPos, Quaternion.identity);

            if (changeSizeChance)
            {
                float width = Random.Range(0f, 1f);
                float height = Random.Range(0f, 1f);

                spawnedCloud.transform.localScale = new Vector2(width, height);
            }

            StartCoroutine(DestroyAfterTimeElapsed(cloudLifeTime, spawnedCloud));

            yield return new WaitForSeconds(delay); // delay
        }
    }

    /// <summary>
    /// Destroys 'cloud' game object after current time.
    /// </summary>
    /// <param name="time">Delay.</param>
    /// <param name="cloud">Cloud game object.</param>
    private IEnumerator DestroyAfterTimeElapsed(float time, GameObject cloud)
    {
        yield return new WaitForSeconds(time); // delay
        Destroy(cloud); // destroy object
    }
}
