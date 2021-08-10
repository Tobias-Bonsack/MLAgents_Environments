using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] DinoEventManager eventManager;
    [SerializeField] SpawnEngine[] spawner;
    [SerializeField] float speedMultiplikator;
    [SerializeField] float speedIncreasePercent;

    [Header("Environment Items")]
    [SerializeField] SpawnEngine[] backgroundSpawner;
    [SerializeField] SpawnEngine[] frontSpawner;
    [SerializeField] float spawnTimeForBackground;

    private void Start()
    {
        eventManager.spawnObstacleEvent += SpawnObstacle;

        StartCoroutine(spawnOhterEnvironment());
    }
    private void SpawnObstacle()
    {
        float percent = Random.Range(0f, 1f);

        int spawnIndex = 0;
        if (percent > 0.75f)
        {
            spawnIndex = 1;
        }
        else if (percent > 0.5f)
        {
            spawnIndex = 2;
        }

        spawner[spawnIndex].Spawn(speedMultiplikator);
        speedMultiplikator *= speedIncreasePercent;
    }

    private void OnDestroy()
    {
        eventManager.spawnObstacleEvent -= SpawnObstacle;
    }

    public float GetSpeedMultiplicator()
    {
        return speedMultiplikator;
    }

    public void SetSpeedMultiplicator(float multiplicator)
    {
        speedMultiplikator = multiplicator;
    }

    private IEnumerator spawnOhterEnvironment()
    {
        while (true)
        {
            backgroundSpawner[Random.Range(0, backgroundSpawner.Length)].Spawn(speedMultiplikator);
            yield return new WaitForSeconds(Random.Range(1f, spawnTimeForBackground));
            frontSpawner[Random.Range(0, frontSpawner.Length)].Spawn(speedMultiplikator);
            yield return new WaitForSeconds(Random.Range(1f, spawnTimeForBackground));
        }
    }
}
