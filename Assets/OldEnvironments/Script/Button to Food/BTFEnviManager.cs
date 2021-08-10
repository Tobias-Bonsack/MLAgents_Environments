using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFEnviManager : MonoBehaviour
{

    [SerializeField] Material[] endMaterials;
    List<GameObject> spawnedFood;

    public event ResetPositionDelegate resetPositionEvent;
    public event ShowEndStatusDelegate showEndStatusEvent;

    public void StartResetPositionEvent()
    {
        resetPositionEvent();
    }

    public void StartShowEndStatusEvent(int index)
    {
        showEndStatusEvent(endMaterials[index]);
    }

    public void destroyFood()
    {
        if (spawnedFood != null)
        {
            foreach (GameObject food in spawnedFood)
            {
                Destroy(food);
            }
            spawnedFood.Clear();
        }
        spawnedFood = null;
    }

    public void addFood(GameObject o)
    {
        if (this.spawnedFood == null)
        {
            this.spawnedFood = new List<GameObject>() { o };
        }
        else
        {

            this.spawnedFood.Add(o);
        }
    }
}
