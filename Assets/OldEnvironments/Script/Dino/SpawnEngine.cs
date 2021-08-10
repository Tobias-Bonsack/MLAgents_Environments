using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEngine : MonoBehaviour
{

    [SerializeField] GameObject[] canSpawnPrefab;

    [SerializeField] float yOffset;

    public void Spawn(float m)
    {
        int indexToSpawn = Random.Range(0, canSpawnPrefab.Length);
        GameObject gameObject1 = Instantiate(canSpawnPrefab[indexToSpawn], transform.position - Vector3.up * yOffset, transform.rotation);
        gameObject1.GetComponent<MoveXPosition>().MultiplicateSpeed(m);
    }

}
