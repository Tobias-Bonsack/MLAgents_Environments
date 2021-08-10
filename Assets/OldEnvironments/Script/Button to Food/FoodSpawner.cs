using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    [Header("Food-Parameter")]
    [SerializeField] int numberOfFood;
    [SerializeField] GameObject food;
    [SerializeField] Vector3 spawnPoint;
    [SerializeField] int[] resetOffsets;
    [Header("Others")]
    [SerializeField] BTFEnviManager manager;

    public void SpawnFood()
    {
        for (int i = 0; i < numberOfFood; i++)
        {
            GameObject gameObject1 = GameObject.Instantiate(food);
            RandomPosition rR = gameObject1.GetComponent<RandomPosition>();
            rR.SetOrigin(spawnPoint);
            rR.SetValues(resetOffsets[0], resetOffsets[1], resetOffsets[2], resetOffsets[3]);
            rR.ResetTransform();

            manager.addFood(gameObject1);
        }
    }
}
